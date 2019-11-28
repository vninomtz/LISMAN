using System;
using System.Windows;

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
                    Logger.log.Error("Function loadTable, "+ ex);
                }
            }
        }

        
    }
}
