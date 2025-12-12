using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp2P2D
{
    public static class AyudaDeDatos
    {
        public static List<Usuario> CargarUsuarios(string rutaArchivo)
        {
            var usuarios = new List<Usuario>();
            if (!File.Exists(rutaArchivo)) return usuarios;
            var lineas = File.ReadAllLines(rutaArchivo);
            foreach (var linea in lineas){
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var partes = linea.Split(',');
                // Estructura esperada: ID, Nombre, ApPaterno, ApMaterno, Correo, Celular, AñoNac, Contraseña 
                if (partes.Length >= 8){
                    if (int.TryParse(partes[0], out int idU) &&
                        int.TryParse(partes[6], out int anioN) &&
                        int.TryParse(partes[5], out int cel))
                    {
                        usuarios.Add(new Usuario(
                            idU: idU,
                            nom: partes[1],
                            aP: partes[2],
                            aM: partes[3],
                            corr: partes[4],
                            anioN: anioN,
                            cel: cel,
                            r: partes.Length > 8 ? partes[8] : "Solicitante" 
                        ));
                    }
                }
            }
            return usuarios;
        }
        public class SolicitudData
        {
            public List<ModeloSolicitud> Solicitudes { get; set; } = new List<ModeloSolicitud>();
            public List<ModeloInmueble> Inmuebles { get; set; } = new List<ModeloInmueble>();
        }
        public static SolicitudData CargarSolicitudesEInmuebles(string rutaArchivo)
        {
            var data = new SolicitudData();
            if (!File.Exists(rutaArchivo)) return data;
            var lineas = File.ReadAllLines(rutaArchivo);
            foreach (var linea in lineas){
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var partes = linea.Split(',');
                if (partes.Length >= 10){
                    if (int.TryParse(partes[0], out int idU) &&
                        int.TryParse(partes[1], out int idSol) &&
                        DateTime.TryParse(partes[2], out DateTime fecha))
                    {
                        data.Solicitudes.Add(new ModeloSolicitud
                        {
                            idUsuario = idU,
                            idSolicitud = idSol,
                            fechaEmision = fecha,
                            estado = partes[3],
                            tipoSolicitud = partes[4],
                            prioridad = partes[5]
                        });
                        data.Inmuebles.Add(new ModeloInmueble
                        {
                            idSolicitud = idSol, 
                            nroMedidor = partes[6],
                            direccion = partes[7],
                            categoria = partes[8],
                            tipoInmueble = partes[9]
                        });
                    }
                }
            }
            return data;
        }
    }
}
