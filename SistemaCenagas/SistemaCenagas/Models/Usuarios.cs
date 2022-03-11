using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Usuarios
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

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
        public string Estatus { get; set; }

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

        [MaxLength(500)]
        public string Image_Url { get; set; }
    }
}
