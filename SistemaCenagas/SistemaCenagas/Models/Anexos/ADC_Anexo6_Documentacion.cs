using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Anexo6_Documentacion
    {
        [Key]
        public int Id { get; set; }
        public int Id_Anexo6 { get; set; }
        public int Id_Elemento_Catalogo { get; set; }
        public string Check { get; set; }
    }

    public class ADC_Anexo6_Documentacion_Catalogo
    {
        [Key]
        public int Id { get; set; }
        public string Elemento { get; set; }
        public int Seccion { get; set; }
    }
}
