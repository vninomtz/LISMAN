using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lisman
{
    class SingletonConnection: LismanService.IHeartBeatCallback
    {
        InstanceContext instance;
        LismanService.HeartBeatClient clientLoggins;
        static SingletonConnection singletonConnection = null;


        private SingletonConnection()
        {
            instance = new InstanceContext(this);
            clientLoggins = new LismanService.HeartBeatClient(instance);
            clientLoggins.NewLogin(SingletonAccount.getSingletonAccount().User);
        }

        public static void CreateConnection()
        {
            if (singletonConnection == null)
            {
                singletonConnection = new SingletonConnection();
            }   
        }


        public void NotifyOk()
        {
            Console.WriteLine(SingletonAccount.getSingletonAccount().User + " Sigue online");
        }

    }
}
