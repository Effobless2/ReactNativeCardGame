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

        public bool AddPlayer(string idRoom, string idUser)
        {
            Console.WriteLine("CardGame.AddPlayer");
            try
            {
                Room room = GetRoom(idRoom);
                ApplicationUser user = GetUser(idUser);
                bool res = room.AddPlayer(user);
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
            return room.EmptyMe();

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

    }
}
