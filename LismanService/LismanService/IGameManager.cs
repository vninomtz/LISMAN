using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace LismanService {

    [ServiceContract (CallbackContract = typeof(IGameManagerCallBack))]
    public interface IGameManager {
        [OperationContract(IsOneWay = true)]
        void CreateGame(String user);
        [OperationContract(IsOneWay = true)]
        void JoinGame(String user);
        [OperationContract(IsOneWay = true)]
        void LeaveGame(String user, int game);
    }

    [ServiceContract]
    public interface IGameManagerCallBack {
        [OperationContract(IsOneWay = true)]
        void NotifyJoinedUser(String user);
        [OperationContract(IsOneWay = true)]
        void NotifyJoined(int game, String user);

        [OperationContract(IsOneWay = true)]
        void NotifyErrorMessage(int error);
        [OperationContract(IsOneWay = true)]
        void NotifyLeaveUser(String user);
        [OperationContract(IsOneWay = true)]
        void NotifyGameCreated(int message);

    }
}
