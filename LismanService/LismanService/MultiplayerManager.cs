using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LismanService
{
    public partial class LismanService : IMultiplayerManager
    {

        static int[,] GAMEMAP = new int[24, 24];
        static int LISMANYELLOW = 3;
        static int LISMANBLUE = 4;
        static int LISMANRED = 5;
        static int LISMANGREEN = 6;


        static Dictionary<String, IMultiplayerManagerCallBack> connectionGameService = new Dictionary<String, IMultiplayerManagerCallBack>();
        static Dictionary<int, Game> multiplayerGameInformation = new Dictionary<int, Game>();
        public void JoinMultiplayerGame(string user, int idgame)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IMultiplayerManagerCallBack>();
            if (connectionGameService.ContainsKey(user))
            {
                connectionGameService[user] = connection;
            }
            else
            {
                connectionGameService.Add(user, connection);
            }
            connectionGameService[user].NotifyColorPlayer(multiplayerGameInformation[idgame].lismanUsers[user].colorLisman, user);
            connectionGameService[user].PrintInformationPlayers(multiplayerGameInformation[idgame].lismanUsers);
            /*int index = listGamesOnline[idgame].FindIndex(u => u == user);

            switch (index)
            {
                case 0:
                    multiplayerGameInformation[idgame].lismanUsers.Add(user, LISMANYELLOW);
                    connectionGameService[user].NotifyColorPlayer(LISMANYELLOW, user);
                    break;
                case 1:
                    multiplayerGameInformation[idgame].lismanUsers.Add(user, LISMANBLUE);
                    connectionGameService[user].NotifyColorPlayer(LISMANBLUE, user);
                    break;
                case 2:
                    multiplayerGameInformation[idgame].lismanUsers.Add(user, LISMANRED);
                    connectionGameService[user].NotifyColorPlayer(LISMANRED, user);
                    break;
                case 3:
                    multiplayerGameInformation[idgame].lismanUsers.Add(user, LISMANGREEN);
                    connectionGameService[user].NotifyColorPlayer(LISMANGREEN, user);
                    break;
            }*/
        }


        public void MoveLisman(int idgame,String user, int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY)
        {
            foreach (var userGame in listGamesOnline[idgame])
            {
                try
                {
                    connectionGameService[userGame].NotifyLismanMoved(multiplayerGameInformation[idgame].lismanUsers[user].colorLisman, finalPositionX, finalPositionY);
                } catch (CommunicationObjectAbortedException e)
                {
                    Console.WriteLine("Error en la conexión con el usuario:" + userGame + ". Error: " + e.Message);
                }
               
            }
        }
    }
}
