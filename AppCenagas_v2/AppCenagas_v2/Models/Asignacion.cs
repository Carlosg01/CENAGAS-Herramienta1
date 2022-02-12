using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Models
{
    public class Asignacion
    {
        [Key]
        public int Idasignacion { get; set; }
        
        [ForeignKey("Empleado")]
        public int Idempleado { get; set; }

        [ForeignKey("Proyecto")]
        public int Idproyecto { get; set; }

        [Required]
        //[ForeignKey("Empleado")]
        public int Idresidencia { get; set; }

        [Required]
        public string Fechaalta { get; set; }

        [Required]
        public string Fechabaja { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Funcion { get; set; }

    }
}
