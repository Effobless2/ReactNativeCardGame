using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serveur.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTests
{
    [TestClass]
    public class ApplicationUserTest
    {
        [TestMethod]
        public void GetUserNameEqualsNameShouldReturnName()
        {
            ApplicationUser user = new ApplicationUser("aaa", "Name");

            Assert.AreEqual("Name", user.UserName);
        }
    }
}
