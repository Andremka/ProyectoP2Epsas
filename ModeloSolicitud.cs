using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    public class ModeloSolicitud
    {
        //PROPIEDADES QUE SE VAN A LEER DEL ARCHIVO DE TEXTO
        public int idUsuario { get; set; }
        public int idSolicitud { get; set; }
        public DateTime fechaEmision { get; set; }
        public string estado { get; set; }
        public string tipoSolicitud { get; set; }
        public string prioridad { get; set; }
    }
}
