using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


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
