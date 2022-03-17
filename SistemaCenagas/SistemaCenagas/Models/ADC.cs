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
        public string Folio { get; set; }
        //Usuarios-------------------------
        public int Id_ProponenteCambio { get; set; }
        public int Id_Lider { get; set; }
        public int Id_ResponsableADC { get; set; }
        public int Id_Suplente { get; set; }
        //-------------------------------
        public string Fecha_Actualizacion { get; set; }
        public int Registro_Eliminado { get; set; }

    }
}
