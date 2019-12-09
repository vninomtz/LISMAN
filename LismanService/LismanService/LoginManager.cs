using System;
using System.Linq;
using DataAccess;



namespace LismanService {
    /// <summary>
    /// Implementacion de la Interfaz ILoginManager
    /// </summary>
    public partial class LismanService : ILoginManager {
        
        /// <summary>
        /// Método que evalua si existe un email igual registrado
        /// </summary>
        /// <param name="emailAdress">email que sera consultado</param>
        /// <returns>regresa un valor true si existe el email</returns>
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

        /// <summary>
        /// Métododo que permite iniciar sesion en el sistema
        /// </summary>
        /// <param name="user">nombre del ususario del jugador</param>
        /// <param name="password">contraseña del usuario del sistema</param>
        /// <returns></returns>
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

        /// <summary>
        /// Método que cierra la sesión de un Cliente
        /// </summary>
        /// <param name="user">nombre de usuario del jugador</param>
        /// <returns>un valor entero si se realizo exitosamente</returns>
        public int LogoutAccount(string user)
        {
            if (connectionChatService.ContainsKey(user)) {
                connectionChatService.Remove(user); 
            }
            return 1;
        }

        /// <summary>
        /// Método que valida si un nombre de ususario ya se encuentra registrado
        /// </summary>
        /// <param name="username"></param>
        /// <returns>regresa un valor true si es que existe</returns>
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

        /// <summary>
        /// Método que obtiene la Account de un jugador por su nombre
        /// </summary>
        /// <param name="user">nombre de usuario del cliente</param>
        /// <returns>Regresa un objeto de tipo Account</returns>
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

        /// <summary>
        /// Método que valida si un usuario ya ha iniciado sesión anteriormente
        /// </summary>
        /// <param name="userName">Nomre de usuario del jugador</param>
        /// <returns>regresa un valor true si ya ha iniciado sesión</returns>
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
