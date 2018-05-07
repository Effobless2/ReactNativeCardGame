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

        /// <summary>
        /// Adds the concerned ApplicationUser into the public of the concerned Room
        /// </summary>
        /// <param name="idRoom">Id of the Concerned Room</param>
        /// <param name="idUser">Id of the concerned ApplicationUser</param>
        /// <returns></returns>
        bool AddPublic(string idRoom, string idUser);

        /// <summary>
        /// Remove an Application user from a Room
        /// </summary>
        /// <param name="idRoom">id of the concerned Room</param>
        /// <param name="idUser">id of the concerned ApplicationUser</param>
        /// <returns>If the Room must be removed</returns>
        bool LeaveGame(string idRoom, string idUser);

        /// <summary>
        /// Removes the Room which has the idRoom.
        /// </summary>
        /// <param name="idRoom">Id of the concerned Room</param>
        /// <returns>The List of ApplicationUser which must be ejected of the Room</returns>
        List<ApplicationUser> RemoveRoom(string idRoom);

        /// <summary>
        /// Removes the User into the Application
        /// </summary>
        /// <param name="idUser">id of the concerned ApplicationUser</param>
        /// <returns>the concerned ApplicationUser</returns>
        ApplicationUser RemoveUser(string idUser);


    }
}
