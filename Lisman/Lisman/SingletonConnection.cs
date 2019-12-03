using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Timers;


namespace Lisman
{
    public class SingletonConnection: LismanService.IHeartBeatCallback
    {
        InstanceContext instance;
        static LismanService.HeartBeatClient clientLoggins;
        static SingletonConnection singletonConnection = null;
        static Timer timerConnection;


        private SingletonConnection()
        {
            instance = new InstanceContext(this);
            clientLoggins = new LismanService.HeartBeatClient(instance);
            
        }

        public static void NotifyConnection()
        {
            clientLoggins.ImLive(SingletonAccount.getSingletonAccount().User);
            Console.WriteLine("Me estoy ejecuntando");
        }

        public static void CreateConnection()
        {
            if (singletonConnection == null)
            {
                singletonConnection = new SingletonConnection();
                clientLoggins.NewLogin(SingletonAccount.getSingletonAccount().User);
                //NotifyConnection();
            }   
        }


        public void NotifyOk()
        {
            Console.WriteLine(SingletonAccount.getSingletonAccount().User + " Sigue online");
        }

    }
}
