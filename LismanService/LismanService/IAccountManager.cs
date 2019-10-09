using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LismanService {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountManager" in both code and config file together.

    [ServiceContract]
    public interface IAccountManager {
        [OperationContract]
        int AddAccount(Account account);
        [OperationContract]
        List<Account> GetAccounts();
        [OperationContract]
        Account GetAccountById(int id);
        [OperationContract]
        Account LoginAccount(String user, String password);
        [OperationContract]
        List<Record> GetRecords();
        [OperationContract]
        bool UserNameExists(String username);
        [OperationContract]
        bool EmailExists(String emailAdress);

    }

    [DataContract]
    public partial class Account {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Key_confirmation { get; set; }
        [DataMember]
        public string Registration_date { get; set; }
        [DataMember]
        public virtual Player Player { get; set; }
        [DataMember]
        public virtual Record Record { get; set; }
    }
    public partial class Player {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string First_name { get; set; }
        [DataMember]
        public string Last_name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public virtual Account Account { get; set; }
    }

    public partial class Record {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Nullable<int> Mult_best_score { get; set; }
        [DataMember]
        public Nullable<int> Story_best_score { get; set; }
        [DataMember]
        public Nullable<int> Mult_games_played { get; set; }
        [DataMember]
        public Nullable<int> Mult_games_won { get; set; }
        [DataMember]
        public virtual Account Account { get; set; }
    }
}
