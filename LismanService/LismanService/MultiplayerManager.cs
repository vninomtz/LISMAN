using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.ServiceModel;
using DataAccess;

namespace LismanService
{
    /// <summary>
    /// Implementación de la interfaz IMultiplayerManager
    /// </summary>
    public partial class LismanService : IMultiplayerManager
    {

        static int[,] GAMEMAP = new int[24, 23];
        const int EMPTYBOX = 1;
        const int POWERPILL = 2;
        const int LISMANYELLOW = 3;
        const int LISMANRED = 4;
        const int LISMANBLUE = 5;
        const int LISMANGREEN = 6;
        const int GHOST = 8;

        int[] COORDINATESLISMANYELLOW = new int[2] { 1, 1 };
        int[] COORDINATESLISMANRED = new int[2] { 22, 1 };
        int[] COORDINATESLISMANBLUE = new int[2] { 1, 21 };
        int[] COORDINATESLISMANGREEN = new int[2] { 22, 21 };

        const int POINTSPOWERPILL = 100;
        const int POINTSEATLISMAN = 200;
        const int SPEEDNORMAL = 300;
        const int SPEEDPOWERFUL = 210;
       

        String parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;


        static Dictionary<String, IMultiplayerManagerCallBack> connectionGameService = new Dictionary<String, IMultiplayerManagerCallBack>();
        static Dictionary<int, Game> multiplayerGameInformation = new Dictionary<int, Game>();


       /// <summary>
       /// Métododo que permite al usuario unirse a una partida multijugador
       /// con su callback
       /// </summary>
       /// <param name="user">nombre de usuario del jugador</param>
       /// <param name="idGame">identificador del juego al que pertenece el jugador</param>
        public void JoinMultiplayerGame(string user, int idGame)
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
            try
            {
                connectionGameService[user].NotifyColorPlayer(GetColorLismanByUser(idGame, user), user);
                connectionGameService[user].PrintInformationPlayers(multiplayerGameInformation[idGame].lismanUsers);

                SaveLastUpdate(idGame);
            }catch(CommunicationException ex)
            {
                Logger.log.Error("JoinMultiplayerGame, " + ex);
            }
           
        }

        /// <summary>
        /// Método que permite obtener el color que tiene asigando un jugador
        /// </summary>
        /// <param name="idGame">identificador del juego al que pertenece</param>
        /// <param name="user">nombre de usuario del jugador</param>
        /// <returns>un entero con el color del jugador</returns>
        public int GetColorLismanByUser(int idGame, String user)
        {
            int colorLisman = 0;
            foreach(KeyValuePair<int, InformationPlayer> listUsers in multiplayerGameInformation[idGame].lismanUsers)
            {
                if(listUsers.Value.userLisman == user)
                {
                    colorLisman = listUsers.Key;
                }
            }
            return colorLisman;
        }

        /// <summary>
        /// Método que recibe un objeto de tipo Movement por parte de un cliente 
        /// para solicitar se movido en el mapa
        /// </summary>
        /// <param name="movement">objeto de tipo Movement que tiene conocimiento del 
        /// movimiento del jugador</param>
        public void MoveLisman(LismanMovement movement)
        {
            int valueBox = GetValueBox(movement.idGame, movement.finalPositionX, movement.finalPositionY);
            switch (valueBox)
            {
                case EMPTYBOX:
                    MoveLismanToNewPosition(movement);
                    break;
                case POWERPILL:
                    EatPowerPill(movement);
                    break;
                default:
                    EatLismanEnemy(movement, valueBox);
                    break;

            }
        }

