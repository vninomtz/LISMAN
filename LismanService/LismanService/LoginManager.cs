using System;
using System.Linq;
using DataAccess;



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
            var newAccount = new Account();
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exists = dataBase.AccountSet.Where(u => u.User == user && u.Password == password).Count();
                    if (exists > 0) {
                        newAccount = dataBase.AccountSet.Where(u => u.User == user && u.Password == password).Select(u => new Account
                        {
                            Id = u.Id,
                            User = u.User,
                            Password = u.Password,
                            Registration_date = u.Registration_date,
                            Key_confirmation = u.Key_confirmation
                        }).FirstOrDefault();
                        if(newAccount.Id > 0) {
                            if (!logginsConnections.ContainsKey(user)) {
                                logginsConnections.Add(user, null);
                            }
                            Console.WriteLine("User: {0} Connected at: {1}", newAccount.User, DateTime.Now);
                            
                        }
                        return newAccount;
                    } else {
                        newAccount.Id = 0;
                        return newAccount;
                    }
                }
            } catch (Exception ex) {
                Logger.log.Error(ex.Message);
                newAccount.Id = -1;
                return newAccount;
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

        public bool UserInSession(String userName)
        {
           bool inSesion = false;
            if (logginsConnections.ContainsKey(userName))
            {
                inSesion = true;
            }

            return inSesion;
        }
    }
}
