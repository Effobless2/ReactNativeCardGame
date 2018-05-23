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
            foreach(Player p in bataille.Players)
            {
                Assert.AreEqual(7, p.Hand.Count);
            }
        }

        [TestMethod]
        public void EveryPlayersShouldHave19CardsInTheDeckForADual()
        {
            List<string> ids = new List<string>();
            ids.Add("a");
            ids.Add("b");
            Bataille bataille = new Bataille(ids);
            foreach (Player p in bataille.Players)
            {
                Assert.AreEqual(19, p.Deck.Count);
            }
        }
    }
}
