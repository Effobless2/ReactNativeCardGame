using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Hubs;
using Serveur.Models;
using System;

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
            Assert.AreEqual(1, room.Public.Count);
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
            Assert.AreEqual(0, room.Public.Count);
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
            Assert.AreEqual(0, room.Public.Count);
        }
    }
}
