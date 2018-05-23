using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Models.BatailleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameTests
{
    [TestClass]
    public class BatailleTest
    {
        [TestMethod]
        public void BatailleShouldHaveTheSameNumberOfPlayersThanTheNumberOfReceivedIds()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            Assert.AreEqual(2, bataille.Players.Count);
        }

        [TestMethod]
        public void BatailleShouldHaveA52CardGameAtTheBeginning()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            Assert.AreEqual(52, bataille.JeuDeCartes.Count);
        }

        [TestMethod]
        public void EveryPlayersShouldShare7CardsAtTheHandBeginning()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            foreach (Player p in bataille.Players.Values)
            {
                Assert.AreEqual(6, p.Hand.Count);
            }
        }

        [TestMethod]
        public void EveryPlayersShouldHave19CardsInTheDeckForADual()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            foreach (Player p in bataille.Players.Values)
            {
                Assert.AreEqual(20, p.Deck.Count);
            }
        }

        [TestMethod]
        public void WhenFirstPlayerPlayACardItIsRemovedFromHandandTransferedFromPlayedCard()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            Card currentCard = bataille.Players.GetValueOrDefault("a").Hand[0];
            Assert.AreEqual(false, bataille.CardPlayed("a", 0));
            Assert.AreEqual(5, bataille.Players.GetValueOrDefault("a").Hand.Count);
            Assert.AreEqual(currentCard, bataille.Players.GetValueOrDefault("a").PlayedCard);

        }

        [TestMethod]
        public void WhenLastPlayerPlayACardTourIsReady()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            bataille.CardPlayed("a", 0);
            Assert.AreEqual(true, bataille.CardPlayed("b", 0));


        }
    }
}
