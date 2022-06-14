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
        public static ADC_Procesos proceso { get; set; }
        public static PreArranque_Procesos proceso_prearranque { get; set; }

        public static int ADMINISTRADOR { get; set; }
        public static int SUPERADMIN { get; set; }
        public static int RESPONSABLE_ADC { get; set; }
        public static int RESPONSABLE_PREARRANQUE { get; set; }
        public static int SUPLENTE { get; set; }
        public static int LIDER_EQUIPO_VERIFICADOR { get; set; }
        public static int EQUIPO_VERIFICADOR { get; set; }
        public static int EMPLEADO { get; set; }

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

        //---------Actividades ADC y Pre-Arranque-------
        
        public static ADC_Actividades actividadADC;
        public static IEnumerable<ADC_Actividades> vista_actividadesADC;

        public static PreArranque_Actividades actividadPreArranque;
        public static IEnumerable<PreArranque_Actividades> vista_actividadesPreArranque;

        //---------ADC y PreArranque Normativas-------
        public struct V_Normativas
        {
            public ADC_Normativas adc_normativas;
            public string actividad;
            public string anexo;
        }
        public static V_Normativas normativas;
        public static IEnumerable<V_Normativas> vista_normativas;

        public struct V_Normativas_PreArranque
        {
            public PreArranque_Normativas prearranque_normativas;
            public string actividad;
            public string registro;
        }
        public static V_Normativas_PreArranque normativasPreArranque;
        public static IEnumerable<V_Normativas_PreArranque> vista_normativas_prearranque;

        //---------ADC y PreArranque-------
        public struct V_ADC
        {
            public ADC adc;
            public ADC_Anexo1 anexo1;
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

        public struct V_PreArranque
        {
            public PreArranque prearranque;
            public PreArranque_Anexo2 anexo2;
            public string residencia;
            public int id_proyecto;
            public string proyecto;
            public string lider;
            public string responsable;
            public string suplente;
        }
        public static V_PreArranque prearranque;
        public static IEnumerable<V_PreArranque> vista_prearranque;
        public static IEnumerable<V_PreArranque> vista_prearranque_propuestas;
        public static IEnumerable<V_PreArranque> vista_prearranque_cargo;

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

        


        //---------Anexo 1: Propuesta de cambio-------
        public struct V_Anexo1
        {
            public ADC_Anexo1 anexo1;
            public string proyecto;
            public string residencia;
            public string instalacion;
            public string gasoducto;
            public string tramo;
        }
        public static V_Anexo1 anexo1;

        //---------Anexo 3: Propuesta de cambio-------
        public struct V_Anexo3
        {
            public ADC_Anexo3 anexo3;
            public string folio;
            public string residencia;
            public string tipoCambio1;
            public string tipoCambio2;
        }
        public static V_Anexo3 anexo3;

        //---------Anexo 2: Propuesta de cambio-------
        public struct V_Anexo2_PreArranque
        {
            public PreArranque_Anexo2 anexo2;
            public string residencia;
            public string instalacion;
            public string gasoducto;
            public string tramo;
            public string proyecto;
        }
        public static V_Anexo2_PreArranque anexo2_prearranque;

        //--------Equipo verificador
        public struct V_EquipoVerificador
        {
            public ADC_Equipo_Verificador_Integrantes integrante;
            public string nombre;
            public string email;
        }

        public struct V_EquipoVerificador_PreArranque
        {
            public PreArranque_Equipo_Verificador_Integrantes integrante;
            public string nombre;
            public string email;
        }

        //---------ADC y PreArranque Tareas-------
        public struct V_Tareas
        {
            public ADC_Procesos proceso;
            public ADC_Actividades actividad;
            public int Id_PropuestaCambio;
        }
        public static V_Tareas tarea;
        public static IEnumerable<V_Tareas> vista_tareas;

        public struct V_Tareas_PreArranque
        {
            public PreArranque_Procesos proceso;
            public PreArranque_Actividades actividad;
            public int Id_PropuestaCambio;
        }
        public static V_Tareas_PreArranque tarea_prearranque;
        public static IEnumerable<V_Tareas_PreArranque> vista_tareas_prearranque;

        //--------ADC y PreArranque Archivos----------
        public struct V_Archivos
        {
            public ADC_Archivos archivo;
            public string usuario;
        }
        public static IEnumerable<V_Archivos> vista_archivos;

        public struct V_Archivos_PreArranque
        {
            public PreArranque_Archivos archivo;
            public string usuario;
        }
        public static IEnumerable<V_Archivos_PreArranque> vista_archivos_prearranque;

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
        public static IEnumerable<Puestos> puestos { get; set; }
        public static IEnumerable<ADC_Anexos> anexos { get; set; }
        public static IEnumerable<Usuarios> lideres { get; set; }
        public static IEnumerable<Usuarios> responsablesADC { get; set; }
        public static IEnumerable<Usuarios> responsablesPreArranque { get; set; }
        public static IEnumerable<Usuarios> suplentes { get; set; }
        public static IEnumerable<Usuarios> equipo_verificador { get; set; }
        public static IEnumerable<Residencias> residencias { get; set; }
        public static IEnumerable<Gasoductos> gasoductos { get; set; }
        public static IEnumerable<Tramos> tramos { get; set; }
        public static IEnumerable<ADC> adcs { get; set; }
        public static IEnumerable<V_ADC> adcs_con_prearranque { get; set; }
        public static IEnumerable<ADC_Anexo3_CatalogoTipoDocumentacion> anexo3_CatalogoTipoDocumentacion { get; set; }


    }
}
