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
    public class CardGame /*: IContractCardGame*/
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

        internal List<ApplicationUser> GetUsers()
        {
            return Users.Values.ToList();
        }

        internal ApplicationUser GetUserWithId(string id)
        {
            Users.TryGetValue(id, out ApplicationUser user);
            if (user == null)
            {
                throw new UserIsUndefinedException();
            }
            return user;
        }

        internal List<Room> GetRooms()
        {
            return Rooms.Values.ToList();
        }

        internal Room GetRoomWithId(string id)
        {
            Rooms.TryGetValue(id, out Room room);
            if (room == null)
            {
                throw new RoomIsUndefinedException();
            }
            return room;
        }

        /// <summary>
        /// Crée un nouvel utilisateur dans le Dictionnaire des ApplicationUser
        /// </summary>
        /// <param name="newId">id du nouvel utilisateur</param>
        /// <returns>l'instance du nouvel utilisateur</returns>
        public ApplicationUser Connection(string newId)
        {
            ApplicationUser user = new ApplicationUser(newId, "Client "+ nbCli);
            Users.TryAdd(newId, user);
            return user;
        }

        /// <summary>
        /// Crée une nouvelle Room dans le Dictionnaire des Room
        /// </summary>
        /// <returns>l'instance de la nouvelle Room</returns>
        public Room CreatingRoom()
        {
            string guid = NewGuidGeneration();
            Room room = new Room(guid);
            Rooms.TryAdd(guid, room);
            return room;
        }

        /// <summary>
        /// Ajoute un ApplicationUser à une Room en tant que Joueur 
        /// </summary>
        /// <param name="roomId">Id de la room concernée</param>
        /// <param name="userId">Id de l'Applicationuser concerné</param>
        /// <returns>true si la partie est prête à commencer</returns>
        public bool AddingPlayer(string roomId, string userId)
        {
            try
            {
                Room room = GetRoomWithId(roomId);
                ApplicationUser user = GetUserWithId(userId);
                bool ready = room.AddPlayer(userId);
                user.AddRoomAsPlayer(roomId);
                return ready;

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void AddingPublic(string roomId, string userId)
        {
            try
            {
                Room room = GetRoomWithId(roomId);
                ApplicationUser user = GetUserWithId(userId);
                room.AddPublic(userId);
                user.AddRoomAsPublic(roomId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RemovingPublic(string roomId, string userId)
        {
            try
            {
                Room room = GetRoomWithId(roomId);
                ApplicationUser user = GetUserWithId(userId);
                room.RemovePublic(userId);
                user.RemoveRoomAsPublic(roomId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal bool RemovingPlayer(string roomId, string userId)
        {
            try
            {
                Room room = GetRoomWithId(roomId);
                ApplicationUser user = GetUserWithId(userId);
                bool toDestroy = room.RemovePlayer(userId);
                user.RemoveRoomAsPlayer(roomId);
                return toDestroy;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal List<string> RemovingRoom(string roomId)
        {
            try
            {
                Rooms.TryRemove(roomId, out Room room);

                if (room == null)
                {
                    throw new RoomIsUndefinedException();
                }

                return room.GetAllUsers();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal List<string> ExtractingUsers(List<string> usersToExtract, string roomId)
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
                catch(UserIsUndefinedException e)
                {

                }
            }
            return usersConnected;
        }

        internal List<string> RemovingUser(string userId)
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
                room.RemovePlayer(userId);
                return true;
            }
            catch(NotInThisRoomException e)
            {
                return false;
            }
            catch(RoomIsUndefinedException e)
            {
                throw e;
            }
        }




        /*






        public bool AddPlayer(string idRoom, string idUser)
        {
            Console.WriteLine("CardGame.AddPlayer");
            try
            {
                Room room = GetRoom(idRoom);
                ApplicationUser user = GetUser(idUser);
                bool res = room.AddPlayer(user);
                user.AddRoom(room);
                Console.WriteLine(res);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("CardGame.AddPlayer(ex)");
                throw e;
            }  
        }
        
        public bool AddPublic(string idRoom, string idUser)
        {
            try
            {
                Room room = GetRoom(idRoom);
                ApplicationUser user = GetUser(idUser);
                user.AddRoom(room);
                return room.AddPublic(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public ApplicationUser AddUser(string idUser)
        {
            try
            {
                ApplicationUser user = new ApplicationUser(idUser, "User " + nbCli);
                nbCli++;

                Users.TryAdd(idUser, user);

                return GetUser(idUser);
            }
            catch (UserIsUndefinedException e)
            {
                throw e;
            }
        }
        
        public Room GetRoom(string roomId)
        {
            Rooms.TryGetValue(roomId, out Room res);
            if (res == null)
            {
                throw new RoomIsUndefinedException();
            }
            return res;
        }
        
        public ApplicationUser GetUser(string userId)
        {
            Users.TryGetValue(userId, out ApplicationUser res);

            if (res == null)
            {
                throw new UserIsUndefinedException();
            }
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
            try
            {
                Room room = GetRoom(idRoom);
                ApplicationUser user = GetUser(idUser);
                return room.RemoveUser(user);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        
        public List<ApplicationUser> RemoveRoom(string idRoom)
        {
            Rooms.TryRemove(idRoom, out Room room);
            if (room == null)
            {
                throw new RoomIsUndefinedException();
            }
            List<string> UserIds = room.EmptyMe();
            List < ApplicationUser > listUsers = new List<ApplicationUser>();
            foreach (string user in UserIds)
            {
                ApplicationUser u=GetUser(user);
                if (u != null)
                {
                    listUsers.Add(u);
                }
            }
            return listUsers;

        }
        
        public ApplicationUser RemoveUser(string idUser)
        {
            Users.TryRemove(idUser, out ApplicationUser user);
            if (user == null)
            {
                throw new UserIsUndefinedException();
            }
            return user;
        }

        */

    }
}
