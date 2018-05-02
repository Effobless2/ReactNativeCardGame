using System;
using System.Collections.Generic;

namespace Serveur.Hubs
{
    public class Room
    {
        public string MyGuid { get; }
        public List<string> Players { get; set; }
        public List<string> Public { get; set; }

        public Room(string guid)
        {
            MyGuid = guid;

            Players = new List<string>();
            Public  = new List<string>();
        }

        public bool AddPlayer(string guid)
        {
            if (Players.Count == 2 || Public.Contains(guid))
            {
                return false;
            }

            Players.Add(guid);
            Public.Add(guid);
            return true;
        }

        public bool AddPublic(string guid)
        {
            if (Public.Contains(guid))
            {
                return false;
            }
            Public.Add(guid);
            return true;
        }
    }
}