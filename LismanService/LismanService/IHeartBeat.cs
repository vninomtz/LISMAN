using System;
using System.ServiceModel;

namespace LismanService
{
    [ServiceContract(CallbackContract = typeof(IHeartBeatCallBack))]
    public interface IHeartBeat
    {


        [OperationContract(IsOneWay = true)]
        void NewLogin(string username);

        [OperationContract(IsOneWay = true)]
        void ImLive(String username);




    }


    [ServiceContract]
    public interface IHeartBeatCallBack
    {
       [OperationContract(IsOneWay = true)]
       void NotifyOk();

    }
}
