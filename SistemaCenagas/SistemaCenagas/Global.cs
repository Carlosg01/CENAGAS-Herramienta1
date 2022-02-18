using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public static class Global
    {
        public static Usuario sesionUsuario { get; set; }
        public static Empleado sesionEmpleado { get; set; }
        public static Proyectos sesionProyecto { get; set; }
        public static Asignacion asignacionProyecto { get; set; }
        public static DetalleProyecto sesionDetalleProyecto { get; set; }

        public static string session { get; set; }
        public static string nombreProyectoEmpleado { get; set; }

        public static IEnumerable<Proyectos> listaProyectos;
    }
}
