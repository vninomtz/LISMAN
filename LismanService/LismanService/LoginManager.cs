using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using DataAccess;
using System.ServiceModel;


namespace LismanService {
    public partial class LismanService : ILoginManager {
        

        public bool EmailExists(string emailAdress)
        {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.PlayerSet.Where(u => u.Email == emailAdress).Count();
                    if (exists > 0) {
                        return true;
                    }
                }
            } catch (Exception ex) {
                Logger.log.Error(ex);
            }
            return false;
        }
        public Account LoginAccount(string user, string password)
        { 
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.AccountSet.Where(u => u.User == user & u.Password == password).Count();
                    if (exists > 0) {
                        var newAccount = dataBase.AccountSet.Where(u => u.User == user & u.Password == password).Select(u => new Account
                        {
                            Id = u.Id,
                            User = u.User,
                            Password = u.Password,
                            Registration_date = u.Registration_date,
                            Key_confirmation = u.Key_confirmation
                        }).FirstOrDefault();
                        if(newAccount != null) {
                            if (!connectionChatService.ContainsKey(user)) {
                                connectionChatService.Add(user, null);
                            }
                            Console.WriteLine("User: {0} Connected at: {1}", newAccount.User, DateTime.Now);
                            
                        }
                        return newAccount;
                    } else {
                        return null;
                    }
                }
            } catch (Exception ex) {
                Logger.log.Error(ex.Message);
                return null;
            }
        }

        public int LogoutAccount(string user)
        {
            if (connectionChatService.ContainsKey(user)) {
                connectionChatService.Remove(user); 
            }
            return 1;
        }

        public bool UserNameExists(string username)
        {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.AccountSet.Where(u => u.User == username).Count();
                    if (exists > 0) {
                        return true;
                    }
                }
            } catch (Exception ex) {
                Logger.log.Error(ex);
            }
            return false;
        }


        public Account GetAccountByUser(string user)
        {
            try {
                using (var dataBase = new EntityModelContainer()) {
                    return dataBase.AccountSet.Where(u => u.User == user).Select(u => new Account
                    {
                        Id = u.Id,
                        User = u.User,
                        Password = u.Password,
                        Registration_date = u.Registration_date,
                        
                    }).FirstOrDefault();
                }
            }catch(Exception ex) {
                Logger.log.Error(ex);
                return null;
            }
        }
    }
}
