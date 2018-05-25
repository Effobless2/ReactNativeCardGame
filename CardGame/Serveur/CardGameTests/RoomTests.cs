using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Models;
using Serveur.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTests
{
    [TestClass]
    public class RoomTests
    {

        [ExpectedException(typeof (FulfillRoomException))]
        [TestMethod]
        public void AddingAPlayerIntoAFulfillRoomShouldThrowAFulfillRoomException()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.AddPlayer("a");
            room.AddPlayer("b");
            room.AddPlayer("c");
        }

        [TestMethod]
        public void RoomShouldLooseOnePublicAfterThisOneJoinPlayers()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.AddPublic("a");
            room.AddPlayer("b");
            room.AddPlayer("a");
            Assert.AreEqual(0, room.PublicMembers.Count);
        }

        [ExpectedException(typeof(AlreadyInRoomException))]
        [TestMethod]
        public void RoomShouldThrowAlreadyInRoomEsceptionIfAUserWantsToJoinPlayersOfRoomTwoTimes()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.AddPlayer("a");
            room.AddPlayer("a");
        }

        [ExpectedException(typeof(AlreadyInRoomException))]
        [TestMethod]
        public void RoomShouldThrowAnAlreadyInRoomExceptionWhenAUserWantsToJoinPublicTwoTimes()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.AddPublic("a");
            room.AddPublic("a");
        }

        [ExpectedException(typeof(NotInThisRoomException))]
        [TestMethod]
        public void RemoveAnNotInThisRoomUserShouldThrowANotInThisRoomException()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.RemovePlayer("a");
        }

        [TestMethod]
        public void RemoveAPlayerShouldReturnTrueIFPlayersAreEmpty()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.AddPlayer("a");
            Assert.AreEqual(true, room.RemovePlayer("a"));
        }

        [TestMethod]
        public void RemoveAPlayerShouldReturnTrueIfPartyWasAnnuled()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.AddPlayer("a");
            room.AddPlayer("b");
            Assert.AreEqual(true, room.RemovePlayer("a"));
        }

        [TestMethod]
        public void RemoveAPlayerShouldReturnFalseIfThePartyWasNotReady()
        {
            Room room = new Room(Guid.NewGuid().ToString());
            room.MaxOfPlayers = 5;
            room.AddPlayer("a");
            room.AddPlayer("b");
            Assert.AreEqual(false, room.RemovePlayer("a"));
        }
    }
}
