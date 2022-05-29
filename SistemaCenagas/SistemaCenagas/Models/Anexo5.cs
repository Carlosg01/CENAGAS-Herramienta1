using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo5
    {
        [Key]
        public int Id_Anexo5 { get; set; }
        public int Id_Anexo1 { get; set; }
        public string Descripcion_Cambio_Temporal { get; set; }
        public int Id_Responsable_Cambio_Temporal { get; set; }
        public string Dia_Cambio { get; set; }
        public string Mes_Cambio { get; set; }
        public string Anio_Cambio { get; set; }
        public string Dia_Retiro { get; set; }
        public string Mes_Retiro { get; set; }
        public string Anio_Retiro { get; set; }
        public int Id_Anexo3 { get; set; }

    }
}
