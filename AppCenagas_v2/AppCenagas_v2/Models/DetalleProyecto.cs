using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Models
{
    public class DetalleProyecto
    {
        [Key]
        public int IdDetalleProyecto { get; set; }
        
        [ForeignKey("Proyectos")]
        public int IdProyecto { get; set; }
        
        [Required]
        public int IdResidencia { get; set; }
        
        [ForeignKey("Asignacion")]
        public int IdAsignacion { get; set; }

        [Required]
        public int NoDesarrollo { get; set; }

        [Required]
        [MaxLength(500)]
        public string Desarrollo { get; set; }

        [Required]
        [MaxLength(500)]
        public string DescripcionActividad { get; set; }

        [Required]
        public int Avance { get; set; }

        [Required]
        public string FaltanteComentarios { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comentarios { get; set; }

        [Required]
        [MaxLength(500)]
        public string PlanAccion { get; set; }

        [Required]
        [MaxLength(500)]
        public string Anexos { get; set; }

        [Required]
        [MaxLength(200)]
        public string TipoProyecto { get; set; }
    }
}
