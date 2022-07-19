using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public struct V_Usuarios
    {
        public Usuarios user;
        public string nombre_completo;
        public string Rol;
    }
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
    public struct V_Normativas
    {
        public ADC_Normativas adc_normativas;
        public string actividad;
        public string anexo;
    }
    public struct V_Normativas_PreArranque
    {
        public PreArranque_Normativas prearranque_normativas;
        public string actividad;
        public string registro;
    }
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
    public struct V_MiembrosProyecto
    {
        public Proyecto_Miembros pm;
        public string nombre_miembro;
        public string email;
    }
    public struct V_Anexo1
    {
        public ADC_Anexo1 anexo1;
        public string proyecto;
        public string residencia;
        public string instalacion;
        public string gasoducto;
        public string tramo;
    }
    public struct V_Anexo3
    {
        public ADC_Anexo3 anexo3;
        public string folio;
        public string residencia;
        public string tipoCambio1;
        public string tipoCambio2;
        public string Responsable;
        public string Director_Seguridad_Industrial;
        public string Director_Ejecutivo_Operacion;
        public string Director_Ejecutivo_Mantenimiento;
    }
    public struct V_Anexo2_PreArranque
    {
        public PreArranque_Anexo2 anexo2;
        public string residencia;
        public string instalacion;
        public string gasoducto;
        public string tramo;
        public string proyecto;
    }
    public struct V_EquipoVerificador
    {
        public ADC_Equipo_Verificador_Integrantes integrante;
        public string nombre;
        public string puesto;
        public string email;
    }
    public struct V_Tareas
    {
        public ADC_Procesos proceso;
        public ADC_Actividades actividad;
        public int Id_PropuestaCambio;
    }
    public struct V_Tareas_PreArranque
    {
        public PreArranque_Procesos proceso;
        public PreArranque_Actividades actividad;
        public int Id_PropuestaCambio;
    }
    public struct V_Archivos
    {
        public ADC_Archivos archivo;
        public string usuario;
    }
    public struct V_Cascada
    {
        public Residencias residencia;
        public Gasoductos gasoducto;
        public Tramos tramo;
    }
    public struct V_ADC_ResponsablesDocumentacionAnexo3
    {
        public ADC_Anexo3_DocumentacionResponsable responsable;
        public string nombre;
        public string puesto;
        public string email;
    }
    public struct V_EquipoVerificador_PreArranque
    {
        public PreArranque_Equipo_Verificador_Integrantes integrante;
        public string nombre;
        public string email;
    }
    public struct V_Archivos_PreArranque
    {
        public PreArranque_Archivos archivo;
        public string usuario;
    }
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

}
