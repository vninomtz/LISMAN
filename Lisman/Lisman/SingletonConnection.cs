using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lisman
{
    class SingletonConnection: LismanService.IHeartBeatCallback
    {
        InstanceContext instance;
        LismanService.HeartBeatClient clientLoggins;
        static SingletonConnection singletonConnection = null;
        DispatcherTimer connectionTimer = new DispatcherTimer();
        

        private SingletonConnection()
        {
            instance = new InstanceContext(this);
            clientLoggins = new LismanService.HeartBeatClient(instance);
            clientLoggins.NewLogin(SingletonAccount.getSingletonAccount().User);
            connectionTimer.Tick += new EventHandler(Hola);
            connectionTimer.Interval = new TimeSpan(0, 0, 0, 1);
            connectionTimer.Start();
            Console.WriteLine("Inicio de timer");
        }

        public void Hola(Object ob, EventArgs e)
        {
            clientLoggins.ImLive(SingletonAccount.getSingletonAccount().User);
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
