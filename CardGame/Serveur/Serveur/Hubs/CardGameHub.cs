using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serveur.Models;

namespace Serveur.Hubs
{
    public class CardGameHub : Hub , IContractCardGame, IContractCardGameHub
    {

        public static Lazy<CardGame> cardGame = new Lazy<CardGame>();
        public static int nbCli = 0;

        public Room GetRoom(string roomId)
        {
            return cardGame.Value.GetRoom(roomId);
        }

        public ApplicationUser GetUser(string userId)
        {
            return cardGame.Value.GetUser(userId);
        }

        public Room NewRoom()
        {
            return cardGame.Value.NewRoom();
        }

        public ApplicationUser AddUser(string user)
        {
            return cardGame.Value.AddUser(user);
        }

        public bool AddPlayer(string idRoom, string idUser)
        {
            return cardGame.Value.AddPlayer(idRoom, idUser);
        }

        public bool AddPublic(string idRoom, string idUser)
        {
            return cardGame.Value.AddPublic(idRoom, idUser);
        }

        public bool LeaveGame(string idRoom, string idUser)
        {
            return cardGame.Value.LeaveGame(idRoom, idUser);
        }

        public List<ApplicationUser> RemoveRoom(string idRoom)
        {
            return cardGame.Value.RemoveRoom(idRoom);
        }

        public ApplicationUser RemoveUser(string idUser)
        {
            return cardGame.Value.RemoveUser(idUser);
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
            await Clients.Caller.SendAsync("Connect", user);
            await Clients.Others.SendAsync("Connect", user);
        }

        public async Task AddingPlayer(string idRoom)
        {
            AddPlayer(idRoom, Context.ConnectionId);
            Room currentRoom = GetRoom(idRoom);

            await Clients.Caller.SendAsync("JoinPlayers", currentRoom);

            ApplicationUser currentUser = GetUser(Context.ConnectionId);

            foreach (ApplicationUser user in currentRoom.Public.Values)
            {
                if (!Context.ConnectionId.Equals(user.UserId))
                {
                    await Clients.Client(user.UserId).SendAsync("NewPlayer", currentUser, currentRoom);
                }
            }

        }

        public async Task AddingPublic(string idRoom)
        {
            AddPublic(idRoom, Context.ConnectionId);

            ApplicationUser currentUser = GetUser(Context.ConnectionId);
            Room room = GetRoom(idRoom);

            await Clients.Caller.SendAsync("JoinPublic", room);


            foreach (ApplicationUser user in room.Public.Values)
            {
                if (!user.UserId.Equals(Context.ConnectionId))
                {
                    await Clients.Client(user.UserId).SendAsync("NewPublic", currentUser, room);
                }
            }
        }

        public async Task LeavingGame(string idRoom)
        {
            bool erase = LeaveGame(idRoom, Context.ConnectionId);
            ApplicationUser currentUser = GetUser(Context.ConnectionId);
            Room room = GetRoom(idRoom);
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

        public async Task RemovingRoom(string idRoom)
        {
            Room currentRoom = GetRoom(idRoom);
            List<ApplicationUser> users = RemoveRoom(idRoom);

            foreach (ApplicationUser user in users)
            {
                await Clients.Client(user.UserId).SendAsync("YourRoomIsDestroyed", currentRoom);
            }

            await Clients.All.SendAsync("RoomDestroyed", currentRoom);


        }

        public async Task RemovingUser(string idUser)
        {
            ApplicationUser user = GetUser(idUser);

            RemoveUser(idUser);

            await Clients.All.SendAsync("Disconnect", user);
        }

        
        /// <summary>
        /// When a user is connected to the server,
        /// a new Instance of ApplicationUser is created
        /// and added in Users with the ConnectionId as key.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            AddingUser(Context.ConnectionId);
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
            RemovingUser(Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
