using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    public class ModeloInmueble
    {
        //PROPIEDADES QUE SE VAN A LEER DEL ARCHIVO DE TEXTO
        public int idSolicitud { get; set; } 
        public string nroMedidor { get; set; }
        public string direccion { get; set; }
        public string categoria { get; set; }
        public string tipoInmueble { get; set; }
    }
}
