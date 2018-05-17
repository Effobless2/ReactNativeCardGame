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
    public class CardGameHub : Hub /*, IContractCardGame, IContractCardGameHub*/
    {

        public static Lazy<APICardGame> cardGame = new Lazy<APICardGame>();
        
        /// <summary>
        /// When a user is connected to the server,
        /// a new Instance of ApplicationUser is created
        /// and added in Users with the ConnectionId as key.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            ApplicationUser user = cardGame.Value.Connection(Context.ConnectionId);
            await Clients.Caller.SendAsync(MessagesConstants.CONNECTION_BEGIN, user, cardGame.Value.GetUsers(), cardGame.Value.GetRooms());
            await Clients.Others.SendAsync(MessagesConstants.CONNECT, user);

            await base.OnConnectedAsync();
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
                List<string> roomsToUpdate = cardGame.Value.RemovingUser(Context.ConnectionId);
                foreach(string roomId in roomsToUpdate)
                {
                    await UpdateRoom(roomId, Context.ConnectionId);
                }
                await Clients.All.SendAsync(MessagesConstants.DISCONNECT, Context.ConnectionId);
            }
            catch (UserIsUndefinedException e)
            {

            }
        }

        public async Task UpdateRoom(string roomId, string userId)
        {
            bool toDestroy = cardGame.Value.UpdateRoom(roomId, userId);
            if (toDestroy)
            {
                RemovingRoom(roomId);
            }
            else
            {
                await Clients.All.SendAsync(MessagesConstants.PUBLIC_REMOVED, roomId, userId);
            }
        }

        public async Task CreatingRoom()
        {
            Room room = cardGame.Value.CreatingRoom();
            await Clients.All.SendAsync(MessagesConstants.ROOM_CREATED, room);
            AddingPlayer(room.RoomId);
        }

        public async Task AddingPlayer(string roomId)
        {
            try
            {
                bool ready = cardGame.Value.AddingPlayer(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.NEW_PLAYER, roomId, Context.ConnectionId);
                if (ready)
                {
                    await Clients.All.SendAsync(MessagesConstants.READY, roomId);
                }
            }
            catch (UserIsUndefinedException e)
            {
                
            }
            catch (RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (FulfillRoomException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_FULFILL, roomId);
            }
            catch (AlreadyInRoomException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ALREADY_IN_ROOM, roomId);
            }
        }

        public async Task AddingPublic(string roomId)
        {
            try
            {
                cardGame.Value.AddingPublic(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.NEW_PUBLIC, roomId, Context.ConnectionId);
            }
            catch (UserIsUndefinedException e)
            {

            }
            catch (RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (AlreadyInRoomException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ALREADY_IN_ROOM, roomId);
            }
        }

        public async Task RemovingPublic(string roomId)
        {
            try
            {
                cardGame.Value.RemovingPublic(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.PUBLIC_REMOVED, roomId, Context.ConnectionId);
            }
            catch (UserIsUndefinedException e)
            {

            }
            catch (RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (NotInThisRoomException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.NOT_IN_THIS_ROOM, roomId);
            }
        }

        public async Task RemovingPlayer(string roomId)
        {
            try
            {
                bool toDestroy = cardGame.Value.RemovingPlayer(roomId, Context.ConnectionId);
                await Clients.All.SendAsync(MessagesConstants.PLAYER_REMOVED, roomId, Context.ConnectionId);
                if (toDestroy)
                {
                    RemovingRoom(roomId);
                }
            }
            catch (UserIsUndefinedException e)
            {

            }
            catch (RoomIsUndefinedException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.ROOM_IS_UNDEFINED, roomId);
            }
            catch (NotInThisRoomException e)
            {
                await Clients.Caller.SendAsync(MessagesConstants.NOT_IN_THIS_ROOM, roomId);
            }

        }

        public async Task RemovingRoom(string roomId)
        {
            try
            {
                List<string> usersToExtract = cardGame.Value.RemovingRoom(roomId);
                await Clients.All.SendAsync(MessagesConstants.ROOM_REMOVED, roomId);
                await ExtractingUsers(usersToExtract, roomId);
            }
            catch (RoomIsUndefinedException e)
            {

            }
        }

        public async Task ExtractingUsers(List<string> usersToExtract, string roomId)
        {
            List<string> usersConnectedForExtraction = cardGame.Value.ExtractingUsers(usersToExtract, roomId);
            foreach(string userId in usersConnectedForExtraction)
            {
                await Clients.Client(userId).SendAsync(MessagesConstants.EJECTED_FROM_ROOM, roomId);
            }

        }

        

        /*



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

                foreach (string user in room.Public)
                {
                    if (!Context.ConnectionId.Equals(user))
                    {
                        await Clients.Client(user).SendAsync("NewPlayer", currentUser, room);
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
                
                foreach (string user in room.Public)
                {
                    if (!user.Equals(Context.ConnectionId))
                    {
                        await Clients.Client(user).SendAsync("NewPublic", currentUser, room);
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
                foreach (string user in room.Public)
                {
                    await Clients.Client(user).SendAsync("LeftTheGame", room, currentUser);
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
                foreach (string r in user.rooms)
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

        */





    }
}
