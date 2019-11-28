﻿using System;
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
                MessageBox.Show(Properties.Resources.server_conecction_error);
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
                MessageBox.Show(Properties.Resources.server_conecction_error);
                Logger.log.Warn("Error en funcion exit game " + ex.Message);                                              
            }

           
           
        }

        public void NotifyMessage(Message message)
        {
            textBox_chat.Text += "\n" + message.Account.User + ": " + message.Text;

        }

        public void NotifyJoinedPlayer(string user)
        {
            textBox_chat.Text += "\n" + user + Properties.Resources.joined_game;

            
        }

        public void SendMessage() {
            Random rd = new Random(999);
            if (textBox_message.Text != String.Empty) {
                Message message = new Message {

                    Id = rd.Next(),
                    Creation_date = DateTime.Now,
                    Text = textBox_message.Text,
                    Account = SingletonAccount.getSingletonAccount()
                };

                try
                {
                    client.SendMessage(message, idGame);
                    textBox_message.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Properties.Resources.server_conecction_error);
                    Logger.log.Warn("Function sendMessage" + ex.Message); 
                }

                
                
            }
            

            
        }

        private void button_send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }


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
                MessageBox.Show(Properties.Resources.server_conecction_error);
                Console.WriteLine("Error en startGame " + ex.Message);
                
            }
            
        }

        public void InitGame()
        {
            MultiplayerGame game = new MultiplayerGame(idGame);
            game.Show();
            this.Close();
        }
    }
}
