using System;
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
            } catch (KeyNotFoundException) {
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
                    
                };
                multiplayerGameInformation.Add(idgame, informationGame);
            }
            foreach (var userGame in listGamesOnline[idgame]) {
                
                connectionChatService[userGame].InitGame();
            }
        }
    }
}
