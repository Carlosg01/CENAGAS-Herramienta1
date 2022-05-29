using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo4
    {
        [Key]
        public int Id_Anexo4 { get; set; }
        public int Id_Anexo1 { get; set; }
        public string Fecha_Retiro_Cambio_Temporal { get; set; }
        public string Tiempo_Estimado { get; set; }
        public string Propuesta_Inicio_Operacion { get; set; }
        public int Id_Anexo3 { get; set; }
        public int Id_Residente { get; set; }
    }
}
