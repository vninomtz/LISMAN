using System;
using System.Windows;
using System.Windows.Input;
using Lisman.LismanService;
using System.ServiceModel;
namespace Lisman {
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window, LismanService.IChatManagerCallback {
        int COMPLETEPLAYERS = 4;
        InstanceContext instance = null;
        LismanService.ChatManagerClient client = null;
        int idGame;  
        
        /// <summary>
        /// Función que une al jugador al chat del juego al crear la ventana Lobby
        /// </summary>
        /// <param name="idGame"> Identificador del juego al cual el jugador se unirá </param>
        public Lobby(int idGame)
        {
            InitializeComponent();
            instance = new InstanceContext(this);
            this.idGame = idGame;
            client = new ChatManagerClient(instance);
            try
            {
                client.JoinChat(SingletonAccount.getSingletonAccount().User, idGame);
                textBlock_name_player.Text = SingletonAccount.getSingletonAccount().User;
                btn_startGame.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.server_connection_error);
                Logger.log.Warn("Function Lobby " + ex.Message);                
            }

            
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MultiplayerHome homeMultiplayer = new MultiplayerHome();
            homeMultiplayer.Show();
            this.Close();
        }

        private void btn_exitGame_Click(object sender, RoutedEventArgs e) {
            MultiplayerHome homeMultiplayer = new MultiplayerHome();
            LismanService.GameManagerClient clientGame = new LismanService.GameManagerClient();

            try
            {
                clientGame.LeaveGame(SingletonAccount.getSingletonAccount().User, idGame);
                client.LeaveChat(SingletonAccount.getSingletonAccount().User, idGame);
                homeMultiplayer.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.server_connection_error);
                Logger.log.Warn("Error en funcion exit game " + ex.Message);                                              
            }          
        }
        
        /// <summary>
        /// Función callback que escribe un mensaje enviado por el jugador en pantalla 
        /// </summary>
        /// <param name="message"> Objeto que contiene que el mensaje que el jugador escribió </param>
        public void NotifyMessage(Message message)
        {
            textBox_chat.Text += "\n" + message.userName + ": " + message.Text;

        }

        /// <summary>
        /// callback que escribe en pantalla el nombre del usuario que se acaba de unir al lobby del juego
        /// </summary>
        /// <param name="user">Nombre del usuario que se acaba de unir al lobby </param>
        public void NotifyJoinedPlayer(string user)
        {
            textBox_chat.Text += "\n" + user + Properties.Resources.joined_game;            
        }

        /// <summary>
        /// Obtiene el mensaje que escribio el jugador en el campo de texto y lo envia al servicio para que lo muestre en pantalla
        /// de los jugadores que estan en el lobby mediante callback
        /// </summary>
        public void SendMessage() {
            if (textBox_message.Text != String.Empty) {

                Message message = new Message {
                    Text = textBox_message.Text,
                    userName = SingletonAccount.getSingletonAccount().User
                };

                try
                {
                    client.SendMessage(message, idGame);
                    textBox_message.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Properties.Resources.server_connection_error);
                    Logger.log.Warn("Function sendMessage" + ex.Message); 
                }

                
                
            }
            

            
        }

        private void button_send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        /// <summary>
        /// Callback que actualiza el número de jugadores que se unen/abandonan la partida 
        /// </summary>
        /// <param name="numberPlayers">Número de jugadores que se encuentran esperando el juego en el lobby </param>
        public void NotifyNumberPlayers(int numberPlayers)
        {
            if(numberPlayers == COMPLETEPLAYERS) {
                btn_startGame.IsEnabled = true;
            }
            textBlock_number_players.Text =  numberPlayers + " / 4";
        }


        public void On_key_Down_Handler(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                SendMessage();
            }
        }

        /// <summary>
        /// Callback que escribe en pantalla cuando un jugador se ha salido del lobby y por lo tanto abandonó el juego 
        /// </summary>
        /// <param name="user">Nombre del usuario que abandonó el lobby </param>
        public void NotifyLeftPlayer(string user) {
            textBox_chat.Text += user + Properties.Resources.left_game;
        }

        private void btn_startGame_Click(object sender, RoutedEventArgs e)
        {
            try{
                client.StartGame(SingletonAccount.getSingletonAccount().User, idGame);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.server_connection_error);
                Console.WriteLine("Error en startGame " + ex.Message);                
            }
            
        }
       
        /// <summary>
        /// Callback que crea y abre la ventana del juego 
        /// </summary>
        public void InitGame()
        {
            MultiplayerGame game = new MultiplayerGame(idGame);
            game.Show();
            this.Close();
        }
    }
}
