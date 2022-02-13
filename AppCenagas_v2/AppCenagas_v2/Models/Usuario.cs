using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido de usuario es requerido")]
        [MaxLength(200)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email de usuario es requerido")]
        [MaxLength(200)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "La ubicación de usuario es requerida")]
        [MaxLength(200)]
        public string Ubicacion { get; set; }

        [MaxLength(500)]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "La contraseña de usuario es requerida")]
        [MaxLength(200)]
        public string Password { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        /*Notificaciones*/
        [MaxLength(20)]
        public string TareaAsignada { get; set; }
        [MaxLength(20)]
        public string MencionEnConversacion { get; set; }
        [MaxLength(20)]
        public string AgregacionAProyecto { get; set; }
        [MaxLength(20)]
        public string ActividadProyectoMiembro { get; set; }
        /*servicio de notificaciones*/
        [MaxLength(20)]
        public string NotasMensuales { get; set; }
        [MaxLength(20)]
        public string CaracteristicasPrincipales { get; set; }
        [MaxLength(20)]
        public string ActualizacionYErrores { get; set; }
    }
}
