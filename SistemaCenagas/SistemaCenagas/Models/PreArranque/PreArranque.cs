using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class PreArranque
    {
        [Key]
        public int Id { get; set; }
        public string Con_ADC { get; set; }
        public int Id_ADC { get; set; } //En caso de que exista
        public int Id_Proyecto { get; set; }
        public string Folio { get; set; }
        //Usuarios-------------------------
        public int Id_Responsable { get; set; }
        public int Id_Suplente { get; set; }
        public int Id_LiderEquipoVerificador { get; set; }
        //-------------------------------
        public string Fecha_Actualizacion { get; set; }
        public int Eliminado { get; set; }

    }
}
