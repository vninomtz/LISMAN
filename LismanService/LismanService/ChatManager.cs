using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace LismanService {
    public partial class LismanService : IChatManager {
        Dictionary<String, IChatManagerCallBack> connectionChatService = new Dictionary<String, IChatManagerCallBack>();

        public void JoinChat(string user, int idgame)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();
            if (connectionChatService.ContainsKey(user)) {
                connectionChatService[user] = connection;
            } else {
                connectionChatService.Add(user, connection);
            }

            foreach (var userGame in listGamesOnline[idgame]) {
                if(userGame != user) {
                    connectionChatService[userGame].NotifyJoinedPlayer(user);
                }
            }
        }

        public void SendMessage(Message message, int idgame)
        {
            foreach (var userGame in listGamesOnline[idgame]) {
                if (userGame != message.Account.User) {
                    connectionChatService[userGame].NotifyMessage(message);
                }
            }
        }
    }
}
