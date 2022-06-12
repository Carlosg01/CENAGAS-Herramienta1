using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class PreArranque_Actividades
    {
        [Key]
        public int Id { get; set; }
        public string Actividad { get; set; }
        public int Eliminado { get; set; }
    }
}
