using System;
using System.Collections.Generic;
using System.ServiceModel;

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
                Console.WriteLine("Se guardo la conexion de " + username);
            }
            else
            {
                logginsConnections.Add(username, connection);
            }
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
