using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Anexo6
    {
        [Key]
        public int Id { get; set; }
        public string Fecha_Recepcion { get; set; }
        public int Id_Anexo1 { get; set; }
        public int Id_anexo3 { get; set; }
        //Periodo de tiempo real del cambio
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }

    }

    public class ADC_Anexo6_Model
    {
        public ADC_Anexo6 anexo6 { get; set; }
        public List<ADC_Anexo6_Documentacion> documentacion { get; set; }
    }
}
