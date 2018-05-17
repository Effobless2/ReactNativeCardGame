using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Hubs
{
    public class APICardGame
    {
        private CardGame cardGame = new CardGame();

        internal List<ApplicationUser> GetUsers()
        {
            return cardGame.GetUsers();
        }

        internal List<Room> GetRooms()
        {
            return cardGame.GetRooms();
        }

        /// <summary>
        /// Crée un nouvel utilisateur dans le gestionnaire de jeu
        /// </summary>
        /// <param name="newId">id du nouvel utilisateur</param>
        /// <returns>l'instance du nouvel utilisateur</returns>
        public ApplicationUser Connection(string newId)
        {
            return cardGame.Connection(newId);
        }

        /// <summary>
        /// Crée une nouvelle Room dans le gestionnaire de jeu
        /// </summary>
        /// <returns>l'instance de la nouvelle Room</returns>
        internal Room CreatingRoom()
        {
            return cardGame.CreatingRoom();
        }

        /// <summary>
        /// Ajoute un ApplicationUser à une Room en tant que Joueur 
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        /// <returns>true si la partie est prête à commencer</returns>
        internal bool AddingPlayer(string roomId, string userId)
        {
            try
            {
                return cardGame.AddingPlayer(roomId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        internal void AddingPublic(string roomId, string userId)
        {
            try
            {
                cardGame.AddingPublic(roomId, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal void RemovingPublic(string roomId, string userId)
        {
            try
            {
                cardGame.RemovingPublic(roomId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        internal bool RemovingPlayer(string roomId, string userId)
        {
            try
            {
                return cardGame.RemovingPlayer(roomId, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal List<string> RemovingRoom(string roomId)
        {
            try
            {
                return cardGame.RemovingRoom(roomId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal List<string> ExtractingUsers(List<string>usersToExtract, string roomId)
        {
            return cardGame.ExtractingUsers(usersToExtract, roomId);
        }

        internal List<string> RemovingUser(string userId)
        {
            try
            {
                return cardGame.RemovingUser(userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool UpdateRoom(string roomId, string userId)
        {
            try
            {
                return cardGame.UpdateRoom(roomId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
