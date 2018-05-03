using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serveur.Models;

namespace Serveur.Hubs
{
    public class MainHub : Hub
    {

        public static string Path = "/main";
        public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
        public static Dictionary<string, ApplicationUser> Users = new Dictionary<string, ApplicationUser>();


        public static int nbCli = 0;
        //Generate a GUID which isn't already exist in the Room Dictionary.
        private string NewGuidGeneration()
        {
            string guid = "";
            while (guid == "" || Rooms.ContainsKey(guid)){
                guid = Guid.NewGuid().ToString();
            }
            return guid;
        }

        //When a user is connected to the server,
        //a new Instance of ApplicationUser is created
        //and added in Users with the ConnectionId as key.
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Passage dans Connection");
            ApplicationUser user = new ApplicationUser(Context.ConnectionId, "User " + nbCli);
            Users.Add(Context.ConnectionId, user);
            await Clients.Others.SendAsync("Connect", user.UserName);
            await base.OnConnectedAsync();
            nbCli++;
        }

        //When a user is disconnected,
        //He's removed from this Users List
        //and from each rooms and every Clients 
        //are prevented of its disconnection.
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);
            string UserName = user.UserName;
            Users.Remove(Context.ConnectionId);
            foreach (Room r in Rooms.Values)
            {
                await RemoveUserFromRoom(r, user);
            }

            await Clients.All.SendAsync("Disconnected", UserName);
            Console.WriteLine("Passage dans Déconnection");
            await base.OnDisconnectedAsync(ex);
        }

        //Function called when a user has left a room
        private async Task RemoveUserFromRoom(Room r, ApplicationUser user)
        {
            bool before = r.isComplete();
            bool result = r.RemoveUser(Context.ConnectionId);
            await Clients.Caller.SendAsync("GameIsLeft", r.RoomId);

            foreach (string ids in r.Public.Keys)
            {
                await Clients.Client(ids).SendAsync("LeftTheGame", r.RoomId, user.UserName);
            }

            if (result)
            {
                if (before && !r.isComplete())
                {
                    await RemoveRoom(r.RoomId);
                }
            }
        }

        //When a user ask for creating a newRoom
        //A new Room is created assoiated with a new GUID,
        //Added to the rooms list and the Caller Client is added
        //To the players list of the new room.
        public async Task NewGroup()
        {
            string guid = NewGuidGeneration();

            Room r = new Room(guid);
            Rooms.Add(guid, r);

            await Clients.Caller.SendAsync("ReceiveNewGroup", guid);
            await Clients.Others.SendAsync("NewGroupCreated", guid);

            await JoinGroup(guid);
        }

        //When a user want to play in a Room
        //The room is searched in the Room list and, 
        //if it exists, add the user to the room.
        public async Task JoinGroup(string guid)
        {
            Room r = Rooms.GetValueOrDefault(guid);

            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);
            if (r != null && user != null)
            {
                bool result = r.AddPlayer(user);

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

        //When a user wants to see what append in a Room identified by the guid parameter
        //The room is searched in the Room list and, if it exists, add the user to this 
        //rooms' public and prevent every user in it.
        public async Task AskForSee(string guid)
        {
            Room r = Rooms.GetValueOrDefault(guid);
            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);

            if (r != null && user != null)
            {
                bool result = r.AddPublic(user);

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

        //A Function called when a user wants to quit a room
        //First we verify if he's in the database before removeing it. 
        public async Task QuitGame(string guid)
        {
            Room r = Rooms.GetValueOrDefault(guid);
            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);

            if (r != null && user != null)
            {
                await RemoveUserFromRoom(r, user);
            }
        }

        //Function called if a Room have to bee destroyed
        public async Task RemoveRoom(string guid)
        {
            Room r = Rooms.GetValueOrDefault(guid);

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
