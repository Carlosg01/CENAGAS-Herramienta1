using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Empleado
    {
        [Key]
        public int Id_Empleado { get; set; }

        public int Id_Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(32)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(32)]
        public string Paterno { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(32)]
        public string Materno { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(9)]
        public string Titulo { get; set; }

        /*[Required(ErrorMessage = "El puesto del empleado es requerido")]
        [MaxLength(32)]
        public string Puesto { get; set; }*/

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(220)]
        public string Observaciones { get; set; }
    }
}
