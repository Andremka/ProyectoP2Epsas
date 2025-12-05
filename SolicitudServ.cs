using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    class SolicitudServ
    {
        public string ci { get; set; }
        public string esPropietario { get; set; }
        public string direccion { get; set; }
        public string nroCasa { get; set; }
        public string coordenadas { get; set; }
        public string tipoInmueble { get; set; }
        public string tipoSolicitud { get; set; }
        public string usoServicio { get; set; }
        public SolicitudServ()
        {
            ci = "No definido";
            esPropietario = "No definido";
            direccion = "No definido";
            nroCasa = "No definido";
            coordenadas = "No definido";
            tipoInmueble = "No definido";
            tipoSolicitud = "No definido";
            usoServicio = "No definido";
        }
        public SolicitudServ(string nroCi, string esProp, string dir, string nCasa, string coord, string tipInm, string tipSol, string uServ)
        {
            ci = "No definido";
            esPropietario = "No definido";
            direccion = "No definido";
            nroCasa = "No definido";
            coordenadas = "No definido";
            tipoInmueble = "No definido";
            tipoSolicitud = "No definido";
            usoServicio = "No definido";
        }
    }
}
