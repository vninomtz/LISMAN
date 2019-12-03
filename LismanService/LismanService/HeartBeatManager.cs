using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Timers;

namespace LismanService
{
    public partial class LismanService : IHeartBeat
    {
        static Dictionary<String, IHeartBeatCallBack> logginsConnections = new Dictionary<string, IHeartBeatCallBack>();
        static System.Timers.Timer timerConnection;

        public void InitializeThreadConnection()
        {
            timerConnection = new Timer(5000);
            timerConnection.Elapsed += VerifyConnection;
            timerConnection.AutoReset = true;
            timerConnection.Enabled = true;
        }

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

        public void VerifyConnection(Object sender, EventArgs e)
        {
            if(logginsConnections.Count != 0)
            {
                foreach(var login in logginsConnections)
                {
                   var channel = (ICommunicationObject)login.Value;
                    if(channel.State == CommunicationState.Faulted)
                    {
                        Console.WriteLine("Eliminando a " + login.Key + " de la conexión");
                        logginsConnections.Remove(login.Key);
                    }
                    else
                    {
                        try
                        {
                            login.Value.NotifyOk();
                            Console.WriteLine("Sigue conectado " + login.Key);
                        }
                        catch(CommunicationException)
                        {
                            Console.WriteLine("Error en la conexión de  " + login.Key);
                        }catch(ObjectDisposedException ex)
                        {
                            Console.WriteLine("Error conexioón " + login.Key + " Error: " + ex.Message + "\nObjecto: " + ex.ObjectName);
                        }
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Sin conexiones");
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
