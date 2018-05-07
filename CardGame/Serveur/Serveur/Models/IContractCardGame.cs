using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    /// <summary>
    /// Interface which contains Methods used by the CardGame Manager
    /// and which must implements the class which release the bridge between the Client
    /// and the CardGame Manager.
    /// </summary>
    public interface IContractCardGame
    {

        /// <summary>
        /// Search the Room which has the roomId as Id
        /// </summary>
        /// <param name="roomId">the id of the wanted Room</param>
        /// <returns>Room with the id roomId or null</returns>
        Room GetRoom(string roomId);

        /// <summary>
        /// Search the ApplicationUser which has the roomId as Id.
        /// </summary>
        /// <param name="userId">The id of the wanted ApplicationUser</param>
        /// <returns>ApplicationUser with the id userId or null</returns>
        ApplicationUser GetUser(string userId);

        /// <summary>
        /// Creates a New Room
        /// </summary>
        /// <returns>The new Created Room</returns>
        Room NewRoom();

        /// <summary>
        /// Add a new Connected ApplicationUser in the Serveur
        /// </summary>
        /// <param name="user">Id of the new ApplicationUser</param>
        /// <returns>The new Created ApplicationUser</returns>
        ApplicationUser AddUser(string user);

        /// <summary>
        /// Add the Player with the id idUser into the Room with id idRoom
        /// </summary>
        /// <param name="idRoom">id of the concerned Room</param>
        /// <param name="idUser">id of the concerned User</param>
        /// <returns></returns>
        bool AddPlayer(string idRoom, string idUser);

        bool AddPublic(string idRoom, string idUser);

        bool LeaveGame(string idRoom, string idUser);

        List<ApplicationUser> RemoveRoom(string idRoom);

        ApplicationUser RemoveUser(string idUser);


    }
}
