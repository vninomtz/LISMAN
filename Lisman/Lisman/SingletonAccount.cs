namespace Lisman {
     static class SingletonAccount {
        private static LismanService.Account account = null;     

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
