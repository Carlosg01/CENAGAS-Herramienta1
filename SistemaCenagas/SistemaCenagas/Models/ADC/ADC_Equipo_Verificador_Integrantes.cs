using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Equipo_Verificador_Integrantes
    {
        [Key]
        public int Id { get; set; }
        public int Id_Equipo_Verificador_ADC { get; set; }
        public int Id_Usuario { get; set; }
        public string Estatus { get; set; }
    }
}
