using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisman {
    class SingletonAccount {
        private static LismanService.Account account = null;

        private SingletonAccount() {

        }

        public static void setSingletonAccount(LismanService.Account accountReceived) {
            if (account == null) {
               account = accountReceived;
            }
        }

        public static LismanService.Account getSingletonAccount() {
            if (account != null) {
                return account;
            } else {
                return null;
            }
        }
        
       

    }
}
