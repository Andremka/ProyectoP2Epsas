using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp2P2D
{
    /// <summary>
    /// Lógica de interacción para Solicitud.xaml
    /// </summary>
    public partial class Solicitud : Window
    {
        private readonly string rutaCarpeta = "c:\\Datos De Solicitud";
        private readonly string rutaArchivo = "c:\\Datos De Solicitud\\DatosSolicitud.txt";
        public Solicitud()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCI.Clear();
            txtDireccion.Clear();
            txtNroCasa.Clear();
            txtCoordenadas.Clear();
            txtTipoInmueble.Clear();
            txtPropietario.Clear();
            txtTipoSolicitud.Clear();
            txtUsoServ.Clear();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCI.Text == "" || txtDireccion.Text == "" || txtNroCasa.Text == "" ||
                txtCoordenadas.Text == "" || txtTipoInmueble.Text == "" ||
                txtPropietario.Text == "" || txtTipoSolicitud.Text == "" ||
                txtUsoServ.Text == "")
            {
                MessageBox.Show("Debe de llenar todos los campos");
                return;
            }
            try
            {
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }
                string datos =
                    $"{txtCI.Text}," +
                    $"{txtDireccion.Text}," +
                    $"{txtNroCasa.Text}," +
                    $"{txtCoordenadas.Text}," +
                    $"{txtTipoInmueble.Text}," +
                    $"{txtPropietario.Text}," +
                    $"{txtTipoSolicitud.Text}," +
                    $"{txtUsoServ.Text}\n";
                File.AppendAllText(rutaArchivo, datos);
                MessageBox.Show("Solicitud guardada correctamente");
                txtCI.Clear();
                txtDireccion.Clear();
                txtNroCasa.Clear();
                txtCoordenadas.Clear();
                txtTipoInmueble.Clear();
                txtPropietario.Clear();
                txtTipoSolicitud.Clear();
                txtUsoServ.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la Solicitud: " + ex.Message);
            }
        }
    }
}
