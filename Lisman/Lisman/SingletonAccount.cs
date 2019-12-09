using System.ServiceModel;

namespace Lisman {
    public class SingletonAccount {
        private static LismanService.Account account = null;
        
        protected SingletonAccount() {
            
        }

        /// <summary>
        /// Permite que solo se pueda crear una instancia de la cuenta 
        /// </summary>
        /// <param name="accountReceived">Cuenta de a cual se creara la instancia</param>
        public static void setSingletonAccount(LismanService.Account accountReceived) {

            if (account == null) {
                account = accountReceived;
            }
        }

        /// <summary>
        /// Retorna la cuenta que fue asignada 
        /// </summary>
        /// <returns></returns>
        public static LismanService.Account getSingletonAccount() {
            if (account != null) {
                return account;
            } else {
                return null;
            }
        }
    }

}
