using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [MaxLength(32)]
        public string Nombre { get; set; }

        [MaxLength(32)]
        public string Paterno { get; set; }

        [MaxLength(32)]
        public string Materno { get; set; }

        [MaxLength(10)]
        public string Titulo { get; set; }

        [MaxLength(220)]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200)]
        public string Password { get; set; }

        [MaxLength(200)]
        public string Confirmar_Password { get; set; }

        [MaxLength(200)]
        public string Nueva_Password { get; set; }

        [MaxLength(200)]
        public string Rol { get; set; }

        [MaxLength(200)]
        public string Confirmacion_email { get; set; }

        [MaxLength(500)]
        public string Image_Url { get; set; }

        /*Notificaciones*/
        [MaxLength(20)]
        public string Tarea_Asignada { get; set; }
        [MaxLength(20)]
        public string Mencion_En_Conversacion { get; set; }
        [MaxLength(20)]
        public string Agregacion_A_Proyecto { get; set; }
        [MaxLength(20)]
        public string Actividad_Proyecto_Miembro { get; set; }
        /*servicio de notificaciones*/
        [MaxLength(20)]
        public string Notas_Mensuales { get; set; }
        [MaxLength(20)]
        public string Caracteristicas_Principales { get; set; }
        [MaxLength(20)]
        public string Actualizacion_Y_Errores { get; set; }
    }
}
