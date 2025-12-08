using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    class DatoServicio
    {
        public string direccion { get; set; }
        public int nroMedidor { get; set; }
        public string tipoInmueble { get; set; }
        public int areaTerreno { get; set; }
        public DatoServicio()
        {
            direccion = "No definido";
            nroMedidor = 0;
            tipoInmueble = "No definido";
            areaTerreno = 0;
        }
        public DatoServicio(string dir, int nMed, string tipInm, int aTer)
        {
            direccion = dir;
            nroMedidor = nMed;
            tipoInmueble = tipInm;
            areaTerreno = aTer;
        }
    }
}
