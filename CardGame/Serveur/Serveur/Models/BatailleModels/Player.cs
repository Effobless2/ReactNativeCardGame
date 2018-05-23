using System;
using System.Collections.Generic;
using System.Linq;

namespace Serveur.Models.BatailleModels
{
    public class Player
    {
        public string UserId;
        public List<Card> Deck;
        public List<Card> Hand;
        public Card PlayedCard = null;

        public Player(string id)
        {
            UserId = id;
            Deck = new List<Card>();
            Hand = new List<Card>();
        }

        public void AddToDeck(Card card)
        {
            Deck.Add(card);
        }

        public void begin()
        {
            Hand = Deck.Skip(0).Take(7).ToList();
            Deck = Deck.Skip(7).ToList();
        }

        public void PlayCard(int cardIndex)
        {
            PlayedCard = Hand[cardIndex];
            Hand.RemoveAt(cardIndex);
        }
    }
}