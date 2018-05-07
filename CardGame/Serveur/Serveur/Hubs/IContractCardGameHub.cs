using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Hubs
{
    interface IContractCardGameHub
    {
        /// <summary>
        /// Called by Clients. Creates a new Room and prevent Clients
        /// </summary>
        /// <returns></returns>
        Task CreatingRoom();

        /// <summary>
        /// Called by Clients. Adds a new ApplicationUser and prevent Clients.
        /// </summary>
        /// <param name="idUser">Token of the concerned Client</param>
        /// <returns></returns>
        Task AddingUser(string idUser);

        /// <summary>
        /// Called by Clients. Adds the current Client into the Called Room as Player.
        /// </summary>
        /// <param name="idRoom">Id of the concerned Room</param>
        /// <returns></returns>
        Task AddingPlayer(string idRoom);

        /// <summary>
        /// Called by Clients. Adds the current Client into the Called Room as Public.
        /// </summary>
        /// <param name="idRoom">Id of the concerned Room</param>
        /// <returns></returns>
        Task AddingPublic(string idRoom);

        /// <summary>
        /// Called by Clients. Remove the current Client from the Called Room.
        /// </summary>
        /// <param name="idRoom">Id of the concerned Room</param>
        /// <returns></returns>
        Task LeavingGame(string idRoom);

        /// <summary>
        /// Called by Clients. Removes the concerned Room from the Server.
        /// </summary>
        /// <param name="idRoom">Id of the concerned Room</param>
        /// <returns></returns>
        Task RemovingRoom(string idRoom);

        /// <summary>
        /// Called by Clients. Removes the concerned ApplicationUser from the Server.
        /// </summary>
        /// <param name="idUser">Id of the concerned ApplicationUser</param>
        /// <returns></returns>
        Task RemovingUser(string idUser);
    }
}
