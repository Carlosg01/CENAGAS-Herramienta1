using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Anexo1
    {
        [Key]
        public int Id { get; set; }
        public int Id_Proyecto { get; set; }
        public string Tipo_Cambio { get; set; }
        public string Fecha { get; set; }
        public int Id_Residencia { get; set; }
        public string Ut_Gasoducto { get; set; } //Gasoducto
        public string Ut_Tramo { get; set; } //Tramo 
        public string Proceso { get; set; }
        public string Prestacion_Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Resultados_Analisis { get; set; }
        public string Resultados_Propuesta { get; set; }
        public string Estatus { get; set; }
        public int Registro_Eliminado { get; set; }

        //
        public string EstatusADC { get; set; }
        public string PresentoARP { get; set; }
        public int Id_Anexo2 { get; set; }
    }
}
