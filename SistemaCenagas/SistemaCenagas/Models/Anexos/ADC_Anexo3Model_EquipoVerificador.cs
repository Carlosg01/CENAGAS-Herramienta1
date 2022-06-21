using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Anexo3Model_EquipoVerificador
    {
        public ADC_Anexo3 anexo3 { get; set; }
        public IFormCollection RadioTipo { get; set; }
        public List<string> miembros { get; set; }
        public List<int> idMiembro { get; set; }
        public ADC_Anexo3_Documentacion[] documentacion { get; set; }
    }
}
