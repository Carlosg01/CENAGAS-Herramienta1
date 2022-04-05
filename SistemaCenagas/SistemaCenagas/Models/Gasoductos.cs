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
        public string Ut_Gasoducto { get; set; }
        public string Gasoducto { get; set; }
        public string Sistema { get; set; }
        public string Ut_Ducto { get; set; }
        public float Diametro_o_pulgadas { get; set; }
        public string Denominacion { get; set; }
        public float Longitud_Metros { get; set; }
        public string Ut_Pemex { get; set; }
        public string Descripcion_Pemex { get; set; }
        public int Registro_Eliminado { get; set; }
    }
}
