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
        public static IEnumerable<ADC_Actividades> VistaActividadesADC(ApplicationDbContext context)
        {
            return context.ADC_Actividades.Where(a => a.Registro_Eliminado == 0).ToList();
        }
        public static IEnumerable<Global.V_Normativas> VistaNormativasADC(ApplicationDbContext context)
        {
            return (from n in context.ADC_Normativas
                    join a in context.ADC_Actividades on n.Id_Actividad equals a.Id_Actividad
                    join an in context.Anexos on n.Id_Anexo equals an.Id_Anexo
                    where n.Registro_Eliminado == 0 && 
                          n.Id_Actividad == Global.actividadADC.Id_Actividad
                    select new Global.V_Normativas
                    {
                        adc_normativas = n,
                        actividad = a.Actividad,
                        anexo = an.Nombre
                    }).ToList();
        }
    }
}
