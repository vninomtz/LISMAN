using System;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

namespace Lisman {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();           
           
        }

        public Boolean ValidateFields()
        {
            
            if(textField_user.Text == "") {
                var messageError = Properties.Resources.message_error_usename;
                MessageBox.Show(messageError);
                return false;
            }
            if (passwordBox_password.Password == "") {
                var messageError = Properties.Resources.message_error_password;
                MessageBox.Show(messageError);
                return false;
            }
            return true;
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

        public void On_key_Down_Handler(object sender, KeyEventArgs inputKey) {
            if (inputKey.Key == Key.Return) {
                LoginUser();
            }
            
        }


        private void btn_login_Click(object sender, RoutedEventArgs e) {
            if (CheckForInternetConnection())
            {
                LoginUser();
            }
            else
            {
                MessageBox.Show("No hay conexion a internet");
            }
                     
        }


        public void LoginUser() {
            if (ValidateFields()) {

                try
                {
                    
                using (var client = new LismanService.LoginManagerClient()) {

                    try {
                        LismanService.Account account = client.LoginAccount(textField_user.Text, Encrypter.EncodePassword(passwordBox_password.Password));
                        if (account != null) {
                            if (account.Key_confirmation == " ") {
                                SingletonAccount.setSingletonAccount(account);
                                SingletonConnection.CreateConnection();
                                MainMenu mainMenu = new MainMenu();
                                mainMenu.Show();
                                
                                this.Close();
                            } else {
                                var messageAccountConfirm = Properties.Resources.message_account_confirm;
                                MessageBox.Show(messageAccountConfirm);
                            }

                        }
                        else
                        {
                                var messageWarningLogin = Properties.Resources.message_warning_login;
                                MessageBox.Show(messageWarningLogin);

                                Logger.log.Warn("Login Failed, user: " + textField_user.Text);
                        }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Logger.log.Error("Function LoginUser, " + ex.Message);
                        }


                    }
                } catch (CommunicationException ex)
                {
                    MessageBox.Show(Properties.Resources.server_connection_error);
                    Logger.log.Error("Function LoginUser, " + ex.Message);
                }
                
            }
        }

        public static bool CheckForInternetConnection()
        {
            bool internetAvailable = true;
            try
            {
                using (var client = new WebClient())
                {                    
                    using (client.OpenRead("http://google.com"))
                    { 
                    }
                }
                
            }
            catch
            {
                internetAvailable = false;               
            }

            return internetAvailable;
        }








    }
}
