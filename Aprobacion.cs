using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
     class Aprobacion
    {
        public int idAprobacion { get; set; }
        public int fechaRevision { get; set; }
        public string desicionFinal { get; set; }
        public string motivoDecisiion { get; set; }
        public Aprobacion()
        {
            idAprobacion = 0;
            fechaRevision = 0;
            desicionFinal = "No definido";
            motivoDecisiion = "No definido";
        }
        public Aprobacion(int idAp, int fechRev, string descFin, string motDesc)
        {
            idAprobacion = idAp;
            fechaRevision = fechRev;
            desicionFinal = descFin;
            motivoDecisiion = motDesc;
        }
    }
}
