using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serveur.Models;

namespace Serveur.Hubs
{
    public class CardGameHub : Hub , IContractCardGame
    {
        
        public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
        public static Dictionary<string, ApplicationUser> Users = new Dictionary<string, ApplicationUser>();
        public static int nbCli = 0;

        /// <summary>
        /// Get the Room in Rooms corresponding to the parameter
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>Room or null</returns>
        private Room GetRoom(string roomId)
        {
            lock (Rooms)
            {
                return Rooms.GetValueOrDefault(roomId);
            }
        }

        /// <summary>
        /// Get the ApplicationUser in Users corresponding to the parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>ApplicationUser or null</returns>
        private ApplicationUser GetUser(string userId)
        {
            lock (Users)
            {
                return Users.GetValueOrDefault(userId);
            }
        }

        //
        /// <summary>
        /// Generate a GUID which isn't already exist in the Room Dictionary.
        /// </summary>
        /// <returns>string</returns>
        private string NewGuidGeneration()
        {
            string guid = "";
            lock (Rooms)
            {
                while (guid == "" || Rooms.ContainsKey(guid))
                {
                    guid = Guid.NewGuid().ToString();
                }
            }
            
            return guid;
        }
        
        /// <summary>
        /// When a user is connected to the server,
        /// a new Instance of ApplicationUser is created
        /// and added in Users with the ConnectionId as key.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Passage dans Connection");
            ApplicationUser user = new ApplicationUser(Context.ConnectionId, "User " + nbCli);
            lock (Users)
            {
                Users.Add(Context.ConnectionId, user);
            }
            await Clients.Others.SendAsync("Connect", user.UserName);
            nbCli++;
            await base.OnConnectedAsync();
        }


        // <summary>
        /// When a user is disconnected,
        /// He's removed from this Users List
        /// and from each rooms and every Clients
        /// are prevented of its disconnection.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);
            string UserName = user.UserName;
            lock (Users)
            {
                Users.Remove(Context.ConnectionId);
            }
            lock (Rooms)
            {
                foreach (Room r in Rooms.Values)
                {
                    RemoveUserFromRoom(r, user);
                }
            }
            

            await Clients.All.SendAsync("Disconnected", UserName);
            Console.WriteLine("Passage dans Déconnection");
            await base.OnDisconnectedAsync(ex);
        }

        /// <summary>
        /// Function called when a user has left a room
        /// </summary>
        /// <param name="r"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task RemoveUserFromRoom(Room r, ApplicationUser user)
        {
            bool before = r.isComplete();
            bool result;
            lock (r)
            {
                result = r.RemoveUser(Context.ConnectionId);
            }
            await Clients.Caller.SendAsync("GameIsLeft", r.RoomId);

            foreach (string ids in r.Public.Keys)
            {
                await Clients.Client(ids).SendAsync("LeftTheGame", r.RoomId, user.UserName);
            }

            if (result)
            {
                if ((before && !r.isComplete()) || r.Players.Count == 0)
                {
                    RemoveRoom(r.RoomId);
                }
            }
        }


        /// <summary>
        /// When a user ask for creating a newRoom
        /// A new Room is created assoiated with a new GUID,
        /// Added to the rooms list and the Caller Client is added
        /// To the players list of the new room.
        /// </summary>
        /// <returns></returns>
        public async Task NewGroup()
        {
            string guid = NewGuidGeneration();

            Room r = new Room(guid);

            lock (Rooms)
            {
                Rooms.Add(guid, r);
            }

            await Clients.Caller.SendAsync("ReceiveNewGroup", guid);
            await Clients.Others.SendAsync("NewGroupCreated", guid);

            JoinGroup(guid);
        }


        /// <summary>
        /// When a user want to play in a Room
        /// The room is searched in the Room list and, 
        /// if it exists, add the user to the room.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task JoinGroup(string guid)
        {
            Room r = GetRoom(guid);
            ApplicationUser user = GetUser(Context.ConnectionId);
            
            if (r != null && user != null)
            {
                bool result;
                lock (r)
                {
                    result = r.AddPlayer(user);
                }

                if (result)
                {
                    await Clients.Caller.SendAsync("JoinGroup", guid);

                    foreach (ApplicationUser u in r.Public.Values)
                    {
                        if (u.UserId != Context.ConnectionId)
                        {
                            await Clients.Client(u.UserId).SendAsync("UserJoinedGroup", user.UserName, guid);
                        }
                    }

                    if (r.isComplete())
                    {
                        await Clients.All.SendAsync("RoomComplete", r.RoomId);
                    }
                }
            }

        }


        /// <summary>
        /// When a user wants to see what append in a Room identified by the guid parameter
        /// The room is searched in the Room list and, if it exists, add the user to this 
        /// rooms' public and prevent every user in it.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task AskForSee(string guid)
        {
            Room r = GetRoom(guid);
            ApplicationUser user = GetUser(Context.ConnectionId);
            
            if (r != null && user != null)
            {
                bool result;
                lock (r)
                {
                    result = r.AddPublic(user);
                }

                if (result)
                {
                    await Clients.Caller.SendAsync("JoinGroup", guid);
                    foreach(string id in r.Players)
                    {
                        await Clients.Client(id).SendAsync("UserSee", user.UserId, guid);
                    }
                }
            }
        }


        /// <summary>
        /// A Function called when a user wants to quit a room
        /// First we verify if he's in the database before removeing it. 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task QuitGame(string guid)
        {
            Room r = GetRoom(guid);
            ApplicationUser user = GetUser(Context.ConnectionId);
            
            if (r != null && user != null)
            {
                await RemoveUserFromRoom(r, user);
            }
        }


        /// <summary>
        /// Function called if a Room have to bee destroyed.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task RemoveRoom(string guid)
        {
            Room r = GetRoom(guid);
            
            if (r!= null)
            {
                foreach(string id in r.Public.Keys){
                    await Clients.Client(id).SendAsync("YourRoomIsDestroyed", guid);
                }

                await Clients.All.SendAsync("RoomDestroyed", guid);
            }
        }
    }
}
