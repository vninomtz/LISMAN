﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace LismanService {
    public partial class LismanService : IChatManager {
        static Dictionary<String, IChatManagerCallBack> connectionChatService = new Dictionary<String, IChatManagerCallBack>();

        public void JoinChat(string user, int idgame)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();
            if (connectionChatService.ContainsKey(user)) {
                connectionChatService[user] = connection;
            } else {
                connectionChatService.Add(user, connection);
            }


            foreach (var userGame in listGamesOnline[idgame]) {
                connectionChatService[userGame].NotifyNumberPlayers(listGamesOnline[idgame].Count);
                if (userGame != user) {
                    connectionChatService[userGame].NotifyJoinedPlayer(user);

                }
                
            }

            
        }

        public void LeaveChat(string user, int idgame) {
            try {
                foreach (var userGame in listGamesOnline[idgame]) {
                    connectionChatService[userGame].NotifyNumberPlayers(listGamesOnline[idgame].Count);
                }
                foreach (var userGame in listGamesOnline[idgame]) {
                    connectionChatService[userGame].NotifyLeftPlayer(user);


                }
            } catch (KeyNotFoundException ex) {
                Logger.log.Error("Function LeaveChat, " + ex);
            }

        }
       

        public void SendMessage(Message message, int idgame)
        {
            foreach (var userGame in listGamesOnline[idgame]) {           
                    connectionChatService[userGame].NotifyMessage(message);
            }
        }

        public void StartGame(string user, int idgame)
        {
            if (!multiplayerGameInformation.ContainsKey(idgame))
            {
                Game informationGame = new Game
                {
                    gameMap = GAMEMAP,
                    lismanUsers = new Dictionary<string, InformationPlayer>()

            };
                multiplayerGameInformation.Add(idgame, informationGame);
            }
            foreach (var userGame in listGamesOnline[idgame]) {

                if (AssignColorPlayer(idgame, userGame))
                {
                    connectionChatService[userGame].InitGame();
                }
               
            }
        }

        private bool AssignColorPlayer(int idgame, String user)
        {
            InformationPlayer infoPlayer = new InformationPlayer();
            int index = listGamesOnline[idgame].FindIndex(u => u == user);
            bool result = false;
            switch (index)
            {
                case 0:
                    infoPlayer.colorLisman = LISMANYELLOW;
                    break;
                case 1:
                    infoPlayer.colorLisman = LISMANBLUE;
                    break;
                case 2:
                    infoPlayer.colorLisman = LISMANRED;
                    break;
                case 3:
                    infoPlayer.colorLisman = LISMANGREEN;
                    break;
            }
            infoPlayer.lifesLisman = 3;
            multiplayerGameInformation[idgame].lismanUsers.Add(user, infoPlayer);

            if (multiplayerGameInformation[idgame].lismanUsers[user].colorLisman != 0)
            {
                result = true;
            }
            
            return result;
        }
    }
}
