using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Hubs;
using Serveur.Models;
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

        [TestMethod]
        public void GetUndefinedUserWithIdShouldThrowAUserIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            ApplicationUser user;
            try
            {
                user = cardGame.cardGame.GetUserWithId(Guid.NewGuid().ToString());
            }
            catch (UserIsUndefinedException)
            {
                user = null;
            }
            Assert.AreEqual(null, user);
        }

        [TestMethod]
        public void GetAnUndefinedRoomWithIdShouldThrowRoomIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            Room room;
            try
            {
                room = cardGame.cardGame.GetRoomWithId(Guid.NewGuid().ToString());
            }
            catch (RoomIsUndefinedException)
            {
                room = null;
            }
            Assert.AreEqual(null, room);
        }

        [TestMethod]
        public void RemovingUndefinedRoomShouldThrowRoomIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            List<string> users;
            try
            {
                users = cardGame.RemovingRoom(Guid.NewGuid().ToString());
            }
            catch (RoomIsUndefinedException)
            {
                users = null;
            }
            Assert.AreEqual(null, users);
        }

        [TestMethod]
        public void RemovingAnUndefinedUserShouldThrowAUserIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            List<string> user;
            try
            {
                user = cardGame.RemovingUser(Guid.NewGuid().ToString());
            }
            catch (UserIsUndefinedException)
            {
                user = null;
            }
            Assert.AreEqual(null, user);
        }

        [TestMethod]
        public void UpdatingRoomWithanUndefinedRoomShouldThrowARoomIsUndefinedException()
        {
            APICardGame cardGame = new APICardGame();
            string guid = Guid.NewGuid().ToString();
            int i = 0;
            try
            {
                cardGame.UpdateRoom(guid, guid);
                i = 1;
            }
            catch (RoomIsUndefinedException)
            {
                i = -1;
            }
            Assert.AreEqual(-1, i);
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
    }
}
