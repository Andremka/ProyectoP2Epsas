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

namespace WpfApp2P2D
{
    /// <summary>
    /// Lógica de interacción para WinPrincipal.xaml
    /// </summary>
    public partial class WinPrincipal : Window
    {
        // variable para guardar los datos del usuario
        private readonly Usuario _usuarioLogueado;
        public WinPrincipal(Usuario usuario)
        {
            InitializeComponent();
            _usuarioLogueado = usuario;
        }
        public WinPrincipal()
        {
            InitializeComponent();
            _usuarioLogueado = null; // Indica que no hay un usuario solicitante específico
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (_usuarioLogueado != null)
            {
                // Si el usuario es Solicitante, pasa el objeto Usuario
                Solicitud ventanaSolicitud = new Solicitud(_usuarioLogueado);
                ventanaSolicitud.Show();
            }
            else
            {
                DatoSolicitud ventanaGestionSolc = new DatoSolicitud();
                ventanaGestionSolc.Show();
            }

            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Login = new MainWindow();
            Login.Show();
            this.Close();
        }
    }
}
