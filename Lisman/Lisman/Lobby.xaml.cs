using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Lisman.LismanService;
using System.ServiceModel;

namespace Lisman {
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window, LismanService.IChatManagerCallback {
        InstanceContext instance = null;
        LismanService.ChatManagerClient client = null;
        int idGame;
        int numberPlayers;
        public Lobby(int idGame)
        {
            InitializeComponent();
            instance = new InstanceContext(this);
            this.idGame = idGame;
            client = new ChatManagerClient(instance);
            client.JoinChat(SingletonAccount.getSingletonAccount().User, idGame);
            textBlock_name_player.Text = SingletonAccount.getSingletonAccount().User;
            

        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MultiplayerHome homeMultiplayer = new MultiplayerHome();
            homeMultiplayer.Show();
            this.Close();
        }

        private void btn_exitGame_Click(object sender, RoutedEventArgs e) {
            MultiplayerHome homeMultiplayer = new MultiplayerHome();
            homeMultiplayer.Show();
            this.Close();
        }

        public void NotifyMessage(Message message)
        {
            textBox_chat.Text += "\n" + message.Account.User + ": " + message.Text;

        }

        public void NotifyJoinedPlayer(string user)
        {
            textBox_chat.Text += "\nThe User " + user + "is joined to the Game";

            
        }

        private void button_send_Click(object sender, RoutedEventArgs e)
        {
            Random rd = new Random(999);
            Message message = new Message
            {
                Id = rd.Next(),
                Creation_date = DateTime.Now,
                Text = textBox_message.Text,
                Account = SingletonAccount.getSingletonAccount()
            };

            client.SendMessage(message,idGame);
            textBox_message.Text = String.Empty;
 
            
        }


        public void NotifyNumberPlayers(int numberPlayers)
        {
            textBlock_number_players.Text = "4/" + numberPlayers;
        }
    }
}
