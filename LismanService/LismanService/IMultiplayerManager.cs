using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace LismanService {
    [ServiceContract(CallbackContract = typeof(IMultiplayerManagerCallBack))]
    public interface IMultiplayerManager {
        [OperationContract(IsOneWay = true)]
        void JoinMultiplayerGame(String user, int idgame);
        [OperationContract(IsOneWay = true)]
        void MoveLisman(int idGame, String user, int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY);
    }

    [ServiceContract]
    public  interface IMultiplayerManagerCallBack {
        [OperationContract(IsOneWay = true)]
        void PrintPlayer(String user, int life, int score);
        [OperationContract(IsOneWay = true)]
        void NotifyColorPlayer(int colorPlayer);
        [OperationContract(IsOneWay = true)]
        void NotifyLismanMoved(int colorPlayer, int positionX, int positionY);



    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public int[,] gameMap  { get; set; }
        [DataMember]
        public Dictionary<String, int> lismanUsers { get; set; }
        
    }
        
}



