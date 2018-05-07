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

        Room NewRoom();

        ApplicationUser AddUser(string user);

        bool AddPlayer(string idRoom, string idUser);

        bool AddPublic(string idRoom, string idUser);

        bool LeaveGame(string idRoom, string idUser);

        List<ApplicationUser> RemoveRoom(string idRoom);

        ApplicationUser RemoveUser(string idUser);


    }
}
