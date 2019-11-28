using System.Windows;

namespace Lisman {
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void table_positions_Click(object sender, RoutedEventArgs e)
        {
            PositionsTable positions = new PositionsTable();
            positions.Show();
            this.Close();
        }

        private void button_multiplayerMode_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerHome multiplayerWindow = new MultiplayerHome();
            multiplayerWindow.Show();
            this.Close();
           
        }

       
    }
}
