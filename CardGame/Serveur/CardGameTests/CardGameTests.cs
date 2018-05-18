using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Models;
using Serveur.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTests
{
    [TestClass]
    public class CardGameTests
    {

        [ExpectedException(typeof(UserIsUndefinedException))]
        [TestMethod]
        public void GetUndefinedUserWithIdShouldThrowAUserIsUndefinedException()
        {
            CardGame cardGame = new CardGame();
            ApplicationUser user;
            cardGame.GetUserWithId(Guid.NewGuid().ToString());
        }
    }
}
