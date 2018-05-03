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
            string UserName = Users.GetValueOrDefault(Context.ConnectionId).UserName;
            Users.Remove(Context.ConnectionId);
            foreach (Room r in Rooms.Values)
            {
                r.RemovePlayer(Context.ConnectionId);
            }

            await Clients.All.SendAsync("Disconnected", UserName);
            Console.WriteLine("Passage dans Déconnection");
            await base.OnDisconnectedAsync(ex);
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

            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);
            Console.WriteLine(user.UserId);
            if (user != null)
            {
                bool result = r.AddPlayer(user);

                if (result)
                {
                    await Clients.Caller.SendAsync("ReceiveNewGroup", guid);
                    await Clients.Others.SendAsync("NewGroupCreated", guid);
                }
            }
        }

        //When a user want to play in a Room
        //The room is searched in the Room list and, 
        //if it exists, add the user to the room.
        public async Task JoinGroup(string guid)
        {
            Console.WriteLine("Recoit demande");
            ApplicationUser user = Users.GetValueOrDefault(Context.ConnectionId);
            Room r = Rooms.GetValueOrDefault(guid);
            
            if (r != null && user != null)
            {
                bool result = r.AddPlayer(user);

                if (result)
                {
                    await Clients.Caller.SendAsync("JoinGroup", guid);

                    foreach (ApplicationUser u in r.Public.Values)
                    {
                        await Clients.Client(u.UserId).SendAsync("UserJoinedGroup", user.UserName, guid);
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
    }
}
