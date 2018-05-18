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
        /// Crée un nouvel utilisateur dans le gestionnaire de jeu
        /// </summary>
        /// <param name="newId">Token (et identifiant) du nouvel utilisateur et qui permettra de l'identifier</param>
        /// <returns>Instance de ApplicationUser correspondant au nouvel Utilisateur</returns>
        ApplicationUser Connection(string newId);

        /// <summary>
        /// Crée une nouvelle Room dans le gestionnaire de jeu
        /// </summary>
        /// <returns></returns>
        Room CreatingRoom();

        /// <summary>
        /// Ajoute un ApplicationUser à une Room en tant que Joueur 
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        /// <returns>true si la partie est prête à commencer</returns>
        bool AddingPlayer(string roomId, string userId);

        /// <summary>
        /// Ajoute un ApplicationUser à une Room en tant que Public 
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        /// <returns>true si la partie est prête à commencer</returns>
        void AddingPublic(string roomId, string userId);

        /// <summary>
        /// Supprime un ApplicationUser de la liste des membres du Public d'une Room
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        void RemovingPublic(string roomId, string userId);

        /// <summary>
        /// Supprime un ApplicationUser de la liste des membres des joueurs d'une Room
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        /// <returns>true si la partie doit être supprimée</returns>
        bool RemovingPlayer(string roomId, string userId);

        /// <summary>
        /// Supprime une room du gestionnaire de Jeu
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <returns>La liste des identifiants des joueurs devant être éjectés de la Room</returns>
        List<string> RemovingRoom(string roomId);

        /// <summary>
        /// Supprime un utilisateur du gestionnaire de jeu
        /// </summary>
        /// <param name="userId">Id de l'ApplicationUser concerné</param>
        /// <returns>La liste de l'ensemble des parties auquel il a fait partie</returns>
        List<string> RemovingUser(string userId);

        /// <summary>
        /// Permet d'obtenir une liste de l'ensemble des ApplicationUser présents
        /// afin de les envoyer aux nouveaux arrivants
        /// </summary>
        /// <returns></returns>
        List<ApplicationUser> GetUsers();

        /// <summary>
        /// Permet d'obtenir une liste de l'ensemble des Room présentes
        /// afin de les envoyer aux nouveaux arrivants
        /// </summary>
        /// <returns></returns>
        List<Room> GetRooms();

        /// <summary>
        /// Permet de mettre à jour d'une Room suite à la déconnexion d'un ApplicationUser.
        /// </summary>
        /// <param name="roomId">Id de la Room concernée</param>
        /// <param name="userId">Id de l'ApplicationUser à supprimer de la Room</param>
        /// <returns>true si la room doit être supprimée</returns>
        bool UpdateRoom(string roomId, string userId);
    }
}
