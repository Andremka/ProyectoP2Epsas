using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;

namespace WpfApp2P2D
{
    /// <summary>
    /// Lógica de interacción para WinSignUp.xaml
    /// </summary>
    public partial class WinSignUp : Window
    {
        private const int ID_INICIO = 1001;
        private readonly string rutaYnombreArch = "c:\\DatosPersonales\\usuariosSignUp.txt";
        private readonly string rutaCarpeta = "c:\\DatosPersonales";
        public WinSignUp()
        {
            InitializeComponent();
        }
        private int ObtenerSiguienteIdUsuario()
        {
            if (!File.Exists(rutaYnombreArch))
            {
                return ID_INICIO;
            }
            try
            {
                var lineas = File.ReadAllLines(rutaYnombreArch).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
                if (!lineas.Any())
                {
                    return ID_INICIO; 
                }
                var ultimaLinea = lineas.Last();
                var partes = ultimaLinea.Split(',');
                if (partes.Length >= 1 && int.TryParse(partes[0], out int ultimoId))
                {
                    return ultimoId + 1;
                }
                else
                {
                    return lineas.Count + ID_INICIO;
                }
            }
            catch (Exception)
            {
                return ID_INICIO;
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Clear();
            txtApPat.Clear();
            txtApMat.Clear();
            txtCorreo.Clear();
            txtCelular.Clear();
            txtNacimiento.Clear();
            pwdContraseña.Password = "";
            lblMensajes.Content = "";
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == "" || txtApPat.Text == "" || txtApMat.Text == "" ||
                txtCorreo.Text == "" || txtCelular.Text == "" ||
                txtNacimiento.Text == "" || pwdContraseña.Password == "")
            {
                lblMensajes.Content = "Debe completar TODOS los datos";
                lblMensajes.Foreground = Brushes.White;
            }
            else
            {
                if (!Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensajes.Content = "Correo electrónico no válido";
                    lblMensajes.Foreground = Brushes.White;
                    return;
                }
                if (pwdContraseña.Password.Length < 6)
                {
                    lblMensajes.Content = "La contraseña debe tener al menos 6 caracteres";
                    lblMensajes.Foreground = Brushes.White;
                    return;
                }
                if (!int.TryParse(txtNacimiento.Text, out int anio))
                {
                    lblMensajes.Content = "El Año de Nacimiento debe ser un número válido.";
                    lblMensajes.Foreground = Brushes.White;
                    txtNacimiento.Clear();
                    return;
                }
                if (anio < 1950 || anio > 2007)
                {
                    lblMensajes.Content = "Año de Nacimiento NO VALIDO";
                    lblMensajes.Foreground = Brushes.White;
                    txtNacimiento.Clear();
                    return;
                }
                string celular = txtCelular.Text;
                if (!Regex.IsMatch(celular, @"^[67]\d{7}$"))
                {
                    lblMensajes.Content = "Nro de Celular NO VALIDO";
                    lblMensajes.Foreground = Brushes.White;
                    txtCelular.Clear();
                    return;
                }
                if (File.Exists(rutaYnombreArch))
                {
                    var lineas = File.ReadAllLines(rutaYnombreArch);
                    if (lineas.Any(l => l.Split(',').Length >= 8 && l.Split(',')[4] == txtCorreo.Text))
                    {
                        lblMensajes.Content = "Este correo ya está registrado";
                        lblMensajes.Foreground = Brushes.White;
                        return;
                    }
                }
                int nuevoId = ObtenerSiguienteIdUsuario();
                if (!Directory.Exists(rutaCarpeta))
                {
                    try
                    {
                        Directory.CreateDirectory(rutaCarpeta); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al crear la carpeta: " + ex.Message);
                        return;
                    }
                }
                try
                {
                    lblMensajes.Content = "Registro exitoso Bienvenido/a " + txtNombre.Text;
                    lblMensajes.Foreground = Brushes.White;

                    string datos =
                        nuevoId + "," +
                        txtNombre.Text + "," +
                        txtApPat.Text + "," +
                        txtApMat.Text + "," +
                        txtCorreo.Text + "," +
                        txtCelular.Text + "," +
                        txtNacimiento.Text + "," +
                        pwdContraseña.Password + "\n";

                    File.AppendAllText(rutaYnombreArch, datos);

                    WinPrincipal principal = new WinPrincipal();
                    principal.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }
        }
        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexNombre = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$");
            e.Handled = !regexNombre.IsMatch(e.Text);
        }
        private void txtApPat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexApPat = new Regex("^[a-zA-Z]+$");
            e.Handled = !regexApPat.IsMatch(e.Text);
        }
        private void txtApMat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexApMat = new Regex("^[a-zA-Z]+$");
            e.Handled = !regexApMat.IsMatch(e.Text);
        }
        private void txtCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexCelular = new Regex(@"^[0-9]");
            e.Handled = !regexCelular.IsMatch(e.Text);
        }
        private void txtNacimiento_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexNacimiento = new Regex("^[0-9]+$");
            e.Handled = !regexNacimiento.IsMatch(e.Text);
        }
        private void txtCorreo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexCorreo = new Regex("^[a-zA-Z0-9@._-]+$");
            e.Handled = !regexCorreo.IsMatch(e.Text);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
