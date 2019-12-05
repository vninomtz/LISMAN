using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.ServiceModel;
using DataAccess;

namespace LismanService
{
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

        public void KillLisman(LismanMovement movement, int colorLismanEnemy, int scoreLisman, int lifesLismanEnemy)
        {
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if (player.Value.isLive == true)
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
        public void RespawnLisman(LismanMovement movement, int colorLismanEnemy, int scoreLisman, int lifesLismanEnemy)
        {
            String userLisman = multiplayerGameInformation[movement.idGame].lismanUsers[movement.colorLisman].userLisman;
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
                if(player.Value.isLive == true)
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
        public void FinishGame(int idGame)
        {
            foreach(KeyValuePair<int,InformationPlayer> player in multiplayerGameInformation[idGame].lismanUsers){
                if(player.Value.isLive == true)
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

        public bool PlayerWillDead (int idGame, int colorLisman)
        {
            bool result = false;
            if (multiplayerGameInformation[idGame].lismanUsers[colorLisman].lifesLisman == 0)
            {
                result = true;
            }

            return result;
        }
        public bool GameWillEnd(int idGame)
        {
            bool result = false;
            int playersLives = 0;
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[idGame].lismanUsers)
            {
                if(player.Value.isLive == true)
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

        public void UpdateGameMap(int idgame, int newValue, int positionX, int positionY)
        {
            multiplayerGameInformation[idgame].gameMap[positionX, positionY] = newValue;
        }

        public int UpdateSubtractLifes(int idGame, int colorLisman)
        {
            int lifesLisman = multiplayerGameInformation[idGame].lismanUsers[colorLisman].lifesLisman;
            if (lifesLisman > 0)
            {
                lifesLisman = multiplayerGameInformation[idGame].lismanUsers[colorLisman].lifesLisman -= 1;
            }
           
            
            return lifesLisman;
        }

        public int UpdateScore(int idgame, int colorLisman, int points)
        {
           return multiplayerGameInformation[idgame].lismanUsers[colorLisman].scoreLisman += points;
        }
        
        private void MoveLismanToNewPosition(LismanMovement movement)
        {
            
            UpdateGameMap(movement.idGame, EMPTYBOX, movement.initialPositionX, movement.initialPositionY);
            UpdateGameMap(movement.idGame, movement.colorLisman , movement.finalPositionX, movement.finalPositionY);
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if(player.Value.isLive == true && connectionGameService[player.Value.userLisman] != null)
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
        private void EatPowerPill(LismanMovement movement)
        {
            UpdateGameMap(movement.idGame, EMPTYBOX, movement.initialPositionX, movement.initialPositionY);
            UpdateGameMap(movement.idGame, movement.colorLisman, movement.finalPositionX, movement.finalPositionY);
            int scoreLisman = UpdateScore(movement.idGame, movement.colorLisman, POINTSPOWERPILL);
            string userName = multiplayerGameInformation[movement.idGame].lismanUsers[movement.colorLisman].userLisman;
            connectionGameService[userName].UpdateLismanSpeed(SPEEDPOWERFUL, true);
            foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[movement.idGame].lismanUsers)
            {
                if(player.Value.isLive == true)
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

        public void RemovePower(String user)
        {
            connectionGameService[user].UpdateLismanSpeed(SPEEDNORMAL,false);
        }

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

         public void ExitGame(int idGame, int colorLisman, int positionX, int positionY)
         {
            int playersLives = 0;
              UpdateGameMap(idGame, EMPTYBOX, positionX, positionY);
              foreach (KeyValuePair<int, InformationPlayer> player in multiplayerGameInformation[idGame].lismanUsers)
              {
                  if (player.Value.isLive == true)
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

        public void Reconntection (String userLisman)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IMultiplayerManagerCallBack>();
            if (connectionGameService.ContainsKey(userLisman))
            {
                if (connectionGameService[userLisman] == null)
                {
                    connectionGameService[userLisman] = connection;
                    
                }
                
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
