using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Residencias
    {
        [Key]
        public int Id_Residencia { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }
        public int Registro_Eliminado { get; set; }
    }
}