        /// <summary>
        /// Método que permite comer a un jugador 
        /// </summary>
        /// <param name="movement">Objeto de tipo Movement</param>
        /// <param name="colorLismanEnemy">Color del jugador el cual será comido</param>
        private void EatLismanEnemy(LismanMovement movement, int colorLismanEnemy)
        {
            int scoreLisman = UpdateScore(movement.idGame, movement.colorLisman, POINTSEATLISMAN);
            int lifesLismanEnemy = UpdateSubtractLifes(movement.idGame, colorLismanEnemy);
            UpdateGameMap(movement.idGame, EMPTYBOX, movement.initialPositionX, movement.initialPositionY);
            UpdateGameMap(movement.idGame, movement.colorLisman, movement.finalPositionX, movement.finalPositionY);

            if (PlayerWillDead(movement.idGame, colorLismanEnemy))
            {
                if (GameWillEnd(movement.idGame))
                {
                    FinishGame(movement, colorLismanEnemy, scoreLisman, lifesLismanEnemy);
                }
                else
                {
                    KillLisman(movement, colorLismanEnemy, scoreLisman, lifesLismanEnemy);
                }
            }
            else
            {
                RespawnLisman(movement, colorLismanEnemy, scoreLisman, lifesLismanEnemy);
            }
        }

