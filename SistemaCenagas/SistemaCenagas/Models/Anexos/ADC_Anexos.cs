using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexos
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Eliminado { get; set; }
    }
}
