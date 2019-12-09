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
        
        /// <summary>
        /// Válida  que los campos no esten vacios
        /// </summary>
        /// <returns>true si todos los campos estan llenos, false si no </returns>
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


        /// <summary>
        /// Obtiene los campos ingresados por el usuario y valida con el servidor si son correctos o existe algún problema 
        /// </summary>

        public void AccessManagement(LismanService.Account account)
        {
            switch (account.Id)
            {
                case 0:
                    var messageWarningLogin = Properties.Resources.message_warning_login;
                    MessageBox.Show(messageWarningLogin);
                    Logger.log.Warn("Login Failed, user: " + textField_user.Text);
                    break;
                case -1:
                    MessageBox.Show("Error en la conexión a la BD");
                    break;
                default:
                    if (account.Key_confirmation == " ")
                    {
                        SingletonAccount.setSingletonAccount(account);
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        var messageAccountConfirm = Properties.Resources.message_account_confirm;
                        MessageBox.Show(messageAccountConfirm);
                    }
                    break;
            }
        }


        public void LoginUser()
        {
            if (ValidateFields())
            {
                try
                {
                    using (var client = new LismanService.LoginManagerClient()) 
                    {
                        try 
                        {
                            bool inSession = client.UserInSession(textField_user.Text);
                            
                            if (!inSession) 
                            {
                                LismanService.Account account = client.LoginAccount(textField_user.Text, Encrypter.EncodePassword(passwordBox_password.Password));
                                AccessManagement(account);
                            }
                            else
                            {
                                MessageBox.Show("Hay una sesion iniciada, por favor cerrarla");

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

        /// <summary>
        /// Corrrobora que exista conexion a internet una llamada a una pagina 
        /// </summary>
        /// <returns>Devuelve true si hay internet y false en caso contrario </returns>

        public static bool CheckForInternetConnection()
        {
            bool internetAvailable = true;
            try
            {
                using (var client = new WebClient())
                {
                    client.OpenRead("http://google.com");
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
