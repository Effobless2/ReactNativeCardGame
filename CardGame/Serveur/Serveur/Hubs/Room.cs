using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serveur.Hubs
{
    public class Room
    {
        public string MyGuid { get; }
        public List<string> Players { get; }
        public Dictionary<string, ApplicationUser> Public { get; }

        public Room(string guid)
        {
            MyGuid = guid;

            Players = new List<string>();
            Public  = new Dictionary<string, ApplicationUser>();
        }

        public bool AddPlayer(ApplicationUser newUser)
        {
            if (Players.Count == 2 || Public.Keys.Contains(newUser.MyGuid))
            {
                return false;
            }

            Players.Add(newUser.MyGuid);
            Public.Add(newUser.MyGuid, newUser);
            return true;
        }

        public bool AddPublic(ApplicationUser newUser)
        {
            if (Public.Keys.Contains(newUser.MyGuid))
            {
                return false;
            }
            Public.Add(newUser.MyGuid, newUser);
            return true;
        }

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