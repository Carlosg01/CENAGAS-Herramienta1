using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public static class Global
    {
        public static Usuario _usuario { get; set; }
        public static Proyectos _proyecto { get; set; }
        public static Asignacion _asignacion { get; set; }
        public static DetalleProyecto _detalleProyecto { get; set; }
        public static IEnumerable<DetalleProyecto> _detallesProyecto { get; set; }
        public static string despliegaProyectos { get; set; }
        public static string session { get; set; }
        public static string nombreProyectoEmpleado { get; set; }

        public static IEnumerable<Proyectos> listaProyectos;
    }
}
