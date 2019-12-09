using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace LismanService {
    /// <summary>
    /// Implementación del la interfaz de IChatManager
    /// </summary>
    public partial class LismanService : IChatManager {

        static Dictionary<String, IChatManagerCallBack> connectionChatService = new Dictionary<String, IChatManagerCallBack>();
        
        private Func<IChatManagerCallBack> callbackChannel;
        public LismanService(Func<IChatManagerCallBack> callbackCreator)
        {
            this.callbackChannel = callbackCreator ?? throw new ArgumentNullException("callbackCreator");
            
        }

        public LismanService() { }
        
        /// <summary>
        /// Método que une a un jugador a un Chat
        /// </summary>
        /// <param name="user">nombre de ususario del jugador</param>
        /// <param name="idgame">identificador del juego al que pertenece</param>
        public void JoinChat(string user, int idgame)
        {
                        
            var connection = OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();
            
            if (connectionChatService.ContainsKey(user)) {
                
                connectionChatService[user] = connection;
            } else {
                connectionChatService.Add(user, connection);
            }
            


            foreach (var userGame in listGamesOnline[idgame]) {
                try
                {
                    if (connectionChatService[userGame] != null)
                    {
                        connectionChatService[userGame].NotifyNumberPlayers(listGamesOnline[idgame].Count);
                        if (userGame != user)
                        {
                            connectionChatService[userGame].NotifyJoinedPlayer(user);

                        }
                    }
                    
                }catch(CommunicationException ex)
                {
                    Logger.log.Error("JoinChat, " + ex);
                }
                
                
            }

            
        }

        /// <summary>
        /// Método que permite a un jugador dejar el chat en el que se encuentra ¿
        /// </summary>
        /// <param name="user">nombre de ususario del jugador</param>
        /// <param name="idgame">identificador del juego al que pertenece</param>
        public void LeaveChat(string user, int idgame) {
            if (callbackChannel == null)
            {
                callbackChannel = () => OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();

            }
            this.callbackChannel().NotifyNumberPlayers(listGamesOnline[idgame].Count);
            this.callbackChannel().NotifyLeftPlayer(user);

            try {
                foreach (var userGame in listGamesOnline[idgame]) {
                    try
                    {
                        if (connectionChatService[userGame] != null)
                        {
                            connectionChatService[userGame].NotifyNumberPlayers(listGamesOnline[idgame].Count);
                            connectionChatService[userGame].NotifyLeftPlayer(user);
                        }
                       
                    }
                    catch (CommunicationException ex)
                    {
                        Logger.log.Error("LeaveChat, " + ex);
                    }

                }
            } catch (KeyNotFoundException ex) {
                Logger.log.Error("Function LeaveChat, " + ex);
            }

        }

        /// <summary>
        /// Métdod que permite enviar un mensaje a todos los miembros del Chat
        /// </summary>
        /// <param name="message">Objeto de tipo mensaje que tiene como atributos el mensaje y el usuario que lo envió</param>
        /// <param name="idgame">identificador del juego al que pertenece</param>
        public void SendMessage(Message message, int idgame)
        {
            if (callbackChannel == null)
            {
                callbackChannel = () => OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();

            }
            this.callbackChannel().NotifyMessage(message);

            foreach (var userGame in listGamesOnline[idgame])
                {
                    try
                    {
                    if (connectionChatService[userGame] != null)
                    {
                        connectionChatService[userGame].NotifyMessage(message);
                    }

                       
                    }
                    catch (CommunicationException ex)
                    {
                        Logger.log.Error("SendMessage, " + ex);
                    }

                }
            

        }

        /// <summary>
        /// Método que manda la señal para iniciar el juego en todos los Clientes
        /// </summary>
        /// <param name="user">nombre de ususario del jugador</param>
        /// <param name="idgame">identificador del juego al que pertenece</param>
        public void StartGame(string user, int idgame)
        {
            if (callbackChannel == null)
            {
                callbackChannel = () => OperationContext.Current.GetCallbackChannel<IChatManagerCallBack>();               
                
            }
            this.callbackChannel().InitGame();





            ReadMapGame();
           try
            {
                if (!multiplayerGameInformation.ContainsKey(idgame))
                {
                    Game informationGame = new Game
                    {

                        gameMap = GAMEMAP,
                        lismanUsers = new Dictionary<int, InformationPlayer>()

                    };
                    multiplayerGameInformation.Add(idgame, informationGame);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Info("Clave de diccionario no encontrada" + ex.Message);          
            }
           
                foreach (var userGame in listGamesOnline[idgame])
                {

                    if (AssignColorPlayer(idgame, userGame))
                    {
                        try
                        {
                        if (connectionChatService[userGame] != null)
                        {
                            connectionChatService[userGame].InitGame();
                        }
                            
                        }
                        catch (CommunicationException ex)
                        {
                            Logger.log.Error("StartGame, " + ex);
                        }

                    }

                }
                Console.WriteLine("Game started ID:{0}, at:{1}", idgame, DateTime.Now);
        }

        /// <summary>
        /// Método que asigna el color que utilizara cada jugador en la partida
        /// </summary>
        /// <param name="idgame">identificador del juego al que pertenece</param>
        /// <param name="user">nombre de usuario del jugador</param>
        /// <returns></returns>
        public bool AssignColorPlayer(int idgame, String user)
        {
            InformationPlayer infoPlayer = new InformationPlayer();
            int index = listGamesOnline[idgame].FindIndex(u => u == user);
            bool result = false;
            int colorAssigned = 0;
            switch (index)
            {
                case 0:
                    colorAssigned =  LISMANYELLOW;
                    infoPlayer.initialDirecction = "RIGHT";
                    break;
                case 1:
                    colorAssigned = LISMANRED;
                    infoPlayer.initialDirecction = "LEFT";
                    break;
                case 2:
                    colorAssigned = LISMANBLUE;
                    infoPlayer.initialDirecction = "RIGHT";
                    break;
                case 3:
                    colorAssigned = LISMANGREEN;
                    infoPlayer.initialDirecction = "LEFT";
                    break;
            }
            infoPlayer.userLisman = user;
            infoPlayer.lifesLisman = 3;
            infoPlayer.hasPower = false;
            infoPlayer.isLive = true;
            infoPlayer.scoreLisman = 0;
            multiplayerGameInformation[idgame].lismanUsers.Add(colorAssigned, infoPlayer);

            if (colorAssigned != 0)
            {
                result = true;
            }
            
            return result;
        }

        



    }
}
