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
        public static string ERROR_MSJ { get; set; }
        public static string SUCCESS_MSJ { get; set; }
        public static string panelTareas { get; set; }
        public static string panelArchivos { get; set; }
        public static int TipoBusqueda { get; set; }
        public static BusquedaReporte busqueda { get; set; }

        //---------USUARIOS-------
        public struct V_Usuarios
        {
            public Usuarios user;
            public string nombre_completo;
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
        public struct V_MiembrosProyecto
        {
            public Proyecto_Miembros pm;
            public string nombre_miembro;
            public string email;
        }        
        public static IEnumerable<Proyectos> vista_proyectos;
        public static Proyectos proyectos;
        public static IEnumerable<V_MiembrosProyecto> miembrosProyecto;

        //---------ADC-------
        public struct V_ADC
        {
            public ADC adc;
            public Anexo1 anexo1;
            public string residencia;
            public int id_proyecto;
            public string proyecto;
            public string proponente;
            public string lider;
            public string responsable;
            public string suplente;
        }
        public static V_ADC adc;
        public static IEnumerable<V_ADC> vista_adc;
        public static IEnumerable<V_ADC> vista_adc_propuestas;
        public static IEnumerable<V_ADC> vista_adc_cargo;


        //---------Anexo 1: Propuesta de cambio-------
        public struct V_Anexo1
        {
            public Anexo1 anexo1;
            public string proyecto;
            public string residencia;
            public string instalacion;
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
        public static V_Tareas tarea;
        public static IEnumerable<V_Tareas> vista_tareas;

        //--------ADC Archivos----------
        public struct V_Archivos
        {
            public ADC_Archivos archivo;
            public string usuario;
        }
        public static IEnumerable<V_Archivos> vista_archivos;

        //-------Vista resumen ADC--------
        public struct V_Resumen
        {
            public int id_adc;
            public string folio_adc;
            public int id_residencia;
            public string residencia;
            public int id_proyecto;
            public string proyecto;
            public float avance_ADC;
            public float avance_Pre;
            public float avance_Fisico;
        }

        public static IEnumerable<V_Resumen> resumenADC;

        //------------Vista---------
        public struct V_Cascada
        {
            public Residencias residencia;
            public Gasoductos gasoducto;
            public Tramos tramo;
        }

        public static IEnumerable<V_Cascada> vista_cascada;

        //-----------CATALOGOS--------
        public static IEnumerable<Roles> roles { get; set; }
        public static IEnumerable<Anexos> anexos { get; set; }
        public static IEnumerable<Usuarios> lideres { get; set; }
        public static IEnumerable<Usuarios> responsablesADC { get; set; }
        public static IEnumerable<Usuarios> suplentes { get; set; }
        public static IEnumerable<Residencias> residencias { get; set; }
        public static IEnumerable<Gasoductos> gasoductos { get; set; }
        public static IEnumerable<Tramos> tramos { get; set; }
        public static IEnumerable<ADC> adcs { get; set; }


    }
}
