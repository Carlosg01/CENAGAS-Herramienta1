using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Anexo3_Documentacion
    {
        [Key]
        public int Id { get; set; }
        public int Id_Anexo3 { get; set; }
        public int Id_Tipo { get; set; }
        public string Check { get; set; }
        public int Id_Responsable { get; set; }
    }
}
