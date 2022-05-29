using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo6_Documentacion
    {
        [Key]
        public int Id_Anexo6_Documentacion { get; set; }
        public int Id_Anexo6 { get; set; }
        public string Elemento { get; set; }
        public string Check { get; set; }
        public string Seccion { get; set; }
    }
}
