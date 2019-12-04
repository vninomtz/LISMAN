using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LismanService;

namespace UnitTest
{
    class TestChatManagerCallback : LismanService.IChatManagerCallBack
    {
        private bool responseInitGame;
        private string responseNotifyJoinedPlayer;
        private string responseNotifyLeftPlayer;
        private LismanService.Message responseNotifyMessage;
        private int responseNumberPlayers;




        internal TestChatManagerCallback()
        {
            this.responseInitGame = false;
            this.responseNotifyJoinedPlayer = "";
            this.responseNotifyLeftPlayer = "";
            this.responseNotifyMessage = null;
            this.responseNumberPlayers = 0;
        }
        internal bool ResponseInitGame {
            get { return responseInitGame; }
        }
        internal String ResponseNotifyJoinedPlayer {
            get { return responseNotifyJoinedPlayer; }
        }

        internal String ResponseNotifyLeftPlayer {
            get { return responseNotifyLeftPlayer; }
        }

        internal Message ResponseNotifyMessage {
            get { return responseNotifyMessage; }
        }

        internal int ResponseNumberPlayers {
            get { return responseNumberPlayers; }
        }

        public void InitGame()
        {
            this.responseInitGame = true;
        }

        public void NotifyJoinedPlayer(string user)
        {
            this.responseNotifyJoinedPlayer = user;
        }

        public void NotifyLeftPlayer(string user)
        {
            this.responseNotifyLeftPlayer = user;
        }

        public void NotifyMessage(Message message)
        {
            this.responseNotifyMessage = message;
        }

        public void NotifyNumberPlayers(int numberPlayers)
        {
            this.responseNumberPlayers = numberPlayers;
        }
    }
}
