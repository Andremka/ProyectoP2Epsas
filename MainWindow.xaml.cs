using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2P2D
{
    public partial class MainWindow : Window
    {
        private readonly string rutaYnombreArch = "c:\\DatosPersonales\\usuariosSignUp.txt";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            txtCorreo.Clear();
            pwdPassword.Password = "";
            lblMensajes.Content = "";
        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            WinSignUp registro = new WinSignUp();
            registro.Show();
            this.Close(); 
        }
        private void btnAcept_Click(object sender, RoutedEventArgs e)
        {
            lblMensajes.Content = "";
            if (txtCorreo.Text == "" || pwdPassword.Password == "")
            {
                
                lblMensajes.Content = "Debe Ingresar todos los datos";
                lblMensajes.Foreground = Brushes.White;
            }
            else 
            {
                if (!Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensajes.Content = "Correo electrónico no válido";
                    lblMensajes.Foreground = Brushes.White;
                    txtCorreo.Clear();
                    pwdPassword.Password = "";
                    
                    return;
                }
                if (pwdPassword.Password.Length < 6)
                {
                    lblMensajes.Content = "La contraseña debe tener al menos 6 caracteres";
                    lblMensajes.Foreground = Brushes.White;
                    txtCorreo.Clear();
                    pwdPassword.Password = "";
                    
                    return;
                }
                try
                {
                    string email = txtCorreo.Text;
                    string contra = pwdPassword.Password;

                    if (!File.Exists(rutaYnombreArch))
                    {
                        lblMensajes.Foreground = Brushes.White;
                        lblMensajes.Content = "La ruta o nombre del archivo no existen!!";
                        return;
                    }
                    //Leer el archivo
                    var lineas = File.ReadAllLines(rutaYnombreArch);
                    bool encontrado = false;
                    foreach (var unaLinea in lineas)
                    {
                        var partes = unaLinea.Split(',');
                        if (partes.Length >= 7 && email.Equals(partes[3]) && contra.Equals(partes[6]))
                        {
                            encontrado = true;
                            break;
                        }
                    }
                    if (encontrado)
                    {
                        lblMensajes.Content = "Bienvenido al sistema" + txtCorreo.Text;
                        lblMensajes.Foreground = Brushes.White;

                        WinPrincipal principal = new WinPrincipal();
                        principal.Show();
                        this.Close();
                    }
                    else 
                    {
                        lblMensajes.Content = "USUARIO NO AUTORIZADO...";
                        lblMensajes.Foreground = Brushes.White;
                        txtCorreo.Clear();
                        pwdPassword.Clear();
                    }

                }
                catch (Exception ex)  
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                    txtCorreo.Clear();
                    pwdPassword.Clear();
                }
            }
        }
    }
}
