using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    public class Inmueble
    {
        //PROPIEDADES
        public int nroMedidor { get; set; }
        public string direccion { get; set; }
        public string categoria { get; set; }
        public string tipoInmueble { get; set; }
        //CONSTRUCTORES
        public Inmueble() //POR DEFECTO
        {
            nroMedidor = 0;
            direccion = "No definido";
            categoria = "No definido";
            tipoInmueble = "No definido";
            
        }
        public Inmueble(int nMed, string dir, string cat, string tipInm) //CON TODOS LOS PARAMETROS
        {
            nroMedidor = nMed;
            direccion = dir;
            categoria = cat;
            tipoInmueble = tipInm;
        }
    }
}
