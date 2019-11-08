using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace LismanService {
    [ServiceContract(CallbackContract = typeof(IMultiplayerManagerCallBack))]
    public interface IMultiplayerManager {
        [OperationContract(IsOneWay = true)]
        void JoinMultiplayerGame(String user, int idgame);
    }

    [ServiceContract]
    public  interface IMultiplayerManagerCallBack {
        [OperationContract(IsOneWay = true)]
        void PrintPlayer(String user, int life, int score);
        [OperationContract(IsOneWay = true)]
        void NotifyColorPlayer(int colorPlayer);


    }

    [DataContract]
    public class Game
    {

        [DataMember]
        public int[,] gameMap  { get; set; }
        
        [DataMember]
        public String lismanRed { get; set; }
        [DataMember]
        public String lismanBlue { get; set; }
        [DataMember]
        public String lismanYellow { get; set; }
        [DataMember]
        public String lismanGreen { get; set; }

    }
        
}



