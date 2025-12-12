using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    public class SolicitudServ
    {
        public int idSolicitud { get; set; }
        public DateTime fechaEmision { get; set; }
        public string estado { get; set; }
        public string tipoServicio { get; set; }
        public string prioridad { get; set; }
        public SolicitudServ()
        {
            idSolicitud = 0;
            
            estado = "No definido";
            tipoServicio = "No definido";
            prioridad = "No definido";
        }
        public SolicitudServ(int idS, DateTime fechEm, string est, string tipoS, string prio)
        {
            idSolicitud = idS;
            fechaEmision = fechEm;
            estado = est;
            tipoServicio = tipoS;
            prioridad = prio;
        }
    }
}
