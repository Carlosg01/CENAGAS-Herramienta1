using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo2
    {
        [Key]
        public int Id_Anexo2 { get; set; }
        public int Id_Proyecto { get; set; }
        /*
        public int Id_Proyecto { get; set; }
        public string Folio { get; set; }
        public string TipoCambio { get; set; }
        public string TipoADC { get; set; }
        public string Residencia { get; set; }
        public string Descripcion { get; set; }
        public string Estatus { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaCierre { get; set; }
        public string ResponsableADC { get; set; }
        public string LiderEquipoVerificador { get; set; }
        */
    }
}
