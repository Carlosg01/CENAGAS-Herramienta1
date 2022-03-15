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
        public int Id_Instalacion { get; set; }

        [MaxLength(200)]
        public string Instalacion { get; set; }

        [MaxLength(200)]
        public string Ut_Instalacion { get; set; }
        
        [MaxLength(200)]
        public string Clase { get; set; }

        [MaxLength(200)]
        public float Km { get; set; }

        [MaxLength(200)]
        public string Residencia { get; set; }

        [MaxLength(200)]
        public string Region { get; set; }

        [MaxLength(200)]
        public string Ut_Tramo { get; set; }

        [MaxLength(200)]
        public string Sistema { get; set; }

        public float Longitud_X_decimal { get; set; }

        public float Latitud_Y_decimal { get; set; }

        public float Altitud_Z_decimal { get; set; }

        [MaxLength(200)]
        public string Sector_Pemex { get; set; }

        [MaxLength(200)]
        public string Gmas_Pemex { get; set; }
        public int Registro_Eliminado { get; set; }


    }
}
