using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serveur.Models
{
    public class Room
    {
        public string RoomId { get; }
        public List<string> Players { get; }
        public Dictionary<string, ApplicationUser> Public { get; }

        public Room(string guid)
        {
            RoomId = guid;

            Players = new List<string>();
            Public  = new Dictionary<string, ApplicationUser>();
        }

        //Add a Player in the Room. First, we had the ApplicationUser into
        //the Public Dictionary with its UserId for Key and add its UserId
        //into the Players List.
        public bool AddPlayer(ApplicationUser newUser)
        {
            if (Players.Count == 2 || Public.Keys.Contains(newUser.UserId))
            {
                return false;
            }

            Players.Add(newUser.UserId);
            Public.Add(newUser.UserId, newUser);
            return true;
        }

        //Add a User into the Public with its UserId for Key.
        public bool AddPublic(ApplicationUser newUser)
        {
            if (Public.Keys.Contains(newUser.UserId))
            {
                return false;
            }
            Public.Add(newUser.UserId, newUser);
            return true;
        }

        //Remove a User from the Room.
        internal void RemovePlayer(string Id)
        {
            if (Public.Keys.Contains(Id))
            {
                Public.Remove(Id);
            }
            if (Players.Contains(Id))
            {
                Players.Remove(Id);
            }
        }
    }
}