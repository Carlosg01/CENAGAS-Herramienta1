using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public static class Global
    {        
        public static string session { get; set; }

        public static Usuarios usuario { get; set; }
        public static Proyectos proyecto { get; set; }
        public static ADC adc { get; set; }
        public static ADC_Actividades adc_actividad { get; set; }
        public static ADC_Procesos adc_proceso { get; set; }
        public static ADC_Normativas adc_normativas { get; set; }
        public static IEnumerable<Anexos> anexos { get; set; }
        public static IEnumerable<Residencias> residencias { get; set; }
        public static IEnumerable<Gasoductos> gasoductos { get; set; }
        public static IEnumerable<Tramos> tramos { get; set; }
        public static IEnumerable<Instalaciones> instalaciones { get; set; }
    }
}
