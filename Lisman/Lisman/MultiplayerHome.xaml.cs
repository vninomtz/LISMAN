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

namespace Lisman {
    /// <summary>
    /// Interaction logic for MultijugadorHome.xaml
    /// </summary>
    public partial class MultiplayerHome : Window {
        public MultiplayerHome()
        {
            InitializeComponent();             
        }        

        private void button_goBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Close();
        }

        private void button_newGame_Click(object sender, RoutedEventArgs e)
        {
            LismanService.GameManagerClient client = new LismanService.GameManagerClient();
            
            try
            {
                int idGame = client.CreateGame(SingletonAccount.getSingletonAccount().User);
                if (idGame > 0)
                {
                    Lobby lobby = new Lobby(idGame);
                    lobby.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error de conexión con el servidor, por favor intente mas tarde");
                Logger.log.Error("Function new game");
            }
           
            
            
        }

        private void button_joinGame_Click(object sender, RoutedEventArgs e)
        {
            LismanService.GameManagerClient client = new LismanService.GameManagerClient();
            int idGame = client.JoinGame(SingletonAccount.getSingletonAccount().User);
            if(idGame > 0) {
                Lobby lobby = new Lobby(idGame);
                lobby.Show();
                this.Close();
            } else {
                MessageBox.Show(Properties.Resources.message_nogames_found);
            }

           
        }
    }
}
