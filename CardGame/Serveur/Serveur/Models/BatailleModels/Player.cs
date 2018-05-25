using System;
using System.Collections.Generic;
using System.Linq;

namespace Serveur.Models.BatailleModels
{
    public class Player
    {
        public string UserId;
        private List<Card> Deck;
        private List<Card> Hand;
        public Card PlayedCard = null;
        public int DeckCount;
        public int HandCount;

        public Player(string id)
        {
            UserId = id;
            Deck = new List<Card>();
            Hand = new List<Card>();
            DeckCount = 0;
            HandCount = 0;
    }

        public void AddToDeck(Card card)
        {
            Deck.Add(card);
            DeckCount++;
        }

        public void begin()
        {
            Hand = Deck.Skip(0).Take(6).ToList();
            HandCount = 6;
            Deck = Deck.Skip(6).ToList();
            DeckCount -= 6;
        }

        public void PlayCard(int cardIndex)
        {
            PlayedCard = Hand[cardIndex];
            Hand.RemoveAt(cardIndex);
            HandCount--;
        }

        public List<Card> GetHand()
        {
            return Hand;
        }

        public void WinRound(List<Card> newCards)
        {
            foreach (Card card in newCards)
            {
                AddToDeck(card);
            }
        }

        public void reset()
        {
            PlayedCard = null;
            Hand.Add(TakeFirstOfTheDeck());
            HandCount++;
            
        }

        public Card TakeFirstOfTheDeck()
        {
            Card c = Deck[0];
            Deck.RemoveAt(0);
            DeckCount--;
            return c;
        }
    }
}