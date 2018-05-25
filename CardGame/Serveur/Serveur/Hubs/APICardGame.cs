using Serveur.Models;
using Serveur.Models.BatailleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Hubs
{
    public class APICardGame : IContractCardGame
    {
        public CardGame cardGame = new CardGame();

        public List<ApplicationUser> GetUsers()
        {
            return cardGame.GetUsers();
        }

        public List<Room> GetRooms()
        {
            return cardGame.GetRooms();
        }

        public Room GetRoomWithId(string roomId)
        {
            return cardGame.GetRoomWithId(roomId);
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
            return cardGame.AddingPlayer(roomId, userId);
        }

        public void AddingPublic(string roomId, string userId)
        {
            cardGame.AddingPublic(roomId, userId);
        }

        public void RemovingPublic(string roomId, string userId)
        {
            cardGame.RemovingPublic(roomId, userId);
        }

        public bool RemovingPlayer(string roomId, string userId)
        {
            return cardGame.RemovingPlayer(roomId, userId);
        }

        public List<string> RemovingRoom(string roomId)
        {
            return cardGame.RemovingRoom(roomId);
        }

        public List<string> RemovingUser(string userId)
        {
            return cardGame.RemovingUser(userId);
        }

        public bool UpdateRoom(string roomId, string userId)
        {
            return cardGame.UpdateRoom(roomId, userId);
        }

        public List<Player> BatailleBegin(string roomId)
        {
            return cardGame.BatailleBegin(roomId);
        }

        public bool CardPlayed(string roomId, string userId, int cardIndex)
        {
            return cardGame.CardPlayed(roomId, userId, cardIndex);
        }

        public List<string> GetAllUsers(string roomId)
        {
            return cardGame.GetAllUsers(roomId);
        }


        public Player FinalizeTour(string roomId)
        {
            return cardGame.FinalizeTour(roomId);
        }
    }
}
