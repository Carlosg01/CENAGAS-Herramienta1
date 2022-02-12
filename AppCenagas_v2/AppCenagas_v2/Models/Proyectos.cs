using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Models
{
    public class Proyectos
    {
        [Key]
        public int Idproyecto { get; set; }

        [Required]
        [MaxLength(19)]
        public string Folio_adc { get; set; }

        [Required]
        [MaxLength(220)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(200)]
        public string InstalacionArea { get; set; }

        [Required]
        [MaxLength(200)]
        public string TipoCambio { get; set; }

        [Required]
        public string Descripcion { get; set; }

    }
}
