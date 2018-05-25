using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Hubs;
using Serveur.Models;
using Serveur.Models.BatailleModels;
using Serveur.Models.Exceptions;
using System;
using System.Collections.Generic;

namespace CardGameTests
{
    [TestClass]
    public class APICardGameTests
    {
        [TestMethod]
        public void NewCardGameShouldHaveEmptyRooms()
        {
            APICardGame cardGame = new APICardGame();
            Assert.AreEqual(0, cardGame.GetRooms().Count);
        }

        [TestMethod]
        public void NewCardGameShouldHaveEmptyUsers()
        {
            APICardGame cardGame = new APICardGame();
            Assert.AreEqual(0, cardGame.GetUsers().Count);
        }

        [TestMethod]
        public void CardGameShouldHaveAUniqueUserAfterOneConnection()
        {
            APICardGame cardGame = new APICardGame();
            cardGame.Connection(Guid.NewGuid().ToString());
            Assert.AreEqual(1, cardGame.GetUsers().Count);
        }

        [TestMethod]
        public void CardGameShouldHaveAUniqueRoomAfterOneRequest()
        {
            APICardGame cardGame = new APICardGame();
            cardGame.CreatingRoom();
            Assert.AreEqual(1, cardGame.GetRooms().Count);
        }

        [TestMethod]
        public void RoomMustHaveAPlayerAfterOneRequest()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            cardGame.Connection(guid);
            Room room = cardGame.CreatingRoom();
            cardGame.AddingPlayer(room.RoomId, guid);
            Assert.AreEqual(1, room.Players.Count);

        }

        [TestMethod]
        public void RoomMustHaveAPublicAfterOneRequest()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            cardGame.Connection(guid);
            Room room = cardGame.CreatingRoom();
            cardGame.AddingPublic(room.RoomId, guid);
            Assert.AreEqual(1, room.PublicMembers.Count);
        }

        [TestMethod]
        public void RoomShouldLooseAPublicAfterRequest()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            cardGame.Connection(guid);
            Room room = cardGame.CreatingRoom();
            cardGame.AddingPublic(room.RoomId, guid);
            cardGame.RemovingPublic(room.RoomId, guid);
            Assert.AreEqual(0, room.PublicMembers.Count);
        }

        [TestMethod]
        public void RoomShouldLooseAPlayerAfterRequest()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            cardGame.Connection(guid);
            Room room = cardGame.CreatingRoom();
            cardGame.AddingPlayer(room.RoomId, guid);
            cardGame.RemovingPlayer(room.RoomId, guid);
            Assert.AreEqual(0, room.Players.Count);
        }

        [TestMethod]
        public void RoomMustBeRemovedAfterRequest()
        {
            APICardGame cardGame = new APICardGame();
            Room room = cardGame.CreatingRoom();
            cardGame.RemovingRoom(room.RoomId);
            Assert.AreEqual(0, cardGame.GetRooms().Count);
        }

        [TestMethod]
        public void CardGameMustLooseAUserAfterDisconnection()
        {
            APICardGame cardGame = new APICardGame();
            ApplicationUser user = cardGame.Connection(Guid.NewGuid().ToString());
            cardGame.RemovingUser(user.UserId);
            Assert.AreEqual(0, cardGame.GetUsers().Count);
        }

        [TestMethod]
        public void RoomContentMustBeUpdatedAfterADisconnectionOfOnePlayer()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            ApplicationUser user = cardGame.Connection(guid);
            Room room = cardGame.CreatingRoom();
            cardGame.AddingPlayer(room.RoomId, guid);
            cardGame.UpdateRoom(room.RoomId, user.UserId);
            Assert.AreEqual(0, room.Players.Count);
        }

        [TestMethod]
        public void RoomContentMustBeUpdatedAfterADisconnectionOfOnePublic()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            ApplicationUser user = cardGame.Connection(guid);
            Room room = cardGame.CreatingRoom();
            cardGame.AddingPublic(room.RoomId, guid);
            cardGame.UpdateRoom(room.RoomId, user.UserId);
            Assert.AreEqual(0, room.PublicMembers.Count);
        }

        [ExpectedException(typeof(RoomIsUndefinedException))]
        [TestMethod]
        public void GetAnUndefinedRoomWithIdShouldThrowRoomIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            cardGame.cardGame.GetRoomWithId(Guid.NewGuid().ToString());
        }

        [ExpectedException(typeof(RoomIsUndefinedException))]
        [TestMethod]
        public void RemovingUndefinedRoomShouldThrowRoomIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            cardGame.RemovingRoom(Guid.NewGuid().ToString());
        }

        [ExpectedException(typeof(UserIsUndefinedException))]
        [TestMethod]
        public void RemovingAnUndefinedUserShouldThrowAUserIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            cardGame.RemovingUser(Guid.NewGuid().ToString());
        }

        [ExpectedException(typeof(RoomIsUndefinedException))]
        [TestMethod]
        public void UpdatingRoomWithanUndefinedRoomShouldThrowARoomIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            cardGame.UpdateRoom(guid, guid);
        }

        [TestMethod]
        public void UpdatingRoomWithanUndefinedUserShouldReturnFalse()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            Room room = cardGame.CreatingRoom();
            bool result = cardGame.UpdateRoom(room.RoomId, guid);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void BatailleCreatedWhenBatailleBegins()
        {
            APICardGame cardGame = new APICardGame();
            Room room = cardGame.CreatingRoom();
            ApplicationUser userA = cardGame.Connection("a");
            ApplicationUser userB = cardGame.Connection("b");
            cardGame.AddingPlayer(room.RoomId, userA.UserId);
            cardGame.AddingPlayer(room.RoomId, userB.UserId);
            List<Player> result = cardGame.BatailleBegin(room.RoomId);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void WhenFirstPlayerPlayACardItIsRemovedFromHandandTransferedFromPlayedCard()
        {
            APICardGame cardGame = new APICardGame();
            Room room = cardGame.CreatingRoom();
            ApplicationUser userA = cardGame.Connection("a");
            ApplicationUser userB = cardGame.Connection("b");
            cardGame.AddingPlayer(room.RoomId, userA.UserId);
            cardGame.AddingPlayer(room.RoomId, userB.UserId);
            cardGame.BatailleBegin(room.RoomId);
            Card currentCard = room.bataille.Players.GetValueOrDefault("a").GetHand()[0];
            Assert.AreEqual(false, cardGame.CardPlayed(room.RoomId, userA.UserId, 0));
            Assert.AreEqual(5, room.bataille.Players.GetValueOrDefault("a").GetHand().Count);
            Assert.AreEqual(currentCard, room.bataille.Players.GetValueOrDefault("a").PlayedCard);

        }

        [TestMethod]
        public void WhenLastPlayerPlayACardTourIsReady()
        {
            APICardGame cardGame = new APICardGame();
            Room room = cardGame.CreatingRoom();
            ApplicationUser userA = cardGame.Connection("a");
            ApplicationUser userB = cardGame.Connection("b");
            cardGame.AddingPlayer(room.RoomId, userA.UserId);
            cardGame.AddingPlayer(room.RoomId, userB.UserId);
            cardGame.BatailleBegin(room.RoomId);
            Card currentCard = room.bataille.Players.GetValueOrDefault("a").GetHand()[0];
            cardGame.CardPlayed(room.RoomId, userA.UserId, 0);
            Assert.AreEqual(true, cardGame.CardPlayed(room.RoomId, userB.UserId, 0));

        }
    }
}
