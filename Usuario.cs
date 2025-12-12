using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2P2D
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string correo { get; set; }
        public int añoNac { get; set; }
        public int celular { get; set; }
        public string rol { get; set; }
        public Usuario()
        {
            idUsuario = 0;
            nombre = "No definido";
            apPaterno = "No definido";
            apMaterno = "No definido";
            correo = "No definido";
            añoNac = 0;
            celular = 0;
            rol = "No definido";
        }
        public Usuario(int idU, string nom, string aP, string aM, string corr, int anioN, int cel, string r)
        {
            idUsuario = idU;
            nombre = nom;
            apPaterno = aP;
            apMaterno = aM;
            correo = corr;
            añoNac = anioN;
            celular = cel;
            rol = r;
        }
    }
}
