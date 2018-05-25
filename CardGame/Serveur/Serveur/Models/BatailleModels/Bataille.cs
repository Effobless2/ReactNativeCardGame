using Serveur.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models.BatailleModels
{
    public class Bataille
    {
        public Dictionary<string,Player> Players = new Dictionary<string,Player>();
        public List<Card> JeuDeCartes = new List<Card>();
        public List<Card> CartesEnJeu = new List<Card>();

        public Bataille(List<string> playersId)
        {
            foreach(string id in playersId)
            {
                Players.Add(id,new Player(id));
            }
            string[] colours = { "C", "D", "H", "S" };
            foreach (string color in colours)
            {
                for (int i = 1; i < 14; i++)
                {
                    JeuDeCartes.Add(new Card(color, i));
                }
            }
            var random = new System.Random();
            JeuDeCartes.Sort((x, y) => random.Next(-1, 2));

            for (int i = 0; i < JeuDeCartes.Count; i++)
            {
                Players.GetValueOrDefault(Players.Keys.ToList()[i%Players.Keys.Count]).AddToDeck(JeuDeCartes[i]);
            }

            foreach(Player p in Players.Values)
            {
                p.begin();
            }

        }

        public bool CardPlayed(string userId, int cardIndex)
        {
            Player currentPlayer = Players.GetValueOrDefault(userId);
            if (currentPlayer.PlayedCard == null)
            {
                currentPlayer.PlayCard(cardIndex);
                CartesEnJeu.Add(currentPlayer.PlayedCard);
                foreach (Player p in Players.Values)
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

        public Player FinalizeTour()
        {
            List<Player> players = Players.Values.ToList();
            CardComparator comparator = new CardComparator();
            players.Sort((p1, p2) => comparator.Compare(p1.PlayedCard, p2.PlayedCard));
            if (comparator.Compare(players[0].PlayedCard,players[1].PlayedCard) == 0)
            {
                foreach(Player p in Players.Values)
                {
                    p.PlayedCard = null;
                }
                throw new EqualityException();
            }
            else
            {
                foreach (Player p in Players.Values)
                {
                    p.PlayedCard = null;
                }
                return players[0];
            }
        }
    }
}
