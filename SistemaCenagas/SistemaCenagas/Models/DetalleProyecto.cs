using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class DetalleProyecto
    {
        [Key]
        public int Id_DetalleProyecto { get; set; }
        
        public int Id_Proyecto { get; set; }
        
        public int Id_Residencia { get; set; }
        
        public int Id_Asignacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public int No_Desarrollo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(500)]
        public string Desarrollo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion_Actividad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public int Avance { get; set; }

        public string Faltante_Comentarios { get; set; }

        public string Comentarios { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Plan_Accion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(500)]
        public string Anexos { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200)]
        public string Tipo_Proyecto { get; set; }
    }
}
