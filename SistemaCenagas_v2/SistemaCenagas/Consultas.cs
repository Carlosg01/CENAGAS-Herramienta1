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
                                                     nombre_completo = u.Nombre + " " + u.Paterno + " " + u.Materno,
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
        public static IEnumerable<Proyectos> VistaProyectos(ApplicationDbContext context)
        {
            return context.Proyectos.ToList();
        }
        public static IEnumerable<Global.V_ADC> VistaADC(ApplicationDbContext context)
        {
            return (from a in context.ADC
                    join p in context.Proyectos on a.Id_Proyecto equals p.Id_Proyecto
                    join pc in context.Usuarios on a.Id_ProponenteCambio equals pc.Id_Usuario
                    join l in context.Usuarios on a.Id_Lider equals l.Id_Usuario
                    join r in context.Usuarios on a.Id_ResponsableADC equals r.Id_Usuario
                    join s in context.Usuarios on a.Id_Suplente equals s.Id_Usuario
                    where a.Registro_Eliminado == 0
                    select new Global.V_ADC
                    {
                        adc = a,
                        proyecto = p.Nombre,
                        proponente = pc.Titulo + " " + pc.Nombre + " " + pc.Paterno + " " + pc.Materno,
                        lider = l.Titulo + " " + l.Nombre + " " + l.Paterno + " " + l.Materno,
                        responsable = r.Titulo + " " + r.Nombre + " " + r.Paterno + " " + r.Materno,
                        suplente = s.Titulo + " " + s.Nombre + " " + s.Paterno + " " + s.Materno
                    }).ToList();
        }
        public static Global.V_Anexo1 VistaAnexo1(ApplicationDbContext context, int? id_adc)
        {
            return (from a in context.Anexo1
                    join p in context.Proyectos on a.Id_Proyecto equals p.Id_Proyecto
                    join r in context.Residencias on a.Id_Residencia equals r.Id_Residencia
                    join g in context.Gasoductos on a.Ut_Gasoducto equals g.Ut_Gasoducto
                    join t in context.Tramos on a.Ut_Tramo equals t.Ut_Tramo
                    where a.Registro_Eliminado == 0 && a.Id_PropuestaCambio == id_adc
                    select new Global.V_Anexo1
                    {
                        anexo1 = a,
                        proyecto = p.Nombre,
                        residencia = r.Nombre,
                        gasoducto = g.Gasoducto,
                        tramo = t.Tramo

                    }).FirstOrDefault();
        }
        public static IEnumerable<Global.V_Tareas> VistaTareas(ApplicationDbContext context)
        {
            return (from t in context.ADC_Procesos
                    join a in context.ADC_Actividades on t.Id_Actividad equals a.Id_Actividad
                    where t.Registro_Eliminado == 0
                    select new Global.V_Tareas
                    {
                        proceso = t,
                        actividad = a.Actividad
                    }).ToList();
        }

        public static IEnumerable<Global.V_MiembrosProyecto> VistaMiembrosProyecto(ApplicationDbContext context, int idProyecto)
        {
            return (from m in context.Proyecto_Miembros
                    join u in context.Usuarios on m.Id_Usuario equals u.Id_Usuario
                    where m.Id_Proyecto == idProyecto
                    select new Global.V_MiembrosProyecto
                    {
                        pm = m,
                        nombre_miembro = u.Nombre + " " + u.Paterno
                    }).ToList();
        }


    }
}
