using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Serveur.Hubs
{
    public class MainHub : Hub
    {

        public static string Path = "/main";
        public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();

        //Generate a GUID which isn't already exist in the Room Dictionary.
        private string NewGuidGeneration()
        {
            string guid = "";
            while (guid == "" || Rooms.ContainsKey(guid)){
                guid = Guid.NewGuid().ToString();
            }
            return guid;
        }

        //When a user is connected
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Passage dans Connection");
            await Clients.All.SendAsync("Connect", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        //When a user is disconnected
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine("Passage dans Déconnection");
            await Clients.All.SendAsync("Disconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }

        //When a user ask for creating a newRoom
        //A new Room is created assoiated with a new GUID,
        //Added to the rooms list and the Caller Client is added
        //To the players list of the new room.
        public async Task NewGroup()
        {
            string guid = NewGuidGeneration();
            Console.WriteLine(guid);
            Console.WriteLine(Rooms.Count);

            Room r = new Room(guid);
            Rooms.Add(guid, r);
            Console.WriteLine(Rooms.Count);

            bool result = r.AddPlayer(Context.ConnectionId);
            
            if (result)
            {
                await Clients.Caller.SendAsync("ReceiveNewGroup", guid);
                await Clients.Others.SendAsync("NewGroupCreated", guid);
            }
        }

        //When a user want to play in a Room
        //The room is searched in the Room list and, 
        //if it exists, add the user to the room.
        public async Task JoinGroup(string guid)
        {
            Console.WriteLine("Recoit demande");
            Room r = Rooms.GetValueOrDefault(guid);
            foreach (string ro in Rooms.Keys)
            {
                Console.WriteLine(ro);

            }
            if (r != null)
            {
                bool result = r.AddPlayer(Context.ConnectionId);

                if (result)
                {
                    await Clients.Caller.SendAsync("JoinGroup", guid);

                    foreach (string id in r.Public)
                    {
                        await Clients.Client(id).SendAsync("UserJoinedGroup", Context.ConnectionId, guid);
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

            if (r != null)
            {
                bool result = r.AddPublic(Context.ConnectionId);

                if (result)
                {
                    await Clients.Caller.SendAsync("JoinGroup", guid);
                    foreach(string id in r.Players)
                    {
                        await Clients.Client(id).SendAsync("UserSee", Context.ConnectionId, guid);
                    }
                }
            }
        }
    }
}
