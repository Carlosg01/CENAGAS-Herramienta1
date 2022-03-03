using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Gasoductos
    {
        [Key]
        public int Id_Instalacion { get; set; }

        [MaxLength(200)]
        public string Ut_Gasoducto { get; set; }

        [MaxLength(200)]
        public string Gasoducto { get; set; }

        [MaxLength(200)]
        public string Sistema { get; set; }

        [MaxLength(200)]
        public string Ut_Ducto { get; set; }

        public float Diametro_o_pulgadas { get; set; }

        [MaxLength(200)]
        public string Denominacion { get; set; }

        public float Longitud_Metros { get; set; }

        [MaxLength(200)]
        public string Ut_Pemex { get; set; }

        [MaxLength(200)]
        public string Descripcion_Pemex { get; set; }
    }
}
