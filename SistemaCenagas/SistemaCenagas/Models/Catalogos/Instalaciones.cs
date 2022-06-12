using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Instalaciones
    {
        [Key]
        public int Id { get; set; }
        public string Instalacion { get; set; }
        public string Ut_Instalacion { get; set; }
        public string Clase { get; set; }
        public float Km { get; set; }
        public string Residencia { get; set; }
        public string Region { get; set; }
        public string Ut_Tramo { get; set; }
        public string Sistema { get; set; }
        public float Longitud_X_decimal { get; set; }
        public float Latitud_Y_decimal { get; set; }
        public float Altitud_Z_decimal { get; set; }
        public string Sector_Pemex { get; set; }
        public string Gmas_Pemex { get; set; }
        public int Registro_Eliminado { get; set; }
    }
}
