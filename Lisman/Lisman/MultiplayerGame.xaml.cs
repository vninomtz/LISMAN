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
using System.Windows.Threading;

namespace Lisman {
    /// <summary>
    /// Interaction logic for MultiplayerGame.xaml
    /// </summary>
    public partial class MultiplayerGame : Window {
        int idgame;
        int count = 0;
        
        public MultiplayerGame(int idgame)
        {
            InitializeComponent();
            this.idgame = idgame;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick +=  new EventHandler(Moverpacman);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
        }

        private void Moverpacman(object sender, EventArgs e)
        {
            count += 1;
            Grid.SetRow(Pacman, 0);
            Grid.SetColumn(Pacman, count);
        } 
    }
}
