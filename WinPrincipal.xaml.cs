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
        public WinPrincipal()
        {
            InitializeComponent();
            Loaded += WinPrincipal_Loaded;
        }

        private void WinPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            Solicitud ventanaSolicitud = new Solicitud();
            ventanaSolicitud.Show();
            this.Close();
        }
    }
}
