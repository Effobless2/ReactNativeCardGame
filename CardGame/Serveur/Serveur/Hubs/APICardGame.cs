using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Hubs
{
    public class APICardGame : IContractCardGame
    {
        private CardGame cardGame = new CardGame();

        public List<ApplicationUser> GetUsers()
        {
            return cardGame.GetUsers();
        }

        public List<Room> GetRooms()
        {
            return cardGame.GetRooms();
        }

        
        public ApplicationUser Connection(string newId)
        {
            return cardGame.Connection(newId);
        }

        
        public Room CreatingRoom()
        {
            return cardGame.CreatingRoom();
        }

        
        public bool AddingPlayer(string roomId, string userId)
        {
            try
            {
                return cardGame.AddingPlayer(roomId, userId);
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
                cardGame.AddingPublic(roomId, userId);
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
                cardGame.RemovingPublic(roomId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool RemovingPlayer(string roomId, string userId)
        {
            try
            {
                return cardGame.RemovingPlayer(roomId, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> RemovingRoom(string roomId)
        {
            try
            {
                return cardGame.RemovingRoom(roomId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> ExtractingUsers(List<string>usersToExtract, string roomId)
        {
            return cardGame.ExtractingUsers(usersToExtract, roomId);
        }

        public List<string> RemovingUser(string userId)
        {
            try
            {
                return cardGame.RemovingUser(userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool UpdateRoom(string roomId, string userId)
        {
            try
            {
                return cardGame.UpdateRoom(roomId, userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
