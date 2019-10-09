using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
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
    /// Lógica de interacción para UserRegister.xaml
    /// </summary>
    public partial class UserRegister : Window {
        public UserRegister()
        {
            InitializeComponent();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData()) {
                using (var client = new LismanService.AccountManagerClient()) {
                    var accountSave = new LismanService.Account
                    {
                        User = textField_userName.Text,
                        Password = EncodePassword(passwordBox_password.Password),
                        Registration_date = DateTime.Now.ToString(),
                        Player = new LismanService.Player
                        {
                            First_name = textField_name.Text,
                            Last_name = textField_lastName.Text,
                            Email = textField_email.Text,                      
                        }
                    };
                    if (client.AddAccount(accountSave) != -1) {
                        var messageRegistrationSuccessful = Properties.Resources.message_registration_successful;
                        MessageBox.Show(messageRegistrationSuccessful);
                        MainWindow login = new MainWindow();
                        login.Show();
                        this.Close();
                    } else {
                        var messageRegistrationError = Properties.Resources.message_registration_error;
                        MessageBox.Show(messageRegistrationError);
                    }
                }
            }
        }

        public bool IsValidEmail(string emailaddress) {
            try {
                MailAddress email = new MailAddress(emailaddress);
                return true;
            } catch (FormatException) {
                return false;
            }
        }

        public bool ExistsEmail(String emailAdress) {
            using (var client = new LismanService.AccountManagerClient()) {
                return client.EmailExists(emailAdress);
            }
        }

        public bool UserNameExists(String username) {

            using (var client = new LismanService.AccountManagerClient()) {
                return client.UserNameExists(username);
            }
        }

        public bool ValidateData() {
            var messageError = "";
            if (textField_name.Text == "") {
                messageError = Properties.Resources.message_error_name;
                MessageBox.Show(messageError);
                return false;
            } 
            if (textField_lastName.Text == "") {
                messageError = Properties.Resources.message_error_lastname;
                MessageBox.Show(messageError);
                return false;
            } 
            if (textField_email.Text == "") {
                messageError = Properties.Resources.message_error_email;
                MessageBox.Show(messageError);
                return false;
            } else if(!IsValidEmail(textField_email.Text)){
                messageError = Properties.Resources.message_invalid_email;
                MessageBox.Show(messageError);
                return false;
            } else if (ExistsEmail(textField_email.Text)) {
                messageError = Properties.Resources.message_exists_email;
                MessageBox.Show(messageError);
                return false;
            }
            if (textField_userName.Text == "") {
                messageError = Properties.Resources.message_error_usename;
                MessageBox.Show(messageError);
                return false;
            } else if (UserNameExists(textField_userName.Text)) {
                messageError = Properties.Resources.message_exists_username;
                MessageBox.Show(messageError);
                return false;
            }
            if (passwordBox_password.Password == "") {
                messageError = Properties.Resources.message_error_password;
                MessageBox.Show(messageError);
                return false;
            } else if (passwordBox_confirmPassword.Password == "") {
                messageError = Properties.Resources.message_error_confirmation_password;
                MessageBox.Show(messageError);
                return false;
            } else if (!passwordBox_password.Password.Equals(passwordBox_confirmPassword.Password)){
                messageError = Properties.Resources.message_error_passwords_different;
                MessageBox.Show(messageError);
                return false;
            }
            return true;
        }

        public string EncodePassword(string originalPassword)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(originalPassword));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
