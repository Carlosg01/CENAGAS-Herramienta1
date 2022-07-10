using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class ADC_Anexo4
    {
        [Key]
        public int Id { get; set; }
        public int Id_Anexo1 { get; set; }
        public string Fecha_Retiro_Cambio_Temporal { get; set; }
        public string Tiempo_Estimado { get; set; }
        public string Propuesta_Inicio_Operacion { get; set; }
        public int Id_Anexo3 { get; set; }
        public int Id_Residente { get; set; }
    }

    public class ADC_Anexo4_Model
    {
        public string Folio_ADC { get; set; }
        public string Sector_Area { get; set; }
        public string Planta_Instalacion { get; set; }
        public string Proceso { get; set; }
        public string Prestacion_Servicio { get; set; }
        public string Clasificacion_Cambio { get; set; }
        public Global.V_Anexo1 anexo1 { get; set; }
        public ADC_Anexo4 anexo4 { get; set; }
        public string Responsable { get; set; }
        public string Lider_EquipoVerificador { get; set; }
        public List<string> Integrantes_EquipoVerificador { get; set; }
        public string Director_Ejecutivo_Operacion { get; set; }
        public string Director_Ejecutivo_Mantenimiento { get; set; }
        public string Residente { get; set; }
    }
}
