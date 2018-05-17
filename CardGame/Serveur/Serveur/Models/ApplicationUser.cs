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

        public List<string> RoomsAsPlayer;

        public List<string> RoomsAsPublic;

        public ApplicationUser(string id, string name)
        {
            UserId = id;
            UserName = name;

            RoomsAsPlayer = new List<string>();
            RoomsAsPublic = new List<string>();
        }

        public void AddRoomAsPlayer(string idRoom)
        {
            RoomsAsPlayer.Add(idRoom);
        }

        public void AddRoomAsPublic(string idRoom)
        {
            RoomsAsPublic.Add(idRoom);
        }

        public void RemoveRoomAsPublic(string idRoom)
        {
            RoomsAsPublic.Remove(idRoom);
        }

        public void RemoveRoomAsPlayer(string roomId)
        {
            RoomsAsPlayer.Remove(roomId);
        }

        public List<string> GetAllRooms()
        {
            return RoomsAsPlayer.Concat(RoomsAsPublic).ToList();
        }
    }
}
