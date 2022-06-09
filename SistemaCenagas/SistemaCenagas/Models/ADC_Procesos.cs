using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Procesos
    {
        [Key]
        public int Id_Proceso { get; set; }
        public int Id_Actividad { get; set; }
        public int Id_ADC { get; set; }
        public float Avance { get; set; }
        public string Faltante_Comentarios { get; set; }
        public string Plan_Accion { get; set; }
        public string Terminado { get; set; }
        public string Confirmado { get; set; }
        public string Activo { get; set; }
        public int Registro_Eliminado { get; set; }
    }
}
