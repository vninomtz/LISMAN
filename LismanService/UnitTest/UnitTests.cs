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
        public void TestInit()
        {
            testChatManagerCallback = new TestChatManagerCallback();
            lismanService = new LismanService.LismanService(() => testChatManagerCallback);            
            this.user = "gume";
            this.idGame = 1;
            lismanService.InitializeTest();


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

        [TestMethod]
        public void TestGetValueBox()
        {
            int result = lismanService.GetValueBox(1,1,5);
            int expected = 1;
            Assert.AreEqual(expected,result);
        }

        [TestMethod]

        public void TestUpdateScore()
        {
            int result = lismanService.UpdateScore(idGame, 3, 100);
            int expected = 100;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestUpdateSubstractLifes()
        {
            int result = lismanService.UpdateSubtractLifes(idGame, 4);
            int expected = 1;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGameWillEnd()
        {
            bool result = lismanService.GameWillEnd(idGame);
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPlayerWillDead()
        {
            bool result = lismanService.PlayerWillDead(idGame, 3);
            bool expected = true;
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void TestPlayerWontDead()
        {
            bool result = lismanService.PlayerWillDead(idGame, 4);
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetColorLismanByUser()
        {
            int colorLisman = lismanService.GetColorLismanByUser(idGame,"Alan");
            int expected = 3;
            Assert.AreEqual(colorLisman, expected);
        }

        [TestMethod]

        public void TestGetInitialPositionLisman()
        {
            int[] result = lismanService.GetInitialPositionsLisman(4);
            int[] expected = new int[2] { 22, 1 };
            Assert.AreEqual(expected[0], result[0]);          

        }

        [TestMethod]

        public void TestAssignColorPlayer()
        {
            bool result = lismanService.AssignColorPlayer(2, "victor");
            bool expected = true;
            Assert.AreEqual(expected, result);
        }




        

















    }
}
