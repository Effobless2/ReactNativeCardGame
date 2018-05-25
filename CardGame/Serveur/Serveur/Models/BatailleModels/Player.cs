using System;
using System.Collections.Generic;
using System.Linq;

namespace Serveur.Models.BatailleModels
{
    public class Player : ApplicationUser
    {
        private List<Card> _deck;
        private List<Card> _hand;
        public Card PlayedCard = null;
        public int DeckCount;
        public int HandCount;

        public Player(ApplicationUser user) : base(user.UserId, user.UserName)
        {
            _deck = new List<Card>();
            _hand = new List<Card>();
            DeckCount = 0;
            HandCount = 0;
    }

        public void AddToDeck(Card card)
        {
            _deck.Add(card);
            DeckCount++;
        }

        public void Begin()
        {
            _hand = _deck.Skip(0).Take(6).ToList();
            HandCount = 6;
            _deck = _deck.Skip(6).ToList();
            DeckCount -= 6;
        }

        public void PlayCard(int cardIndex)
        {
            PlayedCard = _hand[cardIndex];
            _hand.RemoveAt(cardIndex);
            HandCount--;
        }

        public List<Card> GetHand()
        {
            return _hand;
        }

        public void WinRound(List<Card> newCards)
        {
            foreach (Card card in newCards)
            {
                AddToDeck(card);
            }
        }

        public void Reset()
        {
            PlayedCard = null;
            _hand.Add(TakeFirstOfTheDeck());
            HandCount++;
            
        }

        public Card TakeFirstOfTheDeck()
        {
            Card c = _deck[0];
            _deck.RemoveAt(0);
            DeckCount--;
            return c;
        }
    }
}