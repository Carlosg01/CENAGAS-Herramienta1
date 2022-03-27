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
        public int Id_Anexo3 { get; set; } //mismo valor al correspondiente Id_ADC
        public string Fecha_Registro { get; set; }
        public string Tipo_ADC { get; set; }
        public string Residencia { get; set; }


    }
}
