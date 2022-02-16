using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Asignacion
    {
        [Key]
        public int Id_Asignacion { get; set; }
        
        [ForeignKey("Empleado")]
        public int Id_Empleado { get; set; }

        [ForeignKey("Proyecto")]
        public int Id_Proyecto { get; set; }

        [Required]
        //[ForeignKey("Empleado")]
        public int Id_Residencia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Fecha_alta { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Fecha_baja { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200)]
        public string Funcion { get; set; }

    }
}
