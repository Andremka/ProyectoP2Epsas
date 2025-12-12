using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2P2D
{
    public partial class Confirmacion : Window
    {
        private readonly Usuario _usuario;
        private readonly SolicitudServ _solicitud;
        private readonly Inmueble _inmueble;
        // Rutas de Archivo
        private readonly string rutaCarpeta = "c:\\Datos De Solicitud";
        private readonly string rutaArchivo = "c:\\Datos De Solicitud\\DatosSolicitud.txt";
        public Confirmacion(Usuario user, SolicitudServ solicitud, Inmueble inmueble)
        {
            InitializeComponent();
            // Almacena los objetos recibidos de la Solicitud
            _usuario = user;
            _solicitud = solicitud;
            _inmueble = inmueble;
            LlenarResumen();
        }
        public Confirmacion()
        {
            InitializeComponent();
        }
        private void LlenarResumen(){
            // Sección Usuario
            lblResumenIdUsuario.Content = _usuario.idUsuario.ToString();
            lblResumenNombre.Content = $"{_usuario.nombre} {_usuario.apPaterno} {_usuario.apMaterno}";
            lblResumenEmail.Content = _usuario.correo;
            // Sección Solicitud
            lblResumenIdSolicitud.Content = _solicitud.idSolicitud.ToString();
            lblResumenFecha.Content = _solicitud.fechaEmision.ToString("yyyy-MM-dd");
            lblResumenTipoServicio.Content = _solicitud.tipoServicio;
            lblResumenPrioridad.Content = _solicitud.prioridad;
            // Sección Inmueble
            lblResumenNroMedidor.Content = _inmueble.nroMedidor.ToString();
            lblResumenDireccion.Content = _inmueble.direccion;
            lblResumenCategoria.Content = _inmueble.categoria;
            lblResumenTipoInmueble.Content = _inmueble.tipoInmueble;
        }
        private void GuardarDatos(){
            try{
                if (!Directory.Exists(rutaCarpeta)){
                    Directory.CreateDirectory(rutaCarpeta);
                }
                string lineaDatos =
                    $"{_usuario.idUsuario}," +
                    $"{_solicitud.idSolicitud}," +
                    $"{_solicitud.fechaEmision.ToString("yyyy-MM-dd HH:mm:ss")}," +
                    $"{_solicitud.estado}," +
                    $"{_solicitud.tipoServicio}," +
                    $"{_solicitud.prioridad}," +
                    $"{_inmueble.nroMedidor}," +
                    $"{_inmueble.direccion}," +
                    $"{_inmueble.categoria}," +
                    $"{_inmueble.tipoInmueble}";

                File.AppendAllText(rutaArchivo, lineaDatos + Environment.NewLine);
                MessageBoxResult result = MessageBox.Show("¡Solicitud guardada exitosamente!\n\n¿Desea realizar una Nueva Solicitud?","Éxito",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );
                if (result == MessageBoxResult.Yes){
                    Solicitud nuevaSolicitud = new Solicitud(_usuario); //vuelve a la ventana de bienvenida sin los datos ya llenados
                    nuevaSolicitud.Show();
                }
                else{
                    WinPrincipal principal = new WinPrincipal(_usuario);
                    principal.Show();
                }
                this.Close();
            }
            catch (Exception ex){
                MessageBox.Show($"Error al guardar la solicitud: {ex.Message}", "Error de Guardado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnConfirmarEnvio_Click(object sender, RoutedEventArgs e){
            GuardarDatos();
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e){
            Solicitud ventanaSolicitud = new Solicitud(_usuario, _solicitud, _inmueble); //vuelve a la anterior ventana si hay que modificar algo
            ventanaSolicitud.Show();
            this.Close();
        }
    }
}