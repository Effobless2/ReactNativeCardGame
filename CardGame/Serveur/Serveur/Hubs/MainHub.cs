using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Serveur.Hubs
{
    public class MainHub : Hub
    {

        public static string Path = "/main";


        //When some user is connected
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
        public async Task NewGroup()
        {
            string guid = System.Guid.NewGuid().ToString();
            
            await Groups.AddAsync(Context.ConnectionId, guid);
            await Groups.AddAsync(Context.ConnectionId, guid + "-Game");
            await Clients.Caller.SendAsync("ReceiveNewGroup", guid + "-Game");
            await Clients.Others.SendAsync("NewGroupCreated", guid);
        }

        public async Task sendMessageToTheGroup(string message)
        {
            await Clients.Group("new").SendAsync("ReceiveMessage", message);
        }

        public async Task JoinGroup(string guid)
        {
            await Groups.AddAsync(Context.ConnectionId, guid + "-Game");
            await Groups.AddAsync(Context.ConnectionId, guid);
            await Clients.Caller.SendAsync("JoinGroup", guid);
            await Clients.Group(guid).SendAsync("UserJoinedGroup", Context.ConnectionId, guid);

        }

        public async Task AskForSee(string guid)
        {
            await Groups.AddAsync(Context.ConnectionId, guid);
            await Clients.Group(guid).SendAsync("UserSee", Context.ConnectionId, guid);
        }
    }
}
