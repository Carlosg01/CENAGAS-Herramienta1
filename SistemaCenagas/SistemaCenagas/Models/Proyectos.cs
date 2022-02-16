using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Proyectos
    {

        [Key]
        public int Id_Proyecto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(19)]
        public string Folio_adc { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(220)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200)]
        public string Instalacion_Area { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200)]
        public string Tipo_Cambio { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; }

    }
}
