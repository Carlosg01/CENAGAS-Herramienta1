using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class BusquedaReporte
    {
        public int Id_Busqueda { get; set; }
        public string Busqueda { get; set; }
        public int Id_Filtro { get; set; }
        public string Filtro { get; set; }
    }
}
