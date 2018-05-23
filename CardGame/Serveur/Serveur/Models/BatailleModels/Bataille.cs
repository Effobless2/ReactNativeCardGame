using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models.BatailleModels
{
    public class Bataille
    {
        public List<Player> Players = new List<Player>();
        public List<Card> JeuDeCartes = new List<Card>();

        public Bataille(List<string> playersId)
        {
            foreach(string id in playersId)
            {
                Players.Add(new Player(id));
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
                Players[i % Players.Count].AddToDeck(JeuDeCartes[i]);
            }

            foreach(Player p in Players)
            {
                p.begin();
            }

        }
    }
}
