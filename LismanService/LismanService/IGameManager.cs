using System;
using System.ServiceModel;


namespace LismanService {

    [ServiceContract]
    public interface IGameManager {
        [OperationContract]
        int CreateGame(String user);
        [OperationContract]
        int JoinGame(String user);
        [OperationContract]
        int LeaveGame(String user, int game);
    }
}
