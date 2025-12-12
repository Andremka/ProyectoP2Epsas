using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2P2D
{
    public partial class DatoSolicitud : Window
    {
        private const string RUTA_USUARIOS = "c:\\DatosPersonales\\usuariosSignUp.txt";
        private const string RUTA_SOLICITUDES = "c:\\Datos De Solicitud\\DatosSolicitud.txt";
        // Listas para almacenar los datos de los archivos de texto
        private List<Usuario> todosUsuarios;
        private List<ModeloSolicitud> todasSolicitudes;
        private List<ModeloInmueble> todosInmuebles;
        // Propiedades seleccionadas
        private Usuario usuarioSeleccionado;
        private ModeloSolicitud solicitudSeleccionada;
        //Constructor
        public DatoSolicitud()
        {
            InitializeComponent();
            todosUsuarios = new List<Usuario>();
            todasSolicitudes = new List<ModeloSolicitud>();
            todosInmuebles = new List<ModeloInmueble>();
            // Suscribir eventos
            dgSolicitudes.SelectionChanged += DgSolicitudes_SelectionChanged;
            dgInmuebles.SelectionChanged += DgInmuebles_SelectionChanged;
        }
        private void DgSolicitudes_SelectionChanged(object sender, SelectionChangedEventArgs e){
            usuarioSeleccionado = dgSolicitudes.SelectedItem as Usuario; //toma el usuario seleccionado
            if (usuarioSeleccionado != null){ //borra las selecciones anteriores
                dgInmuebles.ItemsSource = null;
                dgDatoServicio.ItemsSource = null;
                solicitudSeleccionada = null;
                // Cargar las solicitudes del usuario seleccionado
                CargarSolicitudesPorUsuario(usuarioSeleccionado.idUsuario);
            }
        }
        private void DgInmuebles_SelectionChanged(object sender, SelectionChangedEventArgs e){
            // Toma la Solicitud seleccionada
            solicitudSeleccionada = dgInmuebles.SelectedItem as ModeloSolicitud;
            if (solicitudSeleccionada != null){
                // Cargar el Inmueble asociado a esa Solicitud
                CargarInmueblePorSolicitud(solicitudSeleccionada.idSolicitud);
            }
            else{
                dgDatoServicio.ItemsSource = null;
            }
        }
        private void btnRegresar_Click(object sender, RoutedEventArgs e){
             WinPrincipal principal = new WinPrincipal();
             principal.Show();
             this.Close();
        }
        private void btnCargarUsr_Click(object sender, RoutedEventArgs e){
            try{
                todosUsuarios = AyudaDeDatos.CargarUsuarios(RUTA_USUARIOS);   //carga solo de Usuarios
                dgSolicitudes.ItemsSource = todosUsuarios;
                if (todosUsuarios.Any()){
                    MessageBox.Show("Usuarios cargados exitosamente", "Carga Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else{
                    MessageBox.Show("Error o archivo vacío al cargar usuarios", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error de Carga", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCargarSolc_Click(object sender, RoutedEventArgs e){   
            if (!File.Exists(RUTA_SOLICITUDES))
            {  // verificacion si existe el archivo
                MessageBox.Show($"ERROR CRÍTICO DE RUTA: No se encontró el archivo en:\n{RUTA_SOLICITUDES}\nVerifique la ruta y el nombre del archivo.", "Error de Archivo", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detener la ejecución si el archivo no existe
            }
            try{
                var datosCombinados = AyudaDeDatos.CargarSolicitudesEInmuebles(RUTA_SOLICITUDES);
                todasSolicitudes = datosCombinados.Solicitudes;
                todosInmuebles = datosCombinados.Inmuebles;
                if (todasSolicitudes.Any()){
                    MessageBox.Show("Solicitudes e Inmuebles cargados exitosamente.", "Carga Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else{
                    MessageBox.Show("Archivo cargado, pero no se encontraron solicitudes.", "Advertencia de Formato", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex){
                MessageBox.Show($"Error al PROCESAR el contenido del archivo: {ex.Message}", "Error de Procesamiento", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CargarSolicitudesPorUsuario(int idUsuario){
            if (todasSolicitudes == null || !todasSolicitudes.Any()){   //verifica que las solicitudes se cargaron
                MessageBox.Show("Por favor, presione 'CARGAR SOLICITUD' antes de seleccionar un usuario.", "Falta Carga", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgInmuebles.ItemsSource = null;
                return;
            }
            // Filtrar las Solicitudes
            var solicitudesFiltradas = todasSolicitudes
                .Where(s => s.idUsuario == idUsuario)
                .ToList();
            dgInmuebles.ItemsSource = solicitudesFiltradas;
            if (!solicitudesFiltradas.Any()){
                dgDatoServicio.ItemsSource = null;
            }
        }

        private void CargarInmueblePorSolicitud(int idSolicitud){
            var inmueble = todosInmuebles
                .FirstOrDefault(i => i.idSolicitud == idSolicitud);
            if (inmueble != null){
                // Usamos una lista temporal para mostrar un solo objeto en el DataGrid
                dgDatoServicio.ItemsSource = new List<ModeloInmueble> { inmueble };
            }
            else{
                dgDatoServicio.ItemsSource = null;
            }
        }
        private void btnGrabarArch_Click(object sender, RoutedEventArgs e){
            // Validacion de que se haya seleccionado una Solicitud
            if (solicitudSeleccionada == null){
                MessageBox.Show("Debe seleccionar una Solicitud para generar el reporte.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try{
                GenerarReporteFinal(solicitudSeleccionada);
            }
            catch (Exception ex){
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error de Reporte", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GenerarReporteFinal(ModeloSolicitud solicitud){
            // BUSQUEDA DEL USUARIO E INMUEBLE
            ModeloInmueble inmueble = todosInmuebles.FirstOrDefault(i => i.idSolicitud == solicitud.idSolicitud);
            Usuario usuarioCompleto = todosUsuarios.FirstOrDefault(u => u.idUsuario == solicitud.idUsuario);
            // Verificaciones 
            if (inmueble == null){
                MessageBox.Show("No se encontró el inmueble asociado a la solicitud.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (usuarioCompleto == null){
                MessageBox.Show("No se encontró el usuario asociado a la solicitud.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // FORMATO PARA EL ARCHIVO DE TEXTO
            string contenidoReporte = $@"
                        =====================================================
                                    REPORTE FINAL DE SOLICITUD
                        =====================================================
                        FECHA GENERACIÓN: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
                        -----------------------------------------------------
                        === DATOS DEL SOLICITANTE ===
                        Nombre Completo: {usuarioCompleto.nombre} {usuarioCompleto.apPaterno} {usuarioCompleto.apMaterno}
                        -----------------------------------------------------
                        === DATOS DE LA SOLICITUD ===
                        ID Solicitud:  {solicitud.idSolicitud}
                        Fecha Emisión: {solicitud.fechaEmision:yyyy-MM-dd HH:mm}
                        Estado:        {solicitud.estado}
                        Tipo Servicio: {solicitud.tipoSolicitud}
                        Prioridad:     {solicitud.prioridad}
                        -----------------------------------------------------
                        === DATOS DEL INMUEBLE ===
                        Nro. Medidor:  {inmueble.nroMedidor}
                        Dirección:     {inmueble.direccion}
                        Categoría:     {inmueble.categoria}
                        Tipo Inmueble: {inmueble.tipoInmueble}
                        =====================================================
                        ";
            // RUTA DEL ARCHIVO 
            string nombreArchivo = $"Reporte_Solicitud_{solicitud.idSolicitud}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string rutaBase = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaCarpetaReporte = Path.Combine(rutaBase, "ReportesEPSAS");
            if (!Directory.Exists(rutaCarpetaReporte)){
                Directory.CreateDirectory(rutaCarpetaReporte);
            }
            string rutaReporte = Path.Combine(rutaCarpetaReporte, nombreArchivo);
            File.WriteAllText(rutaReporte, contenidoReporte);
            MessageBox.Show($"Reporte generado exitosamente en:\n{rutaReporte}", "Reporte Creado", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}