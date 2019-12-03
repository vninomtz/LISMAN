using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace LismanService {
    public partial class LismanService : IChatManager {
        static Dictionary<String, IChatManagerCallBack> connectionChatService = new Dictionary<String, IChatManagerCallBack>();
        
        private Func<IChatManagerCallBack> callbackChannel;
        public LismanService(Func<IChatManagerCallBack> callbackCreator)
        {
            this.callbackChannel = callbackCreator ?? throw new ArgumentNullException("callbackCreator");
        }


        public void JoinChat(string user, int idgame)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();
            
            if (connectionChatService.ContainsKey(user)) {
                connectionChatService[user] = connection;
            } else {
                connectionChatService.Add(user, connection);
            }


            foreach (var userGame in listGamesOnline[idgame]) {
                try
                {
                    connectionChatService[userGame].NotifyNumberPlayers(listGamesOnline[idgame].Count);
                    if (userGame != user)
                    {
                        connectionChatService[userGame].NotifyJoinedPlayer(user);

                    }
                }catch(CommunicationException ex)
                {
                    Logger.log.Error("JoinChat, " + ex);
                }
                
                
            }

            
        }

        public void LeaveChat(string user, int idgame) {
            try {
                foreach (var userGame in listGamesOnline[idgame]) {
                    try
                    {
                        connectionChatService[userGame].NotifyNumberPlayers(listGamesOnline[idgame].Count);
                        connectionChatService[userGame].NotifyLeftPlayer(user);
                    }
                    catch (CommunicationException ex)
                    {
                        Logger.log.Error("LeaveChat, " + ex);
                    }

                }
            } catch (KeyNotFoundException ex) {
                Logger.log.Error("Function LeaveChat, " + ex);
            }

        }
       

        public void SendMessage(Message message, int idgame)
        {
                foreach (var userGame in listGamesOnline[idgame])
                {
                    try
                    {
                        connectionChatService[userGame].NotifyMessage(message);
                    }
                    catch (CommunicationException ex)
                    {
                        Logger.log.Error("SendMessage, " + ex);
                    }

                }
            

        }

        public void StartGame(string user, int idgame)
        {
            ReadMapGame();
            if (!multiplayerGameInformation.ContainsKey(idgame))
            {
                Game informationGame = new Game
                {

                    gameMap = GAMEMAP,
                    lismanUsers = new Dictionary<int, InformationPlayer>()

                };
                multiplayerGameInformation.Add(idgame, informationGame);
            }
                foreach (var userGame in listGamesOnline[idgame])
                {

                    if (AssignColorPlayer(idgame, userGame))
                    {
                        try
                        {
                            connectionChatService[userGame].InitGame();
                        }
                        catch (CommunicationException ex)
                        {
                            Logger.log.Error("StartGame, " + ex);
                        }

                    }

                }
                Console.WriteLine("Game started ID:{0}, at:{1}", idgame, DateTime.Now);
        }

        private bool AssignColorPlayer(int idgame, String user)
        {
            InformationPlayer infoPlayer = new InformationPlayer();
            int index = listGamesOnline[idgame].FindIndex(u => u == user);
            bool result = false;
            int colorAssigned = 0;
            switch (index)
            {
                case 0:
                    colorAssigned =  LISMANYELLOW;
                    infoPlayer.initialDirecction = "RIGHT";
                    break;
                case 1:
                    colorAssigned = LISMANRED;
                    infoPlayer.initialDirecction = "LEFT";
                    break;
                case 2:
                    colorAssigned = LISMANBLUE;
                    infoPlayer.initialDirecction = "RIGHT";
                    break;
                case 3:
                    colorAssigned = LISMANGREEN;
                    infoPlayer.initialDirecction = "LEFT";
                    break;
            }
            infoPlayer.userLisman = user;
            infoPlayer.lifesLisman = 3;
            infoPlayer.hasPower = false;
            infoPlayer.isLive = true;
            infoPlayer.scoreLisman = 0;
            multiplayerGameInformation[idgame].lismanUsers.Add(colorAssigned, infoPlayer);

            if (colorAssigned != 0)
            {
                result = true;
            }
            
            return result;
        }
    }
}
