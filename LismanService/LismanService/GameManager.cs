using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using DataAccess;

namespace LismanService {
    public partial class LismanService : IGameManager {
        Dictionary<String, IGameManagerCallBack> connectionUsers = new Dictionary<String, IGameManagerCallBack>();
        Dictionary<int, List<String>> listGamesOnline = new Dictionary<int,List<String>>();

        public void CreateGame(string user)
        {
            IGameManagerCallBack connection = null;
            if (!connectionUsers.ContainsKey(user)) {
                connection = OperationContext.Current.GetCallbackChannel<IGameManagerCallBack>();
                connectionUsers.Add(user, connection);
            } else {
                connection = connectionUsers[user];
            }

            try {
                using (var dataBase = new EntityModelContainer()) {
                    var newGame = new DataAccess.Game
                    {
                        Creation_date = DateTime.Now,
                        Account = new List<DataAccess.Account>(),
                        Chat = new DataAccess.Chat()
                    };
                    dataBase.GameSet.Add(newGame);
                    if(dataBase.SaveChanges() != -1) {
                        var game = dataBase.GameSet.Where(u => u.Creation_date == newGame.Creation_date).FirstOrDefault();
                        var listPlayer = new List<String>();
                        listGamesOnline.Add(game.Id, listPlayer);
                        connection.NotifyGameCreated(1);
                    } else {
                        connection.NotifyGameCreated(-1);
                    }
                }
            }catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                connection.NotifyErrorMessage(-1);
            }
        }

        public void JoinGame(string user)
        {
            IGameManagerCallBack connection = null;
            if (!connectionUsers.ContainsKey(user)) {
                connection = OperationContext.Current.GetCallbackChannel<IGameManagerCallBack>();
                connectionUsers.Add(user, connection);
            } else {
                connection = connectionUsers[user];
            }

            foreach(KeyValuePair<int, List<String>> games in listGamesOnline) {
                if(games.Value.Count < 4) {
                    games.Value.Add(user);
                    foreach(String usergame in games.Value){
                        if(usergame != user) {
                            IGameManagerCallBack connectionGameUser = connectionUsers[user];
                            connectionGameUser.NotifyJoinedUser(user);
                        } else {
                            connection.NotifyJoined(games.Key, user);
                        }
                    }
                    return;
                }
            }

            connection.NotifyErrorMessage(-2); 
        }

        public void LeaveGame(string user, int game)
        {
            throw new NotImplementedException();
        }
    }
}
