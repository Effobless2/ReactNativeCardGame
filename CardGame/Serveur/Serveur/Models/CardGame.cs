using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    public class CardGame : IContractCardGame
    {
        public ConcurrentDictionary<string, Room> Rooms = new ConcurrentDictionary<string, Room>();
        public ConcurrentDictionary<string, ApplicationUser> Users = new ConcurrentDictionary<string, ApplicationUser>();
        public int nbCli = 0;

        public bool AddPlayer(string idRoom, string idUser)
        {
            Room room = GetRoom(idRoom);
            ApplicationUser user = GetUser(idUser);
            return room.AddPlayer(user);
        }

        public bool AddPublic(string idRoom, string idUser)
        {
            Room room = GetRoom(idRoom);
            ApplicationUser user = GetUser(idUser);
            return room.AddPublic(user);
        }

        public ApplicationUser AddUser(string idUser)
        {
            ApplicationUser user = new ApplicationUser(idUser, "User "+ nbCli);
            nbCli++;

            Users.TryAdd(idUser, user);

            return GetUser(idUser);

        }

        public Room GetRoom(string roomId)
        {
            Rooms.TryGetValue(roomId, out Room res);
            return res;
        }

        public ApplicationUser GetUser(string userId)
        {
            Users.TryGetValue(userId, out ApplicationUser res);
            return res;
        }

        public Room NewRoom()
        {
            Room room = new Room(NewGuidGeneration());
            Rooms.TryAdd(room.RoomId, room);
            return room;

        }

        public bool LeaveGame(string idRoom, string idUser)
        {
            Room room = GetRoom(idRoom);
            ApplicationUser user = GetUser(idUser);
            return room.RemoveUser(user);
        }

        public List<ApplicationUser> RemoveRoom(string idRoom)
        {
            Rooms.TryRemove(idRoom, out Room room);
            return room.EmptyMe();

        }

        public ApplicationUser RemoveUser(string idUser)
        {
            Users.TryRemove(idUser, out ApplicationUser user);
            return user;
        }

        private string NewGuidGeneration()
        {
            string guid = "";
            lock (Rooms)
            {
                while (guid == "" || Rooms.ContainsKey(guid))
                {
                    guid = Guid.NewGuid().ToString();
                }
            }

            return guid;
        }

    }
}
