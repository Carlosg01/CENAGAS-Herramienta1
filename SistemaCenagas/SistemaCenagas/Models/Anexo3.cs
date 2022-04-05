using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo3
    {
        [Key]
        public int Id_Anexo3 { get; set; }
        public int Id_ADC { get; set; }
        public string Tipo_Cambio { get; set; }
        public int Id_Residencia { get; set; }
        public int Id_Lider { get; set; }

    }
}
