using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTestLoginManager
    {
        LismanService.LismanService loginManager = new LismanService.LismanService();

        [TestMethod]
        public void TestEmailExists()

        {
            string email = "alan14_04@hotmail.com";
            bool expected = false;
            
            bool actual = loginManager.EmailExists(email);
            Assert.AreEqual(expected, actual);           
        }

        [TestMethod]
        public void TestLoginAccount()
        {
            string username = "gume";
            string password = "98934";

            LismanService.Account expected = null;
            LismanService.Account actual = loginManager.LoginAccount(username, password);
            Assert.AreEqual(expected,actual);           
        }

        [TestMethod]
        public void TestCallbackInitGame()
        {
            bool expectedResult = true;
            string user = "alan";
            int idGame = 1;
            TestChatManagerCallback testChatManagerCallback = new TestChatManagerCallback();

            LismanService.LismanService lismanService = new LismanService.LismanService(() => testChatManagerCallback);

            lismanService.StartGame(user,idGame);

            Assert.AreEqual(expectedResult,
                testChatManagerCallback.ResponseInitGame, "Callback result");
        }
    }
}
