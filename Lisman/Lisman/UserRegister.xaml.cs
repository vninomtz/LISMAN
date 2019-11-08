using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        private void SendEmail(String destinationEmail,String token){
            String originEmail = "lismanapp@gmail.com";
            String passwordEmail = "#LismaN&1423";
            String subjectEmail = Properties.Resources.subject_email;
            String bodyEmail = Properties.Resources.body_email;
            String url = "http://lismanweb.azurewebsites.net/Home/About?token=" + token;

            MailMessage mailMessage = new MailMessage(originEmail,destinationEmail,subjectEmail,bodyEmail + "<br>" + url);
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(originEmail,passwordEmail);

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();



        }

        public void SaveData() {
            if (validateEmptyFields() && ValidateData()) {
                try {
                    using (var client = new LismanService.AccountManagerClient()) {
                        String token = EncodePassword(Guid.NewGuid().ToString());
                        var accountSave = new LismanService.Account {
                            User = textField_userName.Text,
                            Password = EncodePassword(passwordBox_password.Password),
                            Registration_date = DateTime.Now.ToString(),
                            Key_confirmation = token,
                            Player = new LismanService.Player {
                                First_name = textField_name.Text,
                                Last_name = textField_lastName.Text,
                                Email = textField_email.Text,
                            }
                        };
                        if (client.AddAccount(accountSave) != -1) {
                            SendEmail(accountSave.Player.Email, token);
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
                } catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        public void On_key_Down_Handler(object sender, KeyEventArgs inputKey) {
            if (inputKey.Key == Key.Return) {
                SaveData();
            }
        }

        public bool IsValidEmail(string emailaddress) {
            Regex expressionEmail = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return expressionEmail.IsMatch(emailaddress);
        }

        public bool ExistsEmail(String emailAdress) {
            try {
                using (var client = new LismanService.LoginManagerClient()) {
                    return client.EmailExists(emailAdress);
                }
            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            
        }

        public bool UserNameExists(String username) {
            try {
                using (var client = new LismanService.LoginManagerClient()) {
                    return client.UserNameExists(username);
                }
            } catch (Exception ex) {
                Console.WriteLine("error" + ex.Message);
                return false;
            }
            
            
        }

        public bool showErrorMessage(String errorMessage) {
            MessageBox.Show(errorMessage);
            return false;
        }

        public bool validateEmptyFields() {
            if (textField_name.Text == string.Empty) {
                return showErrorMessage(Properties.Resources.message_error_name);
            }
            if (textField_lastName.Text == string.Empty) {
                return showErrorMessage(Properties.Resources.message_error_lastname);
            }
            if (textField_email.Text == string.Empty) {
                return showErrorMessage(Properties.Resources.message_error_email);
            }
            if (textField_userName.Text == string.Empty) {
                return showErrorMessage(Properties.Resources.message_error_usename);
            }
            if (passwordBox_password.Password == string.Empty) {
                return showErrorMessage(Properties.Resources.message_error_password);
            }
            if (passwordBox_confirmPassword.Password == string.Empty) {
                return showErrorMessage(Properties.Resources.message_error_confirmation_password);
            }

            return true;
        }

        public bool ValidateData() {
            Regex rg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1 ]+$");
            if (!rg.IsMatch(textField_name.Text)) {
                return showErrorMessage(Properties.Resources.message_invalid_name);
            }
            if (!rg.IsMatch(textField_lastName.Text)) {
               return showErrorMessage(Properties.Resources.message_invalid_lastname);
            }
             else if(!IsValidEmail(textField_email.Text)){
                return showErrorMessage(Properties.Resources.message_invalid_email);
            } else if (ExistsEmail(textField_email.Text)) {
                return showErrorMessage(Properties.Resources.message_exists_email);
            }
            if (UserNameExists(textField_userName.Text)) {
                 return showErrorMessage(Properties.Resources.message_exists_username);
            }
            if (!passwordBox_password.Password.Equals(passwordBox_confirmPassword.Password)){
                return showErrorMessage(Properties.Resources.message_error_passwords_different);
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
