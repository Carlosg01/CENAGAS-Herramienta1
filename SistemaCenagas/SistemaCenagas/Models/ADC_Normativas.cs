using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Normativas
    {
        [Key]
        public int Id_Normativa { get; set; }
        public int Id_Actividad { get; set; }
        public string Clave { get; set; }
        public string Responsable { get; set; }
        public string Descripcion { get; set; }
        public int Id_Anexo { get; set; }

    }
}
