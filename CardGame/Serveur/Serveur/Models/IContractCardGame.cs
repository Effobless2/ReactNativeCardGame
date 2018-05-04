using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    public interface IContractCardGame
    {
        Room GetRoom(string roomId);

        ApplicationUser GetUser(string userId);

        Task<bool> NewGroup();

        Task<bool> JoinGroup(string guid);

        Task<bool> AskForSee(string guid);

        Task<bool> QuitGame(string guid);

        Task<bool> RemoveRoom(string guid);


    }
}
