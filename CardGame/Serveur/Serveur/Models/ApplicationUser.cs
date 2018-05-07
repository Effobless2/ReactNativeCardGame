using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    public class ApplicationUser
    {
        public string UserId { get; }
        public string UserName { get; set; }

        public ConcurrentDictionary<string, Room> rooms { get; }

        public ApplicationUser(string id, string name)
        {
            UserId = id;
            UserName = name;

            rooms = new ConcurrentDictionary<string, Room>();
        }

        public bool AddRoom(Room room)
        {
            return rooms.TryAdd(room.RoomId, room);
        }

        public List<Room> WhereIAmPlayer()
        {
            List<Room> result = new List<Room>();
            foreach (Room room in rooms.Values)
            {
                if (room.Players.Contains(UserId))
                {
                    result.Add(room);
                }
            }
            return result;
        }

        internal void RemoveRoom(Room room)
        {
            rooms.TryRemove(room.RoomId, out Room value);
        }
    }
}
