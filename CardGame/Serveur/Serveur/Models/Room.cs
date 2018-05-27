using Serveur.Models;
using Serveur.Models.BatailleModels;
using Serveur.Models.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models
{
    /// <summary>
    /// The Representation of a GameRoom
    /// </summary>
    public class Room
    {
        public string RoomId { get; }
        public int MaxOfPlayers { get; set; }
        private ConcurrentDictionary<string,Player> _players { get; }
        public List<string> PublicMembers { get; }
        public List<Card> EnJeu;


        public Room(string guid)
        {
            RoomId = guid;

            _players = new ConcurrentDictionary<string, Player>();
            PublicMembers  = new List<string>();
            EnJeu = new List<Card>();
            MaxOfPlayers = 2;
        }


        /// <summary>
        /// Add a Player in the Room if the number of players is less than the number
        /// of needed Players and if he's not already in the list of players.
        /// </summary>
        /// <param name="newUser">The User which we want to add in the list of Players</param>
        /// <returns>If the number of players is enough to begin the Party</returns>
        public bool AddPlayer(ApplicationUser newUser)
        {
            if (_players.Count == MaxOfPlayers)
            {
                throw new FulfillRoomException();
            }
            if (_players.Keys.Contains(newUser.UserId))
            {
                throw new AlreadyInRoomException();
            }
            if (PublicMembers.Contains(newUser.UserId))
            {
                PublicMembers.Remove(newUser.UserId);
            }

            _players.TryAdd(newUser.UserId, new Player(newUser));

            return isComplete();
        }

        /// <summary>
        /// Add a User into the Public with its UserId for Key.
        /// </summary>
        /// <param name="newUser">The newUser we want to add in the public</param>
        /// <returns></returns>
        public void AddPublic(string newUser)
        {
            if (PublicMembers.Contains(newUser) || _players.Keys.Contains(newUser))
            {
                throw new AlreadyInRoomException();
            }
            PublicMembers.Add(newUser);
        }

        public void RemovePublic(string user)
        {
            if (!PublicMembers.Contains(user))
            {
                throw new NotInThisRoomException();
            }
            PublicMembers.Remove(user);
        }

        public bool RemovePlayer(string user)
        {
            if (!_players.Keys.Contains(user))
            {
                throw new NotInThisRoomException();
            }
            bool before = isComplete();
            _players.TryRemove(user, out Player suppressed);
            return (before && !isComplete()) || (_players.Count == 0);
        }

        /// <summary>
        /// Remove Every Users in this Room.
        /// </summary>
        /// <returns>List of Users which must be prevent of the removing of the Room</returns>
        internal List<string> GetAllUsers()
        {
            return _players.Keys.Concat(PublicMembers).ToList();
        }

        /// <summary>
        /// Ask if the number of players is good 
        /// for begining the game.
        /// </summary>
        /// <returns>boolean</returns>
        public bool isComplete()
        {
            return _players.Count == MaxOfPlayers;
        }

        public List<Player> BatailleBegin()
        {
            List<Card> Cards = new List<Card>();
            string[] colors = { "C", "D", "H", "S" };
            foreach(string color in colors)
            {
                for(int i = 1; i < 14; i++)
                {
                    Cards.Add(new Card(color, i));
                }
            }
            var random = new System.Random();
            Cards.Sort((x, y) => random.Next(-1, 2));
            for (int i = 0; i < Cards.Count; i++)
            {
                _players.GetValueOrDefault(_players.Keys.ToList()[i % _players.Keys.Count]).AddToDeck(Cards[i]);
            }
            foreach (Player p in _players.Values)
            {
                p.Begin();
            }
            return _players.Values.ToList();
        }

        public bool CardPlayed(string userId, int cardIndex)
        {
            Player currentPlayer = _players.GetValueOrDefault(userId);
            if (currentPlayer.PlayedCard == null)
            {
                currentPlayer.PlayCard(cardIndex);
                EnJeu.Add(currentPlayer.PlayedCard);
                foreach (Player p in _players.Values)
                {
                    if (p.PlayedCard == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                throw new PlayerHasAlreadyPlayedException();
            }
        }

        public List<string> FinalizeTour()
        {
            List<Player> players = _players.Values.ToList();
            CardComparator comparator = new CardComparator();
            players.Sort((p1, p2) => comparator.Compare(p2.PlayedCard, p1.PlayedCard));

            if (comparator.Compare(players[0].PlayedCard, players[1].PlayedCard) != 0)
            {
                players[0].WinRound(EnJeu);
                EnJeu = new List<Card>();
            }
            else
            {
                foreach (Player p in players)
                {
                    if (p.DeckCount != 0)
                    {
                        EnJeu.Add(p.TakeFirstOfTheDeck());
                    }
                }
            }
            List<string> loosers = new List<string>();
            foreach (Player p in _players.Values)
            {
                p.Reset();
                if (p.DeckCount == 0 && p.HandCount == 0)
                {
                    loosers.Add(p.UserId);
                }
            }
            return loosers;
        }

        internal List<Player> GetPlayers()
        {
            return _players.Values.ToList();
        }

        public List<string> GetPlayersId()
        {
            return _players.Keys.ToList();
        }
    }
}