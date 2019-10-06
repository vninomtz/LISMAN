using System;
using System.Collections.Generic;
using System.Linq;
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

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos()) {
                using (var cliente = new LismanService.UserClient()) {
                    var cuentaGuardar = new LismanService.Cuenta
                    {
                        Usuario = txt_nombreUsuario.Text,
                        Contrasenia = EncodePassword(txt_password.Password),
                        fecha_registro = DateTime.Now.ToString(),
                        Jugador = new LismanService.Jugador
                        {
                            Nombre = txt_nombre.Text,
                            Apellido = txt_apellidos.Text,
                            Email = txt_email.Text,
                            


                        }

                    };
                    if (cliente.AddCuenta(cuentaGuardar) != -1) {
                        MessageBox.Show("Se realizo el registro");
                    } else {
                        MessageBox.Show("Ocurrio un error al registrar");

                    }
                }

           

                

                
            }
        }


        public Boolean ValidarDatos()
        {
            if (txt_nombre.Text == "") {
                MessageBox.Show("Llenar el campo nombre, por favor");
                return false;
            } else if (txt_apellidos.Text == "") {
                MessageBox.Show("Llenar el campo apellido, por favor");
                return false;
            } else if (txt_email.Text == "") {
                MessageBox.Show("Llenar el campo nombre email, por favor");
                return false;
            } else if (txt_nombreUsuario.Text == "") {
                MessageBox.Show("Llenar el campo usuario, por favor");
                return false;
            } else if (txt_password.Password == "") {
                MessageBox.Show("Llenar el campo contraseña, por favor");
                return false;
            } else if (txt_confPassword.Password == "") {
                MessageBox.Show("Confirmar contraseña, por favor");
                return false;
            } else if (!txt_password.Password.Equals(txt_confPassword.Password)){
                MessageBox.Show("La contraseña debe de ser igual");
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
