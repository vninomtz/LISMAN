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
    /// Interaction logic for PositionsTable.xaml
    /// </summary>
    public partial class PositionsTable : Window {
        public PositionsTable()
        {
            InitializeComponent();
            LoadTable();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Close();
        }

        public void LoadTable()
        {
            using (var client = new LismanService.UserClient()) {
                try {
                    var listAccounts = client.GetCuentas();
                    tbl_positions.ItemsSource = listAccounts;
                } catch (Exception ex) {
                    MessageBox.Show("Error en la BD");
                }
            }
        }

        
    }
}
