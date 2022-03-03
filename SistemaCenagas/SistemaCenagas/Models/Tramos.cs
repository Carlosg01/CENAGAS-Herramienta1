using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Tramos
    {
        [Key]
        public int Id_Tramo { get; set; }

        [MaxLength(200)]
        public string Ut_Tramo { get; set; }

        [MaxLength(200)]
        public string Tramo { get; set; }

        public float Km_Inicio { get; set; }

        public float Km_Fin { get; set; }

        public float Longitud_Metros { get; set; }

        public float Diametro { get; set; }

        public float Espesor_Nominal { get; set; }

        public float SMYS { get; set; }

        [MaxLength(200)]
        public string Fecha_Construccion { get; set; }

        [MaxLength(200)]
        public string Residencia { get; set; }

        [MaxLength(200)]
        public string Ut_Gasoducto { get; set; }



    }
}
