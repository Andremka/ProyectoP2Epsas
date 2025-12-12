using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2P2D
{
    public partial class Solicitud : Window
    {
        // Variables para recibir las clases
        private readonly Usuario _usuarioLogueado;
        private SolicitudServ NuevaSolicitud;
        private Inmueble InmuebleAfectado;
        private readonly SolicitudServ _solicitudEdicion;
        private readonly Inmueble _inmuebleEdicion;
        // Rutas para guardar la solicitud del solicitante
        private readonly string rutaCarpeta = "c:\\Datos De Solicitud";
        private readonly string rutaArchivo = "c:\\Datos De Solicitud\\DatosSolicitud.txt";
        public Solicitud()
        {
            InitializeComponent();
            _usuarioLogueado = null;
        }
        // Constructor para Solicitantes que recibe el Usuario Logueado
        public Solicitud(Usuario usuario){
            InitializeComponent();
            _usuarioLogueado = usuario;
            LlenarDatosIniciales(); // Inicializa el campo de ID Sugerido de Solicitud
        }
        public Solicitud(Usuario usuario, SolicitudServ solicitud, Inmueble inmueble) : this(usuario){
            _solicitudEdicion = solicitud;
            _inmuebleEdicion = inmueble;
            LlenarDatosEdicion();
        }
        private void LlenarDatosIniciales() {
            if (_usuarioLogueado != null){
                txtIdUsuario.Text = _usuarioLogueado.idUsuario.ToString();
                txtIdUsuario.IsEnabled = false; // Bloquear para que no se pueda cambiar
                // Los campos de Fecha Emision y Estado se llenan automaticamente
                txtFechaEmision.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtEstado.Text = "Pendiente de Envío";
                txtIdSolicitud.Text = GenerarSiguienteIdSolicitud().ToString();
            }
        }
        private void LlenarDatosEdicion(){
            if (_solicitudEdicion != null && _inmuebleEdicion != null){
                // Rellenar campos de Solicitud e Inmueble
                txtIdUsuario.Text = _usuarioLogueado.idUsuario.ToString(); // Ya está bloqueado
                txtIdSolicitud.Text = _solicitudEdicion.idSolicitud.ToString();
                txtFechaEmision.Text = _solicitudEdicion.fechaEmision.ToString("yyyy-MM-dd HH:mm:ss");
                txtEstado.Text = _solicitudEdicion.estado;
                txtTipoServicio.Text = _solicitudEdicion.tipoServicio;
                txtPrioridad.Text = _solicitudEdicion.prioridad;
                txtNroMedidor.Text = _inmuebleEdicion.nroMedidor.ToString();
                txtDireccion.Text = _inmuebleEdicion.direccion;
                txtCategoria.Text = _inmuebleEdicion.categoria;
                txtTipoInmueble.Text = _inmuebleEdicion.tipoInmueble;
            }
        }
        private int GenerarSiguienteIdSolicitud(){
            if (!File.Exists(rutaArchivo)){
                return 1; 
            }
            try{
                var lineas = File.ReadAllLines(rutaArchivo).Where(l => !string.IsNullOrWhiteSpace(l));
                if (!lineas.Any()) return 1;
                var ultimaLinea = lineas.Last();
                var partes = ultimaLinea.Split(',');
                if (partes.Length > 1 && int.TryParse(partes[1], out int ultimoIdSolicitud)){
                    return ultimoIdSolicitud + 1;
                }
                return lineas.Count() + 1; 
            }
            catch{
                return 1;
            }
        }
        private bool ValidarCampos(){
            lblMensajes.Content = "";
            lblMensajes.Foreground = Brushes.White;
            if (txtIdSolicitud.Text == "" || txtTipoServicio.Text == "" ||
                txtPrioridad.Text == "" || txtNroMedidor.Text == "" || txtDireccion.Text == "" ||
                txtCategoria.Text == "" || txtTipoInmueble.Text == "")
            {
                lblMensajes.Content = "Debe llenar todos los campos requeridos.";
                return false;
            }
            // Validacio de los campos numericos
            int idSolicitud;
            if (!int.TryParse(txtIdSolicitud.Text, out idSolicitud) || idSolicitud <= 0){
                lblMensajes.Content = "ID Solicitante NO Valido";
                txtIdSolicitud.Focus();
                return false;
            }
            int nroMedidor;
            if (!int.TryParse(txtNroMedidor.Text, out nroMedidor) || nroMedidor <= 0){
                lblMensajes.Content = "Nro Medidor NO Valido";
                txtNroMedidor.Focus();
                return false;
            }
            // Creacion de los objetos con las clases creadas
            NuevaSolicitud = new SolicitudServ(
                idSolicitud,
                DateTime.Now,
                txtEstado.Text,
                txtTipoServicio.Text,
                txtPrioridad.Text
            );
            InmuebleAfectado = new Inmueble(
                nroMedidor,
                txtDireccion.Text,
                txtCategoria.Text,
                txtTipoInmueble.Text
            );
            return true;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e){
            txtTipoServicio.Clear();
            txtPrioridad.Clear();
            txtNroMedidor.Clear();
            txtDireccion.Clear();
            txtCategoria.Clear();
            txtTipoInmueble.Clear();
            lblMensajes.Content = "";
        }
        private void btnRegSolicitud_Click(object sender, RoutedEventArgs e){
            WinPrincipal principal = new WinPrincipal(_usuarioLogueado);
            principal.Show();
            this.Close();
        }
        private void btnContinuar_Click(object sender, RoutedEventArgs e){
            if (_usuarioLogueado == null){
                lblMensajes.Content = "Error: Usuario no autenticado. Inicie sesión nuevamente.";
                return;
            }
            if (ValidarCampos()){
                try{ //VERIFICA QUE TENGA EL CONSTRUCTOR PARA PASAR A RESUMEN DE DATOS
                    Confirmacion ventanaConfirmacion = new Confirmacion(
                        _usuarioLogueado,
                        NuevaSolicitud,
                        InmuebleAfectado
                    );
                    ventanaConfirmacion.Show();
                    this.Close();
                }
                catch (Exception ex){
                    MessageBox.Show("Error al intentar continuar: " + ex.Message, "Error de Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}