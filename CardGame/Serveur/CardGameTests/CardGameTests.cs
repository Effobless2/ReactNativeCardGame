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

        [TestMethod]
        public void RemovingUserFromMultipleRooms()
        {
            CardGame cardGame = new CardGame();
            Room r1 = cardGame.CreatingRoom();
            Room r2 = cardGame.CreatingRoom();
            ApplicationUser user = cardGame.Connection("userId");
            cardGame.AddingPlayer(r1.RoomId, user.UserId);
            cardGame.AddingPublic(r2.RoomId, user.UserId);
            List<string> result = cardGame.RemovingUser(user.UserId);
            Assert.AreEqual(true, result.Contains(r1.RoomId) && result.Contains(r2.RoomId));

        }
    }
}
