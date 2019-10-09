using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccess;

namespace LismanService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountManager" in both code and config file together.
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class LismanService : IAccountManager {
        public int AddAccount(Account account)
        {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    var newAccount = new DataAccess.Account
                    {
                        User = account.User,
                        Password = account.Password,
                        Registration_date = account.Registration_date,
                        Key_confirmation = account.Key_confirmation,
                        Player = new DataAccess.Player
                        {
                            First_name = account.Player.First_name,
                            Last_name = account.Player.Last_name,
                            Email = account.Player.Email
                        },
                        Record = new DataAccess.Record
                        {
                            Mult_best_score = 0,
                            Mult_games_played = 0,
                            Mult_games_won = 0,
                            Story_best_score = 0
                        }
                    };
                    try {
                        dataBase.AccountSet.Add(newAccount);
                        return dataBase.SaveChanges();

                    }catch(DbEntityValidationException ex) {
                        Console.WriteLine("Error: " + ex.Message);
                        return -1;
                    }
                }
            }catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public bool EmailExists(string emailAdress) {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.PlayerSet.Where(u => u.Email == emailAdress).Count();
                    if (exists > 0) {
                        return true;
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return false;
        }

        public Account GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public List<Record> GetRecords()
        {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    return dataBase.RecordSet.OrderBy(u => u.Story_best_score).Select(u => new Record
                    {

                        Id = u.Id,
                        Mult_best_score = u.Mult_best_score,
                        Mult_games_played = u.Mult_games_played,
                        Mult_games_won = u.Mult_games_won,
                        Story_best_score = u.Story_best_score,
                        Account = new Account
                        {
                            Id = u.Id,
                            User = u.Account.User,
                            Password = u.Account.Password,
                            Registration_date = u.Account.Registration_date,
                            Key_confirmation = u.Account.Key_confirmation
                        }
                        
                    }).ToList();
                }
            }catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public Account LoginAccount(string user, string password)
        {
            try {
                using(var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.AccountSet.Where(u => u.User == user & u.Password == password).Count();
                    if(exists > 0) {
                        return dataBase.AccountSet.Where(u => u.User == user & u.Password == password).Select(u => new Account
                        {
                            Id = u.Id,
                            User = u.User,
                            Password = u.Password,
                            Registration_date = u.Registration_date,
                            Key_confirmation = u.Key_confirmation
                        }).FirstOrDefault();
                    } else {
                        return null;
                    }
                }
            }catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool UserNameExists(string username) {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.AccountSet.Where(u => u.User == username).Count();
                    if (exists > 0) {
                        return true;
                    } 
                }
            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);            
            }
            return false;
        }
         
    }
}
