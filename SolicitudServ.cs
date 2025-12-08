using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    class SolicitudServ
    {
        public int idSolicitud { get; set; }
        public string tipoServicio { get; set; }
        public int fechaSolicitud { get; set; }
        public string estado { get; set; }
        public int fechaAprobRech { get; set; }
        public string observaciones { get; set; }
        public SolicitudServ()
        {
            idSolicitud = 0;
            tipoServicio = "No definido";
            fechaSolicitud = 0;
            estado = "No definido";
            fechaAprobRech = 0;
            observaciones = "No definido";
        }
        public SolicitudServ(int idS, string tipoS, int fechSol, string est, int fechAR, string obs)
        {
            idSolicitud = idS;
            tipoServicio = tipoS;
            fechaSolicitud = fechSol;
            estado = est;
            fechaAprobRech = fechAR;
            observaciones = obs;
        }
    }
}
