using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using DataAccess;
using System.Data.Entity.Validation;


namespace LismanService {
    public partial class LismanService : IGameManager {
       static Dictionary<int, List<String>> listGamesOnline = new Dictionary<int,List<String>>();

        public int CreateGame(string user)
        {
            Random random = new Random();

            int idgame = random.Next(999);
            var listPlayer = new List<String>();
            listGamesOnline.Add(idgame, listPlayer);
            listGamesOnline[idgame].Add(user);
            return idgame;
            /*Account account = GetAccountByUser(user);
            try {
                using (var dataBase = new EntityModelContainer()) {
                    var newGame = new DataAccess.Game
                    {
                        GameCreator = new DataAccess.Account { 
                            Id = account.Id,
                            User = account.User,
                            Password = account.Password,
                            Registration_date = account.Registration_date
                        },
                        Status = false,
                        Creation_date = DateTime.Now,
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
            }catch(DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return -1;
            }*/
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
            int isDelete = 1; 
            var listGameUserNames = listGamesOnline[game];
                listGameUserNames.Remove(user);
                if (listGameUserNames.Count == 0) {
                    listGamesOnline.Remove(game);
            }
            return isDelete;
        }
    }
}
