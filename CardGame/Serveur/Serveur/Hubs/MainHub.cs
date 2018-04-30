using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Serveur.Hubs
{
    public class MainHub : Hub
    {

        public static string Path = "/main";

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Passage dans Connection");
            await Clients.All.SendAsync("Connect", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine("Passage dans Déconnection");
            await Clients.All.SendAsync("Disconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }

        public async Task SendMessage(string message)
        {
            Console.WriteLine("Passage dans SendMessage");
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task NewGroup()
        {
            await Groups.AddAsync(Context.ConnectionId, "new");
            await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId + " joined the new Group");
        }

        public async Task sendMessageToTheGroup(string message)
        {
            await Clients.Group("new").SendAsync("ReceiveMessage", message);
        }
    }
}
