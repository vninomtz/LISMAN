using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        LismanService.LismanService lismanService;
        TestChatManagerCallback testChatManagerCallback;
        string user = "";
        int idGame = 0;

        [TestInitialize]
        public void testInit()
        {
            testChatManagerCallback = new TestChatManagerCallback();
            lismanService = new LismanService.LismanService(() => testChatManagerCallback);
            lismanService.InsertGameTest(1);
            this.user = "gume";
            this.idGame = 1;


        }

        [TestMethod]
        public void TestCallbackInitGame() 
        {            
            
            bool expectedResult = true;                   
            lismanService.StartGame(user,idGame);
            Assert.AreEqual(expectedResult,
                testChatManagerCallback.ResponseInitGame, "Callback result");
        }

        [TestMethod]
        public void TestJoinGame()
        {
            int expected = 1;
            int result = lismanService.JoinGame("Patricio");
            Assert.AreEqual(expected, result);
            
        }

        [TestMethod]
        public void TestLeaveGameCallback()
        {
            int expected = 3;
            lismanService.LeaveGame(user,idGame);
            Assert.AreEqual(expected, testChatManagerCallback.ResponseNumberPlayers);
        }

        [TestMethod]
        public void TestLeaveGameUserCallback()
        {
            string expected = "gume";
            lismanService.LeaveChat(user, idGame);
            Assert.AreEqual(expected, testChatManagerCallback.ResponseNotifyLeftPlayer);
        }

        [TestMethod]
        public void TestSendMessageCallback()
        {
            LismanService.Message expected = new LismanService.Message();
            expected.Text = "Hola";
            lismanService.SendMessage(expected,idGame);

            Assert.AreEqual(expected, testChatManagerCallback.ResponseNotifyMessage);
        }

 

        [TestMethod]
        public void TestLogoutAccount()
        {
            int expected = 1;
            int result = lismanService.LogoutAccount(user);

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestUserInActiveSession()
        {
            bool expected = true;
            bool result = lismanService.UserInSession("Alan");
            Assert.AreEqual(expected, result);

        }

       [TestMethod]
        public void TestUserInInactiveSession()
        {
            bool expected = false;
            bool result = lismanService.UserInSession("juan");
            Assert.AreEqual(expected, result);

        }

















    }
}
