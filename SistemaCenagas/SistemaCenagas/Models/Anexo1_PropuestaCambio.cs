using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo1_PropuestaCambio
    {
        [Key]
        public int Id_PropuestaCambio { get; set; }
        public int Id_Proyecto { get; set; }
        public string Tipo_Cambio { get; set; }
        public string Fecha { get; set; }
        public int Id_Residencia { get; set; }
        public string Sector_Area { get; set; } //Gasoducto
        public string Planta_Instalacion { get; set; } //Tramo 
        public string Proceso { get; set; }
        public string Prestacion_Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Resultados_Analisis { get; set; }
        public int Id_ProponenteCambio { get; set; }
        public int Id_ResponsableADC { get; set; }
        public string Resultados_Propuesta { get; set; }
        public string Estatus { get; set; }
                                                      
    }
}
