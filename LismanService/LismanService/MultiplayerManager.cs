using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LismanService
{
    public partial class LismanService : IMultiplayerManager
    {

        static int[,] GAMEMAP = new int[24, 23];
        const int EMPTYBOX = 1;
        const int POWERPILL = 2;
        const int LISMANYELLOW = 3;
        const int LISMANBLUE = 4;
        const int LISMANRED = 5;
        const int LISMANGREEN = 6;
        const int GHOST = 8;

        int[] COORDINATESLISMANYELLOW = new int[2] { 1, 1 };
        int[] COORDINATESLISMANRED = new int[2] { 22, 1 };
        int[] COORDINATESLISMANBLUE = new int[2] { 1, 21 };
        int[] COORDINATESLISMANGREEN = new int[2] { 22, 21 };

        const int POINTSPOWERPILL = 100;
        const int POINTSEATLISMAN = 200;

        String parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;


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
            
        }


        public void MoveLisman(int idgame,String user, int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY)
        {
            int valueBox = GetValueBox(idgame, finalPositionX, finalPositionY);
            String userEnemy = null;

            switch (valueBox)
            {
                case EMPTYBOX:
                    MoveLismanToNewPosition(idgame, user,initialPositionX,initialPositionY, finalPositionX, finalPositionY);
                    break;
                case POWERPILL:
                    EatPowerPill(idgame, user, initialPositionX, initialPositionY, finalPositionX, finalPositionY);
                    break;
                case LISMANYELLOW:
                    userEnemy = GetUserByColorLisman(idgame, LISMANYELLOW);
                    EatLismanEnemy(idgame, user, userEnemy, initialPositionX, initialPositionY, finalPositionX, finalPositionY);
                    break;
                case LISMANRED:
                    userEnemy = GetUserByColorLisman(idgame, LISMANRED);
                    EatLismanEnemy(idgame, user, userEnemy, initialPositionX, initialPositionY, finalPositionX, finalPositionY);
                    break;
                case LISMANBLUE:
                    userEnemy = GetUserByColorLisman(idgame, LISMANBLUE);
                    EatLismanEnemy(idgame, user, userEnemy, initialPositionX, initialPositionY, finalPositionX, finalPositionY);
                    break;
                case LISMANGREEN:
                    userEnemy = GetUserByColorLisman(idgame, LISMANGREEN);
                    EatLismanEnemy(idgame, user, userEnemy, initialPositionX, initialPositionY, finalPositionX, finalPositionY);
                    break;

            }

        }
        private String GetUserByColorLisman(int idgame,int colorLisman)
        {
            String user = null;
            foreach (var userGame in listGamesOnline[idgame])
            {
                if (colorLisman == multiplayerGameInformation[idgame].lismanUsers[userGame].colorLisman){
                    user = userGame;
                }
            }
            return user;
        }

        private void EatLismanEnemy(int idgame, String lismanAlive, String lismanDead, int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY)
        {
            int colorLismanAlive = multiplayerGameInformation[idgame].lismanUsers[lismanAlive].colorLisman;
            int colorLismanDead = multiplayerGameInformation[idgame].lismanUsers[lismanDead].colorLisman;

            int scoreLismanAlive = UpdateScore(idgame, lismanAlive, POINTSEATLISMAN);
            int lifesLismanDead = multiplayerGameInformation[idgame].lismanUsers[lismanDead].lifesLisman;
            bool isDeadLismanDead = false;
            int[] positionInitialLismanDead = new int[2];

            UpdateGameMap(idgame, EMPTYBOX, initialPositionX, initialPositionY);
            UpdateGameMap(idgame, colorLismanAlive, finalPositionX, finalPositionY);

            if (multiplayerGameInformation[idgame].lismanUsers[lismanAlive].lifesLisman > 1)
            {
                switch (colorLismanDead)
                {
                    case LISMANYELLOW:
                        positionInitialLismanDead[0] = COORDINATESLISMANYELLOW[0];
                        positionInitialLismanDead[1] = COORDINATESLISMANYELLOW[1];
                        break;
                    case LISMANRED:
                        positionInitialLismanDead[0] = COORDINATESLISMANRED[0];
                        positionInitialLismanDead[1] = COORDINATESLISMANRED[1];
                        break;
                    case LISMANBLUE:
                        positionInitialLismanDead[0] = COORDINATESLISMANBLUE[0];
                        positionInitialLismanDead[1] = COORDINATESLISMANBLUE[1];
                        break;
                    case LISMANGREEN:
                        positionInitialLismanDead[0] = COORDINATESLISMANGREEN[0];
                        positionInitialLismanDead[1] = COORDINATESLISMANGREEN[1];
                        break;
                }

                UpdateGameMap(idgame, colorLismanDead, positionInitialLismanDead[0], positionInitialLismanDead[1]);
                lifesLismanDead = UpdateSubtractLifes(idgame, lismanDead);

            }
            else
            {
                isDeadLismanDead = true;
            }

            foreach (var userGame in listGamesOnline[idgame])
            {
                try
                {
                    connectionGameService[userGame].NotifyLismanMoved(colorLismanAlive, finalPositionX, finalPositionY);
                    connectionGameService[userGame].NotifyLismanMoved(colorLismanDead, positionInitialLismanDead[0], positionInitialLismanDead[1]);
                    connectionGameService[userGame].NotifyUpdateScore(colorLismanAlive, scoreLismanAlive);
                    connectionGameService[userGame].NotifyUpdateLifes(colorLismanDead, lifesLismanDead);
                }
                catch (CommunicationException e)
                {
                    Console.WriteLine("Error en la conexión con el usuario:" + userGame + ". Error: " + e.Message);
                }

            }
            if (isDeadLismanDead)
            {
                foreach (var userGame in listGamesOnline[idgame])
                {
                    try
                    {
                        connectionGameService[userGame].NotifyPlayerIsDead(colorLismanDead);
                    }
                    catch (CommunicationException e)
                    {
                        Console.WriteLine("Error en la conexión con el usuario:" + userGame + ". Error: " + e.Message);
                    }

                }
                listGamesOnline[idgame].RemoveAll(u => u == lismanDead);
            }

        }

        private void UpdateGameMap(int idgame, int newValue, int positionX, int positionY)
        {
            multiplayerGameInformation[idgame].gameMap[positionX, positionY] = newValue;
        }

        private int UpdateSubtractLifes(int idgame, String user)
        {
            return multiplayerGameInformation[idgame].lismanUsers[user].lifesLisman -= 1;
        }

        private int UpdateScore(int idgame, String user, int points)
        {
           return multiplayerGameInformation[idgame].lismanUsers[user].scoreLisman += points;
        }
        
        private void MoveLismanToNewPosition(int idgame, String user, int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY)
        {
            int colorLisman = multiplayerGameInformation[idgame].lismanUsers[user].colorLisman;
            UpdateGameMap(idgame, EMPTYBOX, initialPositionX, initialPositionY);
            UpdateGameMap(idgame, colorLisman , finalPositionX, finalPositionY);
            foreach (var userGame in listGamesOnline[idgame])
            {
                try
                {
                    connectionGameService[userGame].NotifyLismanMoved(colorLisman, finalPositionX, finalPositionY);
                }
                catch (CommunicationException e)
                {
                    Console.WriteLine("Error en la conexión con el usuario:" + userGame + ". Error: " + e.Message);
                }

            }
        }
        private void EatPowerPill(int idgame, String user, int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY)
        {
            int colorLisman = multiplayerGameInformation[idgame].lismanUsers[user].colorLisman;
            UpdateGameMap(idgame, EMPTYBOX, initialPositionX, initialPositionY);
            UpdateGameMap(idgame, colorLisman, finalPositionX, finalPositionY);
            int scoreLisman = UpdateScore(idgame, user, POINTSPOWERPILL);
            foreach (var userGame in listGamesOnline[idgame])
            {
                try
                {
                    connectionGameService[userGame].NotifyDisappearedPowerPill(finalPositionX, finalPositionY);
                    connectionGameService[userGame].NotifyLismanMoved(colorLisman, finalPositionX, finalPositionY);
                    connectionGameService[userGame].NotifyUpdateScore(colorLisman, scoreLisman);

                }
                catch (CommunicationException e)
                {
                    Console.WriteLine("Error en la conexión con el usuario:" + userGame + ". Error: " + e.Message);
                }

            }
        }

        private int GetValueBox(int idGame,int finalPositionX, int finalPositionY)
        {
            int result = -1;
            int valuePosition = multiplayerGameInformation[idGame].gameMap[finalPositionX, finalPositionY];
            switch (valuePosition)
            {
                case EMPTYBOX:
                    result = 1;
                    break;
                case POWERPILL:
                    result = 2;
                    break;
                case LISMANYELLOW:
                    result = 3;
                    break;
                case LISMANRED:
                    result = 4;
                    break;
                case LISMANBLUE:
                    result = 5;
                    break;
                case LISMANGREEN:
                    result = 6;
                    break;
                case GHOST:
                    result = 8;
                    break;
            }

            return result;
        }

        public void ReadMapGame()
        {
            using (StreamReader sr = new StreamReader(parentDirectory + "/Resources/Map.txt")) 
            {

                for (int i = 0; i <= 23; i++)
                {
                    for (int j = 0; j <= 22; j++)
                    {
                        int caracter = sr.Read();

                        if (caracter != -1)
                        {
                            switch (caracter)
                            {
                                case 48:
                                    GAMEMAP[i, j] = 0;
                                    break;
                                case 49:
                                    GAMEMAP[i, j] = 1;
                                    break;
                                case 50:
                                    GAMEMAP[i, j] = 2;
                                    break;
                                case 51:
                                    GAMEMAP[i, j] = 3;
                                    break;
                                case 52:
                                    GAMEMAP[i, j] = 4;
                                    break;
                                case 53:
                                    GAMEMAP[i, j] = 5;
                                    break;
                                case 54:
                                    GAMEMAP[i, j] = 6;
                                    break;
                                case 56:
                                    GAMEMAP[i, j] = 8;
                                    break;

                            }

                        }

                    }
                }

            }

        }
    }
}
