using System;
using System.ServiceModel;

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

    }


}
