using Serveur.Models;
using Serveur.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    /// <summary>
    /// The Representation of a GameRoom
    /// </summary>
    public class Room
    {
        public string RoomId { get; }
        public int MaxOfPlayers { get; set; }
        public List<string> Players { get; }
        public List<string> Public { get; }


        public Room(string guid)
        {
            RoomId = guid;

            Players = new List<string>();
            Public  = new List<string>();
            MaxOfPlayers = 2;
        }


        /// <summary>
        /// Add a Player in the Room if the number of players is less than the number
        /// of needed Players and if he's not already in the list of players.
        /// </summary>
        /// <param name="newUser">The User which we want to add in the list of Players</param>
        /// <returns>If the number of players is enough to begin the Party</returns>
        public bool AddPlayer(string newUser)
        {
            if (Players.Count == MaxOfPlayers)
            {
                throw new FulfillRoomException();
            }
            if (Players.Contains(newUser))
            {
                throw new AlreadyInRoomException();
            }
            if (Public.Contains(newUser))
            {
                Public.Remove(newUser);
            }

            Players.Add(newUser);

            return isComplete();
        }

        /// <summary>
        /// Add a User into the Public with its UserId for Key.
        /// </summary>
        /// <param name="newUser">The newUser we want to add in the public</param>
        /// <returns></returns>
        public void AddPublic(string newUser)
        {
            if (Public.Contains(newUser))
            {
                throw new AlreadyInRoomException();
            }
            Public.Add(newUser);
        }

        public void RemovePublic(string user)
        {
            if (!Public.Contains(user))
            {
                throw new NotInThisRoomException();
            }
            Public.Remove(user);
        }

        public bool RemovePlayer(string user)
        {
            if (!Players.Contains(user))
            {
                throw new NotInThisRoomException();
            }
            bool before = isComplete();
            Players.Remove(user);
            return (before && !isComplete()) || (Players.Count == 0);
        }

        /// <summary>
        /// Remove Every Users in this Room.
        /// </summary>
        /// <returns>List of Users which must be prevent of the removing of the Room</returns>
        internal List<string> GetAllUsers()
        {
            return Players.Concat(Public).ToList();
        }

        /// <summary>
        /// Ask if the number of players is good 
        /// for begining the game.
        /// </summary>
        /// <returns>boolean</returns>
        public bool isComplete()
        {
            return Players.Count == MaxOfPlayers;
        }
    }
}