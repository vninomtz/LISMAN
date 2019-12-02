using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LismanService
{
    [ServiceContract(CallbackContract = typeof(IHeartBeatCallBack))]
    public interface IHeartBeat
    {


        [OperationContract(IsOneWay = true)]
        void NewLogin(string username);

        [OperationContract(IsOneWay = true)]
        void ImLive(String usernam);




    }


    [ServiceContract]
    public interface IHeartBeatCallBack
    {
       [OperationContract(IsOneWay = true)]
       void NotifyOk();

    }
}
