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

        public Bataille(List<string> playersId)
        {
            foreach(string id in playersId)
            {
                Players.Add(id,new Player(id));
            }
            string[] colours = { "coeur", "trèfle", "pique", "carreau" };
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
            Players.GetValueOrDefault(userId).PlayCard(cardIndex);
            foreach(Player p in Players.Values)
            {
                if (p.PlayedCard == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
