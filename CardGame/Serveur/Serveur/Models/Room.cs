using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    public class Room
    {
        public string RoomId { get; }
        public int MaxOfPlayers { get; }
        public List<string> Players { get; }
        public Dictionary<string, ApplicationUser> Public { get; }

        public Room(string guid)
        {
            RoomId = guid;

            Players = new List<string>();
            Public  = new Dictionary<string, ApplicationUser>();
            MaxOfPlayers = 2;
        }


        /// <summary>
        /// Add a Player in the Room if the number of players is less than the number
        /// of needed Players and if he's not already in the list of players.
        /// </summary>
        /// <param name="newUser">The User which we want to add in the list of Players</param>
        /// <returns>If the number of players is enough to begin the Party</returns>
        public bool AddPlayer(ApplicationUser newUser)
        {
            if (Players.Count == MaxOfPlayers)
            {
                throw new FulfillRoomException();
            }
            else if (Public.Keys.Contains(newUser.UserId))
            {
                if (Players.Contains(newUser.UserId))
                {
                    throw new AlreadyInRoomException();
                }
                else
                {
                    Players.Add(newUser.UserId);
                }
            }
            else
            {
                Players.Add(newUser.UserId);
                Public.Add(newUser.UserId, newUser);
            }

            return isComplete();
        }

        /// <summary>
        /// Add a User into the Public with its UserId for Key.
        /// </summary>
        /// <param name="newUser">The newUser we want to add in the public</param>
        /// <returns>boolean</returns>
        public bool AddPublic(ApplicationUser newUser)
        {
            if (Public.Keys.Contains(newUser.UserId))
            {
                throw new AlreadyInRoomException();
            }
            Public.Add(newUser.UserId, newUser);
            return true;
        }

        /// <summary>
        /// Remove A User From the current Room.
        /// </summary>
        /// <param name="user">ApplicationUser which we want to remove</param>
        /// <returns>If the Room must be deleted</returns>
        public bool RemoveUser(ApplicationUser user)
        {
            if (!Public.Keys.Contains(user.UserId))
            {
                throw new NotInThisRoomException();
            }
            bool before = isComplete();
            Public.Remove(user.UserId);
            user.RemoveRoom(this);
            if (Players.Contains(user.UserId))
            {
                Players.Remove(user.UserId);
                
            }
            bool after = isComplete();
            return (before && !after) || Players.Count == 0;
        }

        /// <summary>
        /// Remove Every Users in this Room.
        /// </summary>
        /// <returns>List of Users which must be prevent of the removing of the Room</returns>
        internal List<ApplicationUser> EmptyMe()
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            foreach (ApplicationUser user in Public.Values)
            {
                user.RemoveRoom(this);
                result.Add(user);
            }

            return result;
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