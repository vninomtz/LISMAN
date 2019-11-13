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
        void PrintInformationPlayers(Dictionary<String,InformationPlayer> listPlayers);
        [OperationContract(IsOneWay = true)]
        void NotifyColorPlayer(int colorPlayer, String user);
        [OperationContract(IsOneWay = true)]
        void NotifyLismanMoved(int colorPlayer, int positionX, int positionY);
        [OperationContract(IsOneWay = true)]
        void NotifyDisappearedPowerPill(int positionX, int positionY);
        [OperationContract(IsOneWay = true)]
        void NotifyUpdateScore(int colorPlayer, int scorePlayer);
        [OperationContract(IsOneWay = true)]
        void NotifyUpdateLifes(int colorPlayer, int lifePlayer);
        [OperationContract(IsOneWay = true)]
        void NotifyPlayerIsDead(int colorPlayer);
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public int[,] gameMap  { get; set; }
        [DataMember]
        public Dictionary<String, InformationPlayer> lismanUsers { get; set; }
        
    }

    [DataContract]
    public class InformationPlayer
    {
        [DataMember]
        public int colorLisman { get; set; }
        [DataMember]
        public int lifesLisman { get; set; }
        [DataMember]
        public int scoreLisman { get; set; }
        [DataMember]
        public bool hasPower { get; set; }
    }
        
}



