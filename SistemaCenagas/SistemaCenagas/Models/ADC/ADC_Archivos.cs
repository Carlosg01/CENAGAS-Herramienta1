using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Archivos
    {
        [Key]
        public int Id { get; set; }
        public int Id_ADC { get; set; }
        public int Id_Proceso { get; set; }
        public string Actividad { get; set; }
        public int Id_Usuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public float Size { get; set; }
        public string Ubicacion { get; set; }
        public int Eliminado { get; set; }
    }
}
