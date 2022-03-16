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

        //-----------CATALOGOS--------
        public static IEnumerable<Roles> roles { get; set; }
        public static IEnumerable<Anexos> anexos { get; set; }
        

    }
}
