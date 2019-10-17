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
        Dictionary<int, List<String>> listGamesOnline = new Dictionary<int,List<String>>();

        public int CreateGame(string user)
        {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    var newGame = new DataAccess.Game
                    {
                        Creation_date = DateTime.Now,
                        
                        Members = new List<DataAccess.Account>(),
                        Chat = new DataAccess.Chat()
                    };
                    dataBase.GameSet.Add(newGame);
                    if(dataBase.SaveChanges() != -1) {
                        var game = dataBase.GameSet.Where(u => u.Creation_date == newGame.Creation_date).FirstOrDefault();
                        var listPlayer = new List<String>();
                        listGamesOnline.Add(game.Id, listPlayer);
                        return game.Id;
                    } else {
                        return -1;
                    }
                }
            }catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public int JoinGame(string user)
        {
            foreach (KeyValuePair<int, List<String>> games in listGamesOnline) {
                if(games.Value.Count < 4) {
                    games.Value.Add(user);
                    return games.Key;
                }
            }

            return -1;
        }

        public int LeaveGame(string user, int game)
        {
            throw new NotImplementedException();
        }
    }
}
