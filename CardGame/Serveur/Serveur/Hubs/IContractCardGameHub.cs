using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Hubs
{
    interface IContractCardGameHub
    {
        Task CreatingRoom();

        Task AddingUser(string idUser);

        Task AddingPlayer(string idRoom);

        Task AddingPublic(string idRoom);

        Task LeavingGame(string idRoom);

        Task RemovingRoom(string idRoom);

        Task RemovingUser(string idUser);
    }
}
