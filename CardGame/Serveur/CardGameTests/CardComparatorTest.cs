using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Models.BatailleModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTests
{
    [TestClass]
    public class CardComparatorTest
    {
        [TestMethod]
        public void XEqualsTenAndYEqualsOneReturn1()
        {
            Card x = new Card("S", 10);
            Card y = new Card("H", 1);
            Assert.AreEqual(1, new CardComparator().Compare(x, y));
        }

        [TestMethod]
        public void XEqualsYReturn0()
        {
            Card x = new Card("S", 10);
            Card y = new Card("H", 10);
            Assert.AreEqual(0, new CardComparator().Compare(x, y));
        }

        [TestMethod]
        public void XEqualsAHeadAndYEqualsOneReturnsMinus1()
        {
            Card x = new Card("S", 12);
            Card y = new Card("H", 1);
            Assert.AreEqual(-1, new CardComparator().Compare(x, y));
        }

        [TestMethod]
        public void XEquals1AndYEquals2ReturnsMinus1()
        {
            Card x = new Card("S", 1);
            Card y = new Card("H", 2);
            Assert.AreEqual(-1, new CardComparator().Compare(x, y));
        }

        [TestMethod]
        public void XEqualsOneAndYEqualsAHeadReturns1()
        {
            Card x = new Card("S", 1);
            Card y = new Card("H", 12);
            Assert.AreEqual(1, new CardComparator().Compare(x, y));
        }
    }
}
