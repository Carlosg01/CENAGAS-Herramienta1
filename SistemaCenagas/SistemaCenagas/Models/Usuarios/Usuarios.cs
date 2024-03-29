﻿using Microsoft.AspNetCore.Identity;
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
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int Id_Rol { get; set; }
        public string Estatus { get; set; }
        public string Confirmar_Password { get; set; }
        public string Nueva_Password { get; set; }
        public int Id_Puesto { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Titulo { get; set; }
        public string Observaciones { get; set; }
        public string Image_Url { get; set; }
        public string Notificacion_Proyecto { get; set; }
        public string Notificacion_Tarea { get; set; }
        public string Notificacion_ADC { get; set; }
        public int Eliminado { get; set; }
    }

    
}
