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
           
        }
        public void ImLive(String username)
        {
            Console.WriteLine(username + " Se encuentra vivo");

            try
            {
                logginsConnections[username].NotifyOk();
            }
            catch (Exception ex)
            {
                Logger.log.Info("Sudden disconnection user: " + username + " " + ex.Message);
                Console.WriteLine(username + " se ha desconectado");
                QuitConnection(username);

            }  
            
            
        }

        public static void QuitConnection(string username)
        {
            logginsConnections.Remove(username);
        }
    }
}
