using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serveur.Models;
using Serveur.Models.Exceptions;

namespace Serveur.Hubs
{
    /// <summary>
    /// Realize the connection between the Server and Clients
    /// </summary>
    public class CardGameHub : Hub , IContractCardGame, IContractCardGameHub
    {

        public static Lazy<CardGame> cardGame = new Lazy<CardGame>();

        public Room GetRoom(string roomId)
        {
            try
            {
                return cardGame.Value.GetRoom(roomId);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public ApplicationUser GetUser(string userId)
        {
            try
            {
                return cardGame.Value.GetUser(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Room NewRoom()
        {
            return cardGame.Value.NewRoom();
        }

        public ApplicationUser AddUser(string user)
        {
            try
            {
                return cardGame.Value.AddUser(user);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool AddPlayer(string idRoom, string idUser)
        {
            try
            {
                return cardGame.Value.AddPlayer(idRoom, idUser);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool AddPublic(string idRoom, string idUser)
        {
            try
            {
                return cardGame.Value.AddPublic(idRoom, idUser);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool LeaveGame(string idRoom, string idUser)
        {
            try
            {
                return cardGame.Value.LeaveGame(idRoom, idUser);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<ApplicationUser> RemoveRoom(string idRoom)
        {
            try
            {
                return cardGame.Value.RemoveRoom(idRoom);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public ApplicationUser RemoveUser(string idUser)
        {
            try
            {
                return cardGame.Value.RemoveUser(idUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task CreatingRoom()
        {
            Room room = NewRoom();

            await Clients.Caller.SendAsync("ReceiveNewRoom", room);
            await Clients.Others.SendAsync("NewRoomCreated", room);

            await AddingPlayer(room.RoomId);
        }

        public async Task AddingUser(string idUser)
        {
            ApplicationUser user = AddUser(idUser);
            await Clients.Caller.SendAsync("ConnectionBegin", user, cardGame.Value.Users, cardGame.Value.Rooms);
            await Clients.Others.SendAsync("Connect", user);
        }

        public async Task AddingPlayer(string idRoom)
        {
            Room room = null;
            try
            {
                room = GetRoom(idRoom);
                AddPlayer(idRoom, Context.ConnectionId);

                await Clients.Caller.SendAsync("JoinPlayers", room);
                
                ApplicationUser currentUser = GetUser(Context.ConnectionId);

                Console.WriteLine("Task AddPlayer");

                foreach (ApplicationUser user in room.Public.Values)
                {
                    if (!Context.ConnectionId.Equals(user.UserId))
                    {
                        await Clients.Client(user.UserId).SendAsync("NewPlayer", currentUser, room);
                    }
                }
            }
            catch (AlreadyInRoomException e)
            {
                Console.WriteLine("Alreaady ex");
                await Clients.Caller.SendAsync("AlreadyInRoom", room);
            }
            catch (FulfillRoomException e)
            {
                Console.WriteLine("Fulfill ex");
                await Clients.Caller.SendAsync("RoomFulFill", room);
            }

            catch(UserIsUndefinedException e)
            {
                Console.WriteLine("User Undef ex");
                await Clients.Caller.SendAsync("UserUndefined", Context.ConnectionId);
            }

            catch(RoomIsUndefinedException e)
            {

                Console.WriteLine("Room Undef ex");
                await Clients.Caller.SendAsync("RoomIsUndefined", idRoom);
            }
        }

        public async Task AddingPublic(string idRoom)
        {
            Room room = null;
            try
            {
                room = GetRoom(idRoom);
                AddPublic(idRoom, Context.ConnectionId);

                ApplicationUser currentUser = GetUser(Context.ConnectionId);

                await Clients.Caller.SendAsync("JoinPublic", room);
                
                foreach (ApplicationUser user in room.Public.Values)
                {
                    if (!user.UserId.Equals(Context.ConnectionId))
                    {
                        await Clients.Client(user.UserId).SendAsync("NewPublic", currentUser, room);
                    }
                }
            }
            catch (AlreadyInRoomException e)
            {
                await Clients.Caller.SendAsync("AlreadyInRoom", room);
            }
            catch(RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("RoomIsUndefined", idRoom);
            }
            catch(UserIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("UserIsUndefinded", Context.ConnectionId);
            }
        }

        public async Task LeavingGame(string idRoom)
        {
            Room room = null;
            try
            {
                ApplicationUser currentUser = GetUser(Context.ConnectionId);
                room = GetRoom(idRoom);

                bool erase = LeaveGame(idRoom, Context.ConnectionId);
                await Clients.Caller.SendAsync("GameIsLeft", room);
                foreach (ApplicationUser user in room.Public.Values)
                {
                    await Clients.Client(user.UserId).SendAsync("LeftTheGame", room, currentUser);
                }
                if (erase)
                {
                    RemovingRoom(idRoom);
                }
            }
            catch(NotInThisRoomException e)
            {
                await Clients.Caller.SendAsync("NotInThisRoom", room);
            }
            catch (RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("RoomIsUndefined", idRoom);
            }
            catch (UserIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("UserIsUndefinded", Context.ConnectionId);
            }

        }

        public async Task RemovingRoom(string idRoom)
        {
            try
            {
                Room currentRoom = GetRoom(idRoom);
                List<ApplicationUser> users = RemoveRoom(idRoom);

                foreach (ApplicationUser user in users)
                {
                    await Clients.Client(user.UserId).SendAsync("YourRoomIsDestroyed", currentRoom);
                }

                await Clients.All.SendAsync("RoomDestroyed", currentRoom);
            }
            catch(RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("RoomIsUndefined", idRoom);
            }
        }

        public async Task RemovingUser(string idUser)
        {
            ApplicationUser user = null;

            try
            {
                user = GetUser(idUser);
                foreach (string r in user.rooms.Keys)
                {
                    Console.WriteLine(r);
                    LeavingGame(r);
                }
                RemoveUser(idUser);
                await Clients.All.SendAsync("Disconnect", user);
            }
            catch(UserIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("UserIsUndefined", Context.ConnectionId);
            }
        }

        
        /// <summary>
        /// When a user is connected to the server,
        /// a new Instance of ApplicationUser is created
        /// and added in Users with the ConnectionId as key.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            try
            {
                AddingUser(Context.ConnectionId);
            }
            catch(UserIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("UserIsUndefined", Context.ConnectionId);
            }
            finally
            {
                await base.OnConnectedAsync();
            }
        }


        /// <summary>
        /// When a user is disconnected,
        /// He's removed from this Users List
        /// and from each rooms and every Clients
        /// are prevented of its disconnection.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            try
            {
                RemovingUser(Context.ConnectionId);
            }
            catch(UserIsUndefinedException e)
            {
                await Clients.Caller.SendAsync("UserIsUndefined", Context.ConnectionId);
            }
            finally
            {
                await base.OnDisconnectedAsync(ex);
            }
        }
    }
}
