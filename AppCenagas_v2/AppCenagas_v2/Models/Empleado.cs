using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Models
{
    public class Empleado
    {
        [Key]
        public int Idempleado { get; set; }

        [Required(ErrorMessage = "El nombre del empleado es requerido")]
        [MaxLength(32)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno del empleado es requerido")]
        [MaxLength(32)]
        public string Paterno { get; set; }

        [Required(ErrorMessage = "El apellido materno del empleado es requerido")]
        [MaxLength(32)]
        public string Materno { get; set; }

        [Required(ErrorMessage = "El titulo del empleado es requerido")]
        [MaxLength(9)]
        public string Titulo { get; set; }

        /*[Required(ErrorMessage = "El puesto del empleado es requerido")]
        [MaxLength(32)]
        public string Puesto { get; set; }*/

        [Required(ErrorMessage = "Las observaciones del empleado es requerido")]
        [MaxLength(220)]
        public string Observaciones { get; set; }
    }
}
