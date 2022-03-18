using SistemaCenagas.Data;
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

        //---------USUARIOS-------
        public struct V_Usuarios
        {
            public Usuarios user;
            public string Rol;
        }
        public static V_Usuarios usuario;
        public static V_Usuarios session_usuario;
        public static IEnumerable<V_Usuarios> vista_usuarios;

        //---------ADC Actividades-------
        
        public static ADC_Actividades actividadADC;
        public static IEnumerable<ADC_Actividades> vista_actividadesADC;

        //---------ADC Normativas-------
        public struct V_Normativas
        {
            public ADC_Normativas adc_normativas;
            public string actividad;
            public string anexo;
        }
        public static V_Normativas normativas;
        public static IEnumerable<V_Normativas> vista_normativas;

        //---------Proyectos-------
        public static Proyectos proyectos;
        public static IEnumerable<Proyectos> vista_proyectos;

        //---------ADC-------
        public struct V_ADC
        {
            public ADC adc;
            public string proyecto;
            public string proponente;
            public string lider;
            public string responsable;
            public string suplente;
        }
        public static V_ADC adc;
        public static IEnumerable<V_ADC> vista_adc;

        //---------Anexo 1: Propuesta de cambio-------
        public struct V_Anexo1
        {
            public Anexo1 anexo1;
            public string proyecto;
            public string residencia;
            public string gasoducto;
            public string tramo;
        }
        public static V_Anexo1 anexo1;

        //---------ADC Tareas-------
        public struct V_Tareas
        {
            public ADC_Procesos proceso;
            public string actividad;
            public int Id_PropuestaCambio;
        }
        public static IEnumerable<V_Tareas> tareas;

        //-----------CATALOGOS--------
        public static IEnumerable<Roles> roles { get; set; }
        public static IEnumerable<Anexos> anexos { get; set; }
        public static IEnumerable<Usuarios> lideres { get; set; }
        public static IEnumerable<Usuarios> responsablesADC { get; set; }
        public static IEnumerable<Usuarios> suplentes { get; set; }
        public static IEnumerable<Residencias> residencias { get; set; }
        public static IEnumerable<Gasoductos> gasoductos { get; set; }
        public static IEnumerable<Tramos> tramos { get; set; }
    }
}
