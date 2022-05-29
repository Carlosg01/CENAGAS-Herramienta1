using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public class Anexo3
    {
        [Key]
        public int Id_Anexo3 { get; set; }
        public string Fecha_Registro { get; set; }
        public string Tipo_ADC { get; set; }
        
        public int Id_Anexo1 { get; set; }
        /*
        public string Residencia { get; set; }
        public string LiderADC { get; set; }
        public string Id_Equipo_Verificador { get; set; }
        */

        //Elementos a modificar
        public string Equipo { get; set; }
        public string Instrumento { get; set; }
        public string Componente_o_Dispositivo { get; set; }
        public string Valvula { get; set; }
        public string Accesorio_Ducto { get; set; }
        public string Estacion_Compresion { get; set; }
        public string Estacion_Medicion_y_Regulacion { get; set; }
        public string Trampa_Envios_y_Recibo_Diablos { get; set; }
        public string Otro_Elemento { get; set; }
        //---------------------------------

        public string Numero_Identificacion { get; set; }
        public string Descripcion_Riesgo { get; set; }
        public double Inversion_Cambio { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
        public string Justificacion { get; set; }
        public string Descripcion_Cambio { get; set; }

        //Documentacion
        public string Otra_Documentacion { get; set; }

        public int Id_Responsable_ADC { get; set; }
        public int Id_Director_Seguridad_Industrial { get; set; }
        public int Id_Director_Ejecutivo_Operacion { get; set; }
        public int Id_Director_Ejecutivo_Mantenimiento_y_Seguridad { get; set; }

        //
        public int Id_Anexo2 { get; set; }

    }
}