        /// <summary>
        /// Métdodo  que permite matar a un jugador y avisar a todos los demás
        /// </summary>
        /// <param name="movement">objeto de tipo Movement</param>
        /// <param name="colorLismanEnemy">color del jugador que murio </param>
        /// <param name="scoreLisman">puntaje del jugador vivo</param>
        /// <param name="lifesLismanEnemy">vidas del jugador que morirá</param>
        public void KillLisman(LismanMovement movement, int colorLismanEnemy, int scoreLisman, int lifesLismanEnemy)
        {
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if (player.Value.isLive)
                {
                    try
                    {
                        connectionGameService[player.Value.userLisman].NotifyPlayerIsDead(colorLismanEnemy);
                        connectionGameService[player.Value.userLisman].NotifyLismanMoved(movement.colorLisman, movement.finalPositionX, movement.finalPositionY, movement.goTo);
                        connectionGameService[player.Value.userLisman].NotifyUpdateScore(movement.colorLisman, scoreLisman);
                        connectionGameService[player.Value.userLisman].NotifyUpdateLifes(colorLismanEnemy, lifesLismanEnemy);
                    }
                    catch (CommunicationException e)
                    {
                        Console.WriteLine("Error en la conexión con el usuario:" + player.Value.userLisman + ". Error: " + e.Message);
                    }
                }
            }
            multiplayerGameInformation[movement.idGame].lismanUsers[colorLismanEnemy].isLive = false;
        }

        /// <summary>
        /// Método que permite reaparecer a un jugador que ha sido comido
        /// </summary>
        /// <param name="movement">Objeto de tipo Movement</param>
        /// <param name="colorLismanEnemy">Color del jugador que ha sido comido</param>
        /// <param name="scoreLisman">Puntaje del jugador que comio </param>
        /// <param name="lifesLismanEnemy">Vidas del jugador que ha sido comido</param>
        public void RespawnLisman(LismanMovement movement, int colorLismanEnemy, int scoreLisman, int lifesLismanEnemy)
        {
            String userLismanEnemy = multiplayerGameInformation[movement.idGame].lismanUsers[colorLismanEnemy].userLisman;
            int[] positionInitialLismanEnemy = GetInitialPositionsLisman(colorLismanEnemy);
            UpdateGameMap(movement.idGame, colorLismanEnemy, positionInitialLismanEnemy[0], positionInitialLismanEnemy[1]);

            try
            {
                connectionGameService[userLismanEnemy].ReturnLismanToInitialPosition(colorLismanEnemy, positionInitialLismanEnemy[0], positionInitialLismanEnemy[1]);
            }
            catch (CommunicationException e)
            {
                Console.WriteLine("Error en la conexión con el usuario:" + userLismanEnemy + ". Error: " + e.Message);
            }
            String initialDirectionLismanEnemy = multiplayerGameInformation[movement.idGame].lismanUsers[colorLismanEnemy].initialDirecction;
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if(player.Value.isLive)
                {
                    try
                    {
                        connectionGameService[player.Value.userLisman].NotifyLismanMoved(movement.colorLisman, movement.finalPositionX, movement.finalPositionY, movement.goTo);
                        connectionGameService[player.Value.userLisman].NotifyLismanMoved(colorLismanEnemy, positionInitialLismanEnemy[0], positionInitialLismanEnemy[1],
                            initialDirectionLismanEnemy);

                        connectionGameService[player.Value.userLisman].NotifyUpdateScore(movement.colorLisman, scoreLisman);
                        connectionGameService[player.Value.userLisman].NotifyUpdateLifes(colorLismanEnemy, lifesLismanEnemy);
                    }
                    catch (CommunicationException e)
                    {
                        Console.WriteLine("Error en la conexión con el usuario:" + player.Value.userLisman + ". Error: " + e.Message);
                    }
                }
               

            }
        }

        /// <summary>
        /// Método que obtiene las posiciones iniciales de cada jugador
        /// </summary>
        /// <param name="colorLisman">color del jugador</param>
        /// <returns>Arreglo de dos posiciones con las posiciones en X,Y</returns>
        public int[] GetInitialPositionsLisman(int colorLisman)
        {
            int[] positionInitialLisman = new int[2];
            switch (colorLisman)
            {
                case LISMANYELLOW:
                    positionInitialLisman[0] = COORDINATESLISMANYELLOW[0];
                    positionInitialLisman[1] = COORDINATESLISMANYELLOW[1];
                    break;
                case LISMANRED:
                    positionInitialLisman[0] = COORDINATESLISMANRED[0];
                    positionInitialLisman[1] = COORDINATESLISMANRED[1];
                    break;
                case LISMANBLUE:
                    positionInitialLisman[0] = COORDINATESLISMANBLUE[0];
                    positionInitialLisman[1] = COORDINATESLISMANBLUE[1];
                    break;
                case LISMANGREEN:
                    positionInitialLisman[0] = COORDINATESLISMANGREEN[0];
                    positionInitialLisman[1] = COORDINATESLISMANGREEN[1];
                    break;
            }
            return positionInitialLisman;
            
        }

        /// <summary>
        /// Métdodo que termina el juego
        /// </summary>
        /// <param name="movement">Objeto de tipo Movement</param>
        /// <param name="colorLismanEnemy">Color del jugador enemigo</param>
        /// <param name="scoreLisman">Puntaje del jugador vivo</param>
        /// <param name="lifesLismanEnemy">Vidas del jugador enemigo</param>
        public void FinishGame(LismanMovement movement, int colorLismanEnemy, int scoreLisman, int lifesLismanEnemy)
        {

            String userLisman = multiplayerGameInformation[movement.idGame].lismanUsers[movement.colorLisman].userLisman;
            String userLismanEnemy = multiplayerGameInformation[movement.idGame].lismanUsers[colorLismanEnemy].userLisman;
            try
            {
                connectionGameService[userLisman].NotifyPlayerIsDead(colorLismanEnemy);
                connectionGameService[userLismanEnemy].NotifyPlayerIsDead(colorLismanEnemy);

                connectionGameService[userLisman].NotifyUpdateScore(movement.colorLisman, scoreLisman);
                connectionGameService[userLisman].NotifyUpdateLifes(colorLismanEnemy, lifesLismanEnemy);

                connectionGameService[userLisman].NotifyEndGame(movement.colorLisman);
                SaveGame(movement.idGame, userLisman);
            }
            catch (CommunicationException ex)
            {
                Logger.log.Error("FinishGame, " + ex);
            }
           

        }

        /// <summary>
        /// Sobrescritura del método FinishGame, es utilizado cuando los jugadores se salen 
        /// del juego sin jugar
        /// </summary>
        /// <param name="idGame">identificador del juego</param>
        public void FinishGame(int idGame)
        {
            foreach(KeyValuePair<int,InformationPlayer> player in multiplayerGameInformation[idGame].lismanUsers){
                if(player.Value.isLive)
                {
                    try
                    {
                        connectionGameService[player.Value.userLisman].NotifyEndGame(player.Key);
                        SaveGame(idGame, player.Value.userLisman);
                        Console.WriteLine("Game finished ID:{0}, at:{1}", idGame, DateTime.Now);
                        return;
                    }
                    catch (CommunicationException e)
                    {
                        Console.WriteLine("FinishGame:" + player.Value.userLisman + ". Error: " + e.Message);
                    }
                }
            }

          

        }
        /// <summary>
        /// Métdodo que permite saber si un jugador morirá
        /// </summary>
        /// <param name="idGame">identificador del juego </param>
        /// <param name="colorLisman">color del jugador</param>
        /// <returns>Regresa un valor true si es que morirá</returns>
        public bool PlayerWillDead (int idGame, int colorLisman)
        {
            bool result = false;
            if (multiplayerGameInformation[idGame].lismanUsers[colorLisman].lifesLisman == 0)
            {
                result = true;
            }

            return result;
        }
        /// <summary>
        /// Métdodo que permite saber si un juego terminará
        /// </summary>
        /// <param name="idGame">identificador del juego</param>
        /// <returns>regresa true si el juego terminará</returns>
        public bool GameWillEnd(int idGame)
        {
            bool result = false;
            int playersLives = 0;
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[idGame].lismanUsers)
            {
                if(player.Value.isLive)
                {
                    playersLives += 1;
                }
            }
            if(playersLives == 2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Método que permite guardar la información del juego en la BD
        /// </summary>
        /// <param name="idgame">identificador del juego</param>
        /// <param name="userWinner">nombre de usuario ganador</param>
        private void SaveGame(int idgame, String userWinner)
        {
            try
            {
                using (var dataBase = new EntityModelContainer())
                {
                    var gameUpdate = dataBase.GameSet.Where(u => u.Id == idgame).FirstOrDefault();
                    if (gameUpdate.Id != 0)
                    {
                        gameUpdate.Last_update = DateTime.Now;
                        gameUpdate.Status = false;
                        gameUpdate.Game_over = DateTime.Now;
                        foreach (KeyValuePair<int, InformationPlayer> players in multiplayerGameInformation[idgame].lismanUsers)
                        {
                           var account = dataBase.AccountSet.Where(u => u.User == players.Value.userLisman).FirstOrDefault();
                            account.Record.Mult_best_score = players.Value.scoreLisman;
                            account.Record.Mult_games_played += 1;
                            if(players.Value.userLisman == userWinner)
                            {
                                account.Record.Mult_games_won += 1;
                            }
                            gameUpdate.Members.Add(account);
                        }
                        try
                        {
                            dataBase.SaveChanges();

                        }
                        catch (DbEntityValidationException ex)
                        {
                            Logger.log.Warn("SaveLastUpdate DataBase" + ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error("Funtion SaveLastUpdate: " + ex.Message);
            }
        }

        /// <summary>
        /// Método que permite actualizar la información del mapa del juego
        /// </summary>
        /// <param name="idgame">identificador del juego</param>
        /// <param name="newValue">valor nuevo que será guardado</param>
        /// <param name="positionX">coordenada en X para la matriz</param>
        /// <param name="positionY">coordenada en Y para la matriz</param>
        public void UpdateGameMap(int idgame, int newValue, int positionX, int positionY)
        {
            multiplayerGameInformation[idgame].gameMap[positionX, positionY] = newValue;
        }

        /// <summary>
        /// Métdodo que resta una vida a un jugador
        /// </summary>
        /// <param name="idGame">identificador del juego</param>
        /// <param name="colorLisman">color del jugador al que se le restará la vida</param>
        /// <returns></returns>
        public int UpdateSubtractLifes(int idGame, int colorLisman)
        {
            int lifesLisman = multiplayerGameInformation[idGame].lismanUsers[colorLisman].lifesLisman;
            if (lifesLisman > 0)
            {
                lifesLisman = multiplayerGameInformation[idGame].lismanUsers[colorLisman].lifesLisman -= 1;
            }
           
            
            return lifesLisman;
        }

        /// <summary>
        /// Métdodo que actualiza el puntaje de un jugador
        /// </summary>
        /// <param name="idgame">identificador del juego</param>
        /// <param name="colorLisman">color del jugador</param>
        /// <param name="points">puntos que serán sumados</param>
        /// <returns></returns>
        public int UpdateScore(int idgame, int colorLisman, int points)
        {
           return multiplayerGameInformation[idgame].lismanUsers[colorLisman].scoreLisman += points;
        }
        /// <summary>
        /// Metodo que mueve a un jugador a una nueva posicion
        /// </summary>
        /// <param name="movement">Objeto de tipo LismanMovement</param>
        private void MoveLismanToNewPosition(LismanMovement movement)
        {
            
            UpdateGameMap(movement.idGame, EMPTYBOX, movement.initialPositionX, movement.initialPositionY);
            UpdateGameMap(movement.idGame, movement.colorLisman , movement.finalPositionX, movement.finalPositionY);
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if(player.Value.isLive && connectionGameService[player.Value.userLisman] != null)
                {
                    try
                    {
                        connectionGameService[player.Value.userLisman].NotifyLismanMoved(movement.colorLisman, movement.finalPositionX, movement.finalPositionY, movement.goTo);
                    }
                    catch (CommunicationException e)
                    {
                        Console.WriteLine("Error en la conexión con el usuario:" + player.Value.userLisman + ". Error: " + e.Message);
                        connectionGameService[player.Value.userLisman] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Método que permite comserse una bolita a un jugador y obtener poder
        /// </summary>
        /// <param name="movement">Objeto de tipo LismanMovement</param>
        private void EatPowerPill(LismanMovement movement)
        {
            UpdateGameMap(movement.idGame, EMPTYBOX, movement.initialPositionX, movement.initialPositionY);
            UpdateGameMap(movement.idGame, movement.colorLisman, movement.finalPositionX, movement.finalPositionY);
            int scoreLisman = UpdateScore(movement.idGame, movement.colorLisman, POINTSPOWERPILL);
            string userName = multiplayerGameInformation[movement.idGame].lismanUsers[movement.colorLisman].userLisman;
            connectionGameService[userName].UpdateLismanSpeed(SPEEDPOWERFUL, true);
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if(player.Value.isLive)
                {
                    try
                    {
                        connectionGameService[player.Value.userLisman].NotifyDisappearedPowerPill(movement.finalPositionX, movement.finalPositionY);
                        connectionGameService[player.Value.userLisman].NotifyLismanMoved(movement.colorLisman, movement.finalPositionX, movement.finalPositionY, movement.goTo);
                        connectionGameService[player.Value.userLisman].NotifyUpdateScore(movement.colorLisman, scoreLisman);
                    }
                    catch (CommunicationException e)
                    {
                        Console.WriteLine("Error en la conexión con el usuario:" + player.Value.userLisman + ". Error: " + e.Message);
                    }
                }
            }
            
        }

        /// <summary>
        /// Métodod que permite obtener el valor de una celda del mapa
        /// </summary>
        /// <param name="idGame">identificador del juego</param>
        /// <param name="finalPositionX">posicion en X del mapa</param>
        /// <param name="finalPositionY">posicion en Y del mapa</param>
        /// <returns></returns>
        public int GetValueBox(int idGame, int finalPositionX, int finalPositionY)
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

        /// <summary>
        /// Método que permite leer los valores por defecto del mapa 
        /// </summary>
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

        /// <summary>
        /// Método que quita el poder a un jugador
        /// </summary>
        /// <param name="user">nombre de usuario del jugador</param>
        public void RemovePower(String user)
        {
            connectionGameService[user].UpdateLismanSpeed(SPEEDNORMAL,false);
        }

        /// <summary>
        /// Métdodo que permite guardar en la base de datos la última actualización de un
        /// juego
        /// </summary>
        /// <param name="idgame">identificador del juego</param>
        public void SaveLastUpdate(int idgame)
        {
            try
            {
                using(var dataBase = new EntityModelContainer())
                {
                    var gameUpdate = dataBase.GameSet.Where(u => u.Id == idgame).FirstOrDefault();
                    if(gameUpdate.Id != 0)
                    {
                        gameUpdate.Last_update = DateTime.Now;
                        try
                        {
                            dataBase.SaveChanges();

                        }
                        catch (DbEntityValidationException ex)
                        {
                            Logger.log.Warn("SaveLastUpdate DataBase" + ex);
                        }
                    }
                }
            }catch(Exception ex)
            {
                Logger.log.Error("Funtion SaveLastUpdate: " + ex.Message);
            }
        }
        /// <summary>
        /// Método que permite salirse del juego a un jugador
        /// </summary>
        /// <param name="idGame">identificador del juego</param>
        /// <param name="colorLisman">color del jugador</param>
        /// <param name="positionX">posicion en X del jugador</param>
        /// <param name="positionY">posicion en Y del jugador</param>
         public void ExitGame(int idGame, int colorLisman, int positionX, int positionY)
         {
            int playersLives = 0;
              UpdateGameMap(idGame, EMPTYBOX, positionX, positionY);
              foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[idGame].lismanUsers)
              {
                  if (player.Value.isLive)
                  {
                      try
                      {
                          connectionGameService[player.Value.userLisman].NotifyLismanLeaveGame(colorLisman);
                          playersLives++;
                      }
                      catch (CommunicationException e)
                      {
                          Console.WriteLine("Error en la conexión con el usuario:" + player.Value.userLisman + ". Error: " + e.Message);
                      }
                    
                  }
              }
              multiplayerGameInformation[idGame].lismanUsers[colorLisman].isLive = false;
            if(playersLives == 2)
            {
                FinishGame(idGame);
            }
          }

        /// <summary>
        /// Metdodo que permite al jugador reconectarse al juego si
        /// es que perdió su conexión
        /// </summary>
        /// <param name="userLisman">nombre de usuario del jugador</param>
        public void Reconntection (String userLisman)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IMultiplayerManagerCallBack>();
            if (connectionGameService.ContainsKey(userLisman) && connectionGameService[userLisman] == null)
            {
                connectionGameService[userLisman] = connection;
            }
        }


        public void InitializeTest()
        {
            List<String> playersList = new List<string>();
            playersList.Add("victor");
            playersList.Add("Alan");
            playersList.Add("Pepe");

            connectionChatService.Add("victor", null);
            connectionChatService.Add("Alan", null);
            connectionChatService.Add("Pepe", null);

            logginsConnections.Add("Alan", null);

            listGamesOnline.Add(1, playersList);
            listGamesOnline.Add(2, playersList);

            InformationPlayer info1 = new InformationPlayer();
            info1.userLisman = "Alan";
            info1.isLive = true;
            info1.lifesLisman = 0;
            info1.scoreLisman = 0;
            InformationPlayer info2 = new InformationPlayer();
            info2.userLisman = "Victor";
            info2.isLive = true;
            info2.lifesLisman = 2;
            info2.scoreLisman = 0;
            InformationPlayer info3 = new InformationPlayer();
            info3.userLisman = "Pablo";
            info3.isLive = true;
            info3.lifesLisman = 2;
            info3.scoreLisman = 0;
            InformationPlayer info4 = new InformationPlayer();
            info4.userLisman = "Gume";
            info4.isLive = true;
            info4.lifesLisman = 3;
            info4.scoreLisman = 0;

            ReadMapGame();

            Game game = new Game();
            game.gameMap = GAMEMAP;
            game.lismanUsers = new Dictionary<int, InformationPlayer>();
            game.lismanUsers.Add(3, info1);
            game.lismanUsers.Add(4, info2);
            game.lismanUsers.Add(5, info3);
            game.lismanUsers.Add(6, info4);

            multiplayerGameInformation.Add(1,game);

            Game game2 = new Game();
            game2.lismanUsers = new Dictionary<int, InformationPlayer>();
            multiplayerGameInformation.Add(2, game2);

            

        }
    }
}
