using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo3_Documentacion
    {
        [Key]
        public int Id_Anexo3_Documentacion { get; set; }
        public int Id_Anexo3 { get; set; }
        public string Tipo { get; set; }
        public string Check { get; set; }
        public int Id_Responsable { get; set; }
    }
}
