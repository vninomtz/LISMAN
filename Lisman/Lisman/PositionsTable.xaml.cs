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

        private void Button_Click_go_back(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Close();
        }

        public void LoadTable()
        {
            using (var client = new LismanService.AccountManagerClient()) {
                try {
                    var listRecords = client.GetRecords();
                    table_positions.ItemsSource = listRecords;
                } catch (Exception ex) {
                    MessageBox.Show("Error en la BD");
                }
            }
        }

        
    }
}
