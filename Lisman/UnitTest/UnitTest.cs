using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        Lisman.MultiplayerGame windowGame;
        LismanService.LoginManagerClient LoginClient = new LismanService.LoginManagerClient();
        LismanService.AccountManagerClient accountClient = new LismanService.AccountManagerClient();       


        [TestInitialize]
        public void InitializeTest()
        {
         windowGame = new Lisman.MultiplayerGame("test");
         windowGame.MatrixGame();
        }

        [TestMethod]
        public void TestCanMove()
        {
            
            bool expected = false;
            bool result = windowGame.canMove(0, 5);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCantMove()
        {

            bool expected = true;
            bool result = windowGame.canMove(1, 5);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]

        public void TestLoginInvalidAccount()
        {
           var account = LoginClient.LoginAccount("alanbrito", "12345");
            int result = account.Id;
            int expected = 0;
            Assert.AreEqual(expected, result);
           
            
        }

        [TestMethod]
        public void TestEmailExists()
        {
            bool result = LoginClient.EmailExists("alan14_04@hotmail.com");
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void testEmailDoesntExists()
        {
            bool result = LoginClient.EmailExists("pepe@hotmail.com");
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

     


    }
}
