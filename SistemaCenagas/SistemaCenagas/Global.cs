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

        //-----------CATALOGOS--------
        public static IEnumerable<Roles> roles { get; set; }
        

    }
}
