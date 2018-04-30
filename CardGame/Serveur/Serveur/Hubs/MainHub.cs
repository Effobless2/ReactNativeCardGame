using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Serveur.Hubs
{
    public class MainHub : Hub
    {

        public static string Path = "/mainner";

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Passage dans Connection");
            await Clients.All.SendAsync("Connect", Context.User.Identity.Name, "joined");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine("Passage dans Déconnection");
            await Clients.All.SendAsync("Disconnected", Context.User.Identity.Name, "left");
        }

        public async Task Send(string message)
        {
            Console.WriteLine("Passage dans Send");
            await Clients.All.SendAsync("Action", message);
        }
    }
}
