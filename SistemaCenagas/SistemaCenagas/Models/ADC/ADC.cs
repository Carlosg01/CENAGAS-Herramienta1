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
        public int Id { get; set; }
        public int Id_Proyecto { get; set; }
        public string Folio { get; set; }
        //Usuarios-------------------------
        public int Id_ProponenteCambio { get; set; }
        public int Id_ResponsableADC { get; set; }
        public int Id_Suplente { get; set; }
        public int Id_LiderEquipoVerificador { get; set; }
        //-------------------------------
        public string Fecha_Actualizacion { get; set; }
        public string PreArranque { get; set; }
        public int Eliminado { get; set; }

    }
}
