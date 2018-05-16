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

        private ConcurrentDictionary<string, Room> rooms;

        public ApplicationUser(string id, string name)
        {
            UserId = id;
            UserName = name;

            rooms = new ConcurrentDictionary<string, Room>();
        }

        /// <summary>
        /// Adds a Room into the rooms's list
        /// </summary>
        /// <param name="room">The Room wich </param>
        /// <returns>Confirmation of the adding</returns>
        public bool AddRoom(Room room)
        {
            return rooms.TryAdd(room.RoomId, room);
        }

        /// <summary>
        /// Remove a room from the List of rooms
        /// </summary>
        /// <param name="room">the room which will be removed</param>
        internal void RemoveRoom(Room room)
        {
            rooms.TryRemove(room.RoomId, out Room value);
        }
    }
}
