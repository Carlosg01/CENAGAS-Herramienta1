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
        public static Anexo1_PropuestaCambio anexo1 { get; set; }
        public static IEnumerable<Anexos> anexos { get; set; }
        public static IEnumerable<Residencias> residencias { get; set; }
        public static IEnumerable<Gasoductos> gasoductos { get; set; }
        public static IEnumerable<Tramos> tramos { get; set; }
        public static IEnumerable<Instalaciones> instalaciones { get; set; }
        public static IEnumerable<Usuarios> lideres { get; set; }
        public static IEnumerable<Usuarios> responsables_adc { get; set; }
        public static IEnumerable<Usuarios> suplentes { get; set; }

        //VISTAS
        public struct VistaProyectos
        {
            public int id_proyecto;
            public string clave;
            public string nombre;
            public string descripcion;
            public string lider;
        }

        public struct VistaAnexo1
        {
            public int id_PropuestaCambio;
            public string tipoCambio;
            public string fecha;
            public int id_lider;
            public string lider;
            public int id_residencia;
            public string residencia;
            public string sector_area;
            public string planta_instalacion;
            public string proceso;
            public string prestacion_servicio;
            public string descripcion;
            public string resultados_analisis;
            public int id_proponente;
            public string proponente;
            public int id_responsableADC;
            public string responsableADC;
            public string resultados_propuesta;
            public string estatus;
        }

        public static IEnumerable<VistaProyectos> vistaProyectos { get; set; }
        public static IEnumerable<VistaAnexo1> vistaAnexo1 { get; set; }
        public static VistaAnexo1 vistaPropuesta { get; set; }
    }
}
