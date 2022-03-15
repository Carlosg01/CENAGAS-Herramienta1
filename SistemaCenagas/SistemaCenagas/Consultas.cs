using SistemaCenagas.Data;
using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public static class Consultas
    {
        //------CATALOGOS-----

        public static IEnumerable<Global.V_Usuarios> VistaUsuarios(ApplicationDbContext context)
        {
            IEnumerable<Global.V_Usuarios> vu = (from u in context.Usuarios
                                                 join r in context.Roles on u.Id_Rol equals r.Id_Rol
                                                 select new Global.V_Usuarios
                                                 {
                                                     user = u,
                                                     Rol = r.Nombre
                                                 }).ToList();
            return vu;

        }
    }
}
