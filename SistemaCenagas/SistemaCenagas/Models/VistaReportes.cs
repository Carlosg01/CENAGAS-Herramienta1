using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Models
{
    public static class Controls
    {
        public static string mark_yes = "&#9746;";
        public static string mark_no = "&#9744;";
        public static string url_logo = "~/assets/logo.png";

    }
    public class V_Reporte_Anexo1_ADC
    {
        public V_Anexo1 anexo1 { get; set; }
        public V_ADC adc { get; set; }
        
    }
    public struct Anexo2_Registro
    {
        public string NoFolio { get; set; }
        public string tipoCambio1 { get; set; }
        public string tipoCambio2 { get; set; }
        public string lugar { get; set; }
        public string descripcion { get; set; }
        public string estatus { get; set; }
        public string fechaRegistro { get; set; }
        public string fechaCierre { get; set; }
        public string responsable { get; set; }
        public string lider { get; set; }
        public string estatusADC { get; set; }
        public string presentoARP { get; set; }
    }
    public class V_Reporte_Anexo2_ADC
    {
        public List<Anexo2_Registro> registros { get; set; }

    }
    public class V_Reporte_Anexo3_ADC
    {
        public ADC_Anexo3 anexo3 { get; set; }
        public V_Anexo1 anexo1 { get; set; }
        public V_ADC adc { get; set; }
        public Proyectos proyecto { get; set; }
        public ADC_Equipo_Verificador EV { get; set; }
        public ADC_Equipo_Verificador_Integrantes integrantesEV { get; set; }
        public List<Usuarios> usuarios { get; set; }
        public List<ADC_Anexo3_Documentacion> documentacion { get; set; }

    }
    public class V_Reporte_Anexo4_ADC
    {
        public ADC_Anexo4 anexo4 { get; set; }
        public V_Anexo1 anexo1 { get; set; }
        public V_ADC adc { get; set; }
        public ADC_Anexo3 anexo3 { get; set; }
        public string responsable { get; set; }
        public string lider { get; set; }
        public string deo { get; set; }
        public string dems { get; set; }
        public string residente { get; set; }
        public List<Usuarios> ev { get; set; }
    }
    public class V_Reporte_Anexo5_ADC
    {

    }
    public class V_Reporte_Anexo6_ADC
    {

    }
}
