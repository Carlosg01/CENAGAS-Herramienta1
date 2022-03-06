using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC
    {
        [Key]
        public int Id_ADC { get; set; }
        public int Id_Proyecto { get; set; }
        //Usuarios-------------------------
        public int Id_ResponsableADC { get; set; }
        public int Id_Suplente { get; set; }
        public int Id_Lider { get; set; }
        //-------------------------------
        public int Fecha_Actualizacion { get; set; }

    }
}
