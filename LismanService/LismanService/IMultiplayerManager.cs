using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace LismanService {
    [ServiceContract(CallbackContract = typeof(IMultiplayerManagerCallBack))]
    public interface IMultiplayerManager {
        [OperationContract(IsOneWay = true)]
        void JoinMultiplayerGame(String user, int idGame);
        [OperationContract(IsOneWay = true)]
        void MoveLisman(LismanMovement movement);
        [OperationContract(IsOneWay = true)]
        void RemovePower(String user);
    }

    [ServiceContract]
    public interface IMultiplayerManagerCallBack {
        [OperationContract(IsOneWay = true)]
        void PrintInformationPlayers(Dictionary<int, InformationPlayer> listPlayers);
        [OperationContract(IsOneWay = true)]
        void NotifyColorPlayer(int colorPlayer, String user);
        [OperationContract(IsOneWay = true)]
        void NotifyLismanMoved(int colorPlayer, int positionX, int positionY,String goTo);
        [OperationContract(IsOneWay = true)]
        void NotifyDisappearedPowerPill(int positionX, int positionY);
        [OperationContract(IsOneWay = true)]
        void NotifyUpdateScore(int colorPlayer, int scorePlayer);
        [OperationContract(IsOneWay = true)]
        void NotifyUpdateLifes(int colorPlayer, int lifePlayer);
        [OperationContract(IsOneWay = true)]
        void NotifyPlayerIsDead(int colorPlayer);
        [OperationContract(IsOneWay = true)]
        void ReturnLismanToInitialPosition(int colorPlayer, int positionX, int positionY);
        [OperationContract(IsOneWay = true)]
        
        void UpdateLismanSpeed(int speed, bool hasPower);
        [OperationContract(IsOneWay = true)]
        void NotifyEndGame(int colorLisman);

    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public int[,] gameMap  { get; set; }
        [DataMember]
        public Dictionary<int, InformationPlayer> lismanUsers { get; set; }
        
    }

    [DataContract]
    public class InformationPlayer
    {
        [DataMember]
        public String userLisman { get; set; }
        [DataMember]
        public int lifesLisman { get; set; }
        [DataMember]
        public int scoreLisman { get; set; }
        [DataMember]
        public bool hasPower { get; set; }
        [DataMember]
        public bool isLive { get; set; }
        [DataMember]
        public String initialDirecction { get; set; }
    }

    [DataContract]
    public class LismanMovement
    {
        [DataMember]
        public int idGame { get; set; }
        [DataMember]
        public int colorLisman { get; set; }
        [DataMember]
        public int initialPositionX { get; set; }
        [DataMember]
        public int initialPositionY { get; set; }
        [DataMember]
        public int finalPositionX { get; set; }
        [DataMember]
        public int finalPositionY { get; set; }
        [DataMember]
        public String goTo { get; set; }
    }

}



