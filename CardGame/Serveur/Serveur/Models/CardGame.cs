using Serveur.Models.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    /// <summary>
    /// CardGame Manager
    /// </summary>
    public class CardGame : IContractCardGame
    {
        public ConcurrentDictionary<string, Room> Rooms = new ConcurrentDictionary<string, Room>();
        public ConcurrentDictionary<string, ApplicationUser> Users = new ConcurrentDictionary<string, ApplicationUser>();
        public int nbCli = 0;

        /// <summary>
        /// Generates a new id for a new Room
        /// </summary>
        /// <returns>a New Guid</returns>
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

        public List<ApplicationUser> GetUsers()
        {
            return Users.Values.ToList();
        }

        public ApplicationUser GetUserWithId(string id)
        {
            Users.TryGetValue(id, out ApplicationUser user);
            if (user == null)
            {
                throw new UserIsUndefinedException();
            }
            return user;
        }

        public List<Room> GetRooms()
        {
            return Rooms.Values.ToList();
        }

        public Room GetRoomWithId(string id)
        {
            Rooms.TryGetValue(id, out Room room);
            if (room == null)
            {
                throw new RoomIsUndefinedException();
            }
            return room;
        }

        
        public ApplicationUser Connection(string newId)
        {
            ApplicationUser user = new ApplicationUser(newId, "Client "+ nbCli);
            nbCli++;
            Users.TryAdd(newId, user);
            return user;
        }

        
        public Room CreatingRoom()
        {
            string guid = NewGuidGeneration();
            Room room = new Room(guid);
            Rooms.TryAdd(guid, room);
            return room;
        }

        
        public bool AddingPlayer(string roomId, string userId)
        {
            Room room = GetRoomWithId(roomId);
            ApplicationUser user = GetUserWithId(userId);
            bool ready = room.AddPlayer(userId);
            user.AddRoomAsPlayer(roomId);
            return ready;
        }

        public void AddingPublic(string roomId, string userId)
        {
            Room room = GetRoomWithId(roomId);
            ApplicationUser user = GetUserWithId(userId);
            room.AddPublic(userId);
            user.AddRoomAsPublic(roomId);
        }

        public void RemovingPublic(string roomId, string userId)
        {
            Room room = GetRoomWithId(roomId);
            ApplicationUser user = GetUserWithId(userId);
            room.RemovePublic(userId);
            user.RemoveRoomAsPublic(roomId);
        }

        public bool RemovingPlayer(string roomId, string userId)
        {
            Room room = GetRoomWithId(roomId);
            ApplicationUser user = GetUserWithId(userId);
            bool toDestroy = room.RemovePlayer(userId);
            user.RemoveRoomAsPlayer(roomId);
            return toDestroy;
        }

        public List<string> RemovingRoom(string roomId)
        {
            Rooms.TryRemove(roomId, out Room room);

            if (room == null)
            {
                throw new RoomIsUndefinedException();
            }
            return room.GetAllUsers();
        }

        public List<string> ExtractingUsers(List<string> usersToExtract, string roomId)
        {
            List<string> usersConnected = new List<string>();
            foreach (string userId in usersToExtract)
            {
                try
                {
                    ApplicationUser currentUser = GetUserWithId(userId);
                    currentUser.RemoveRoomAsPlayer(roomId);
                    currentUser.RemoveRoomAsPublic(roomId);
                    usersConnected.Add(userId);
                }
                catch(UserIsUndefinedException)
                {

                }
            }
            return usersConnected;
        }

        public List<string> RemovingUser(string userId)
        {
            Users.TryRemove(userId, out ApplicationUser user);
            if (user == null)
            {
                throw new UserIsUndefinedException();
            }
            return user.GetAllRooms();
        }

        public bool UpdateRoom(string roomId, string userId)
        {
            try
            {
                Room room = GetRoomWithId(roomId);
                if (room.Players.Contains(userId))
                {
                    return room.RemovePlayer(userId);
                }
                else
                {
                    room.RemovePublic(userId);
                    return false;
                }
            }
            catch(NotInThisRoomException)
            {
                return false;
            }
            catch(RoomIsUndefinedException e)
            {
                throw e;
            }
        }
    }
}
