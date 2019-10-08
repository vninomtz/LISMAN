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
    /// Interaction logic for MultiplayerGame.xaml
    /// </summary>
    public partial class MultiplayerGame : Window {
        public MultiplayerGame()
        {
            InitializeComponent();
        }

        private void btn_exitGame_Click(object sender, RoutedEventArgs e)
        {
            MultijugadorHome homeMultijugador = new MultijugadorHome();
            homeMultijugador.Show();
            this.Close();
        }
    }
}
