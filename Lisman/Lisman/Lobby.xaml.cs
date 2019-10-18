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
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window {
        public Lobby()
        {
            InitializeComponent();
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
    }
}
