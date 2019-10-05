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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lisman {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*LismanService.UserClient  client = new LismanService.UserClient();
            if (client.getConexion() == 1) {
                MessageBox.Show("Conexion Establecida");
            }*/
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.Show();
            this.Close();
        }

        private void MenuItem_Click_Spanish(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
