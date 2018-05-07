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
        /// Add a Player in the Room. First, we had the ApplicationUser into
        /// the Public Dictionary with its UserId for Key and add its UserId
        /// into the Players List.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>boolean</returns>
        public bool AddPlayer(ApplicationUser newUser)
        {
            if (Players.Count == MaxOfPlayers || Public.Keys.Contains(newUser.UserId))
            {
                return false;
            }

            Players.Add(newUser.UserId);
            Public.Add(newUser.UserId, newUser);
            return true;
        }

        /// <summary>
        /// Add a User into the Public with its UserId for Key.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>boolean</returns>
        public bool AddPublic(ApplicationUser newUser)
        {
            if (Public.Keys.Contains(newUser.UserId))
            {
                return false;
            }
            Public.Add(newUser.UserId, newUser);
            return true;
        }

        /// <summary>
        /// Remove a User from the Room.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>boolean</returns>
        public bool RemoveUser(ApplicationUser user)
        {
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