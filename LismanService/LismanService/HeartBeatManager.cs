using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Windows.Threading;

namespace LismanService
{
    public partial class LismanService : IHeartBeat
    {
        static Dictionary<String, IHeartBeatCallBack> logginsConnections = new Dictionary<string, IHeartBeatCallBack>();
        
        DispatcherTimer timerReconnection = new DispatcherTimer();
        public LismanService()
        {
            timerReconnection.Tick += new EventHandler(Hola);
            timerReconnection.Interval = new TimeSpan(0, 0, 0,5);
            timerReconnection.Start();
        }


        
        public void NewLogin(string username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IHeartBeatCallBack>();
            if (logginsConnections.ContainsKey(username))
            {
                logginsConnections[username] = connection;
            }
            else
            {
                logginsConnections.Add(username, connection);
            }       
            

            
           
        }

        public void Hola(Object sender, EventArgs e)
        {
            Console.WriteLine("Hola");
            //Thread.Sleep(1000);
        }
        public static void TheyLive()
        {
            Console.WriteLine("Hilito");
            foreach (KeyValuePair<string, IHeartBeatCallBack> user in logginsConnections)
            {
                try
                {
                    user.Value.NotifyOk();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(user.Key + " se ha desconectado");
                    QuitConnection(user.Key);
                }
                
            }
            
        }

        public static void QuitConnection(string username)
        {
            logginsConnections.Remove(username);
        }
    }
}
