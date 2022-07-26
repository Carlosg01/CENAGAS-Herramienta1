using SistemaCenagas.Data;
using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public class Prueba
    {
        public string Id { get; set; }
        //public Usuarios? user { get; set; }
        public Prueba() { }
    }
    public class Global
    {
        public Global() { }
        public string session { get; set; }
        public string ERROR_MSJ { get; set; }
        public string SUCCESS_MSJ { get; set; }
        public string panelTareas { get; set; }
        public string panelArchivos { get; set; }
        public int TipoBusqueda { get; set; }
        public BusquedaReporte busqueda { get; set; }
        public ADC_Procesos proceso { get; set; }
        public PreArranque_Procesos proceso_prearranque { get; set; }

        public bool VISTA_PDF { get; set; }

        public int ADMINISTRADOR { get; set; }
        public int SUPERADMIN { get; set; }
        public int RESPONSABLE_ADC { get; set; }
        public int RESPONSABLE_PREARRANQUE { get; set; }
        public int SUPLENTE { get; set; }
        public int LIDER_EQUIPO_VERIFICADOR { get; set; }
        public int EQUIPO_VERIFICADOR { get; set; }
        public int EMPLEADO { get; set; }

        //---------USUARIOS-------
        
        public V_Usuarios usuario;
        public V_Usuarios session_usuario;
        public IEnumerable<V_Usuarios> vista_usuarios;

        //---------Actividades ADC y Pre-Arranque-------
        
        public ADC_Actividades actividadADC;
        public IEnumerable<ADC_Actividades> vista_actividadesADC;

        public PreArranque_Actividades actividadPreArranque;
        public IEnumerable<PreArranque_Actividades> vista_actividadesPreArranque;

        //---------ADC y PreArranque Normativas-------
        
        public V_Normativas normativas;
        public IEnumerable<V_Normativas> vista_normativas;

        
        public V_Normativas_PreArranque normativasPreArranque;
        public IEnumerable<V_Normativas_PreArranque> vista_normativas_prearranque;

        //---------ADC y PreArranque-------
        
        public V_ADC adc;
        public IEnumerable<V_ADC> vista_adc;
        public IEnumerable<V_ADC> vista_adc_propuestas;
        public IEnumerable<V_ADC> vista_adc_cargo;

        
        public V_PreArranque prearranque;
        public IEnumerable<V_PreArranque> vista_prearranque;
        public IEnumerable<V_PreArranque> vista_prearranque_propuestas;
        public IEnumerable<V_PreArranque> vista_prearranque_cargo;

        //---------Proyectos-------
             
        public IEnumerable<Proyectos> vista_proyectos;
        public Proyectos proyectos;
        public IEnumerable<V_MiembrosProyecto> miembrosProyecto;

        


        //---------Anexo 1: Propuesta de cambio-------
        
        public V_Anexo1 anexo1;

        //---------Anexo 3: Propuesta de cambio-------
        
        public V_Anexo3 anexo3;

        //---------Anexo 2: Propuesta de cambio-------
        
        public V_Anexo2_PreArranque anexo2_prearranque;

        //--------Equipo verificador
        
        

        

        //---------ADC y PreArranque Tareas-------
        
        public V_Tareas tarea;
        public IEnumerable<V_Tareas> vista_tareas;

        
        public V_Tareas_PreArranque tarea_prearranque;
        public IEnumerable<V_Tareas_PreArranque> vista_tareas_prearranque;

        //--------ADC y PreArranque Archivos----------
        
        public IEnumerable<V_Archivos> vista_archivos;

        
        public IEnumerable<V_Archivos_PreArranque> vista_archivos_prearranque;

        //-------Vista resumen ADC--------
        

        public IEnumerable<V_Resumen> resumenADC;

        //------------Vista---------
        

        public IEnumerable<V_Cascada> vista_cascada;

        //-----------CATALOGOS--------
        public IEnumerable<Roles> roles { get; set; }
        public IEnumerable<Puestos> puestos { get; set; }
        public IEnumerable<ADC_Anexos> anexos { get; set; }
        public IEnumerable<Proyectos> lista_proyectos_adc { get; set; }
        public IEnumerable<Proyectos> lista_proyectos_prearranque { get; set; }
        public IEnumerable<Usuarios> lideres { get; set; }
        public IEnumerable<Usuarios> responsablesADC { get; set; }
        public IEnumerable<Usuarios> responsablesPreArranque { get; set; }
        public IEnumerable<Usuarios> suplentes { get; set; }
        public IEnumerable<Usuarios> equipo_verificador { get; set; }
        public IEnumerable<Residencias> residencias { get; set; }
        public IEnumerable<Estados> estados { get; set; }
        public IEnumerable<Unidad> unidades { get; set; }
        public IEnumerable<Direccion_Ejecutiva> direcciones_ejecutivas { get; set; }
        public IEnumerable<Gasoductos> gasoductos { get; set; }
        public IEnumerable<Tramos> tramos { get; set; }
        public IEnumerable<ADC> adcs { get; set; }
        public IEnumerable<ADC> adcsPrearranque { get; set; }
        public IEnumerable<V_ADC> adcs_con_prearranque { get; set; }
        public IEnumerable<ADC_Anexo3_CatalogoTipoDocumentacion> anexo3_CatalogoTipoDocumentacion { get; set; }
        public List<V_ADC_ResponsablesDocumentacionAnexo3> responsablesDocumentacionAnexo3 { get; set; }

        public List<PreArranque_Anexo1_Avtividades_Model> modelActividades { get; set; }
        public List<V_EquipoVerificador_PreArranque> equipoVerificador_PreArranque { get; set; }
    }

    //Estructura de vistas
}
