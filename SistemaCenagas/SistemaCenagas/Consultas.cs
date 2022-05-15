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
                                                 where u.Id_Rol != 5
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
                    join a1 in context.Anexo1 on a.Id_ADC equals a1.Id_PropuestaCambio
                    join res in context.Residencias on a1.Id_Residencia equals res.Id_Residencia
                    join p in context.Proyectos on a.Id_Proyecto equals p.Id_Proyecto
                    join pc in context.Usuarios on a.Id_ProponenteCambio equals pc.Id_Usuario
                    join l in context.Usuarios on a.Id_Lider equals l.Id_Usuario
                    join r in context.Usuarios on a.Id_ResponsableADC equals r.Id_Usuario
                    join s in context.Usuarios on a.Id_Suplente equals s.Id_Usuario
                    where a.Registro_Eliminado == 0
                    select new Global.V_ADC
                    {
                        adc = a,
                        anexo1 = a1,
                        residencia = res.Nombre,
                        id_proyecto = p.Id_Proyecto,
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
                    join i in context.Instalaciones on t.Ut_Tramo equals i.Ut_Tramo
                    where a.Registro_Eliminado == 0 && a.Id_PropuestaCambio == id_adc && 
                          i.Ut_Tramo.Equals(a.Ut_Tramo) && i.Residencia.Equals(t.Residencia)
                    select new Global.V_Anexo1
                    {
                        anexo1 = a,
                        proyecto = p.Nombre,
                        residencia = r.Nombre,
                        instalacion = i.Instalacion,
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
                        nombre_miembro = u.Nombre + " " + u.Paterno,
                        email = u.Email
                    }).ToList();
        }

        public static IEnumerable<Global.V_EquipoVerificadorADC> VistaEquipoVerificadorADC(ApplicationDbContext context, int idADC)
        {
            return (from ev_ in context.ADC_EquipoVerificador
                    join u in context.Usuarios on ev_.Id_Usuario equals u.Id_Usuario
                    where ev_.Id_ADC == idADC
                    select new Global.V_EquipoVerificadorADC
                    {
                        ev = ev_,
                        nombre_miembro = u.Nombre + " " + u.Paterno,
                        email = u.Email
                    }).ToList();
        }

        public static IEnumerable<Global.V_Resumen> VistaResumenADC(ApplicationDbContext context)
        {
            IEnumerable<Global.V_Resumen> resumen = (from a1 in context.Anexo1
                                                     join adc in context.ADC on a1.Id_PropuestaCambio equals adc.Id_ADC
                                                     join r in context.Residencias on a1.Id_Residencia equals r.Id_Residencia
                                                     join p in context.Proyectos on a1.Id_Proyecto equals p.Id_Proyecto
                                                     join proc in context.ADC_Procesos on adc.Id_ADC equals proc.Id_ADC
                                                     where adc.Registro_Eliminado == 0
                                                     
                                                     select new Global.V_Resumen
                                                     {
                                                         id_adc = adc.Id_ADC,
                                                         residencia = r.Nombre,
                                                         id_proyecto = p.Id_Proyecto,
                                                         proyecto = p.Nombre,
                                                         avance_ADC = proc.Avance
                                                     }).ToList();


            return resumen
                .GroupBy(r => r.id_adc)
                .Select(s => new Global.V_Resumen
                {
                    id_adc = s.First().id_adc,
                    residencia = s.First().residencia,
                    id_proyecto = s.First().id_proyecto,
                    proyecto = s.First().proyecto,
                    avance_ADC = s.Sum(a => a.avance_ADC)/ s.Count(),
                    avance_Pre = 0,
                    avance_Fisico = 0
                }).ToList();
        }

        public static IEnumerable<Global.V_Archivos> VistaArchivosADC(ApplicationDbContext context, int Id_ADC)
        {
            return (from a in context.ADC_Archivos
                    join u in context.Usuarios on a.Id_Usuario equals u.Id_Usuario
                    select new Global.V_Archivos
                    {
                        archivo = a,
                        usuario = $"{u.Nombre} {u.Paterno}"
                    }).ToList();
                
                //context.ADC_Archivos.Where(a => a.Registro_Eliminado == 0 && a.Id_ADC == Id_ADC).ToList();
        }        
        public static IEnumerable<Gasoductos> getGasoductos(ApplicationDbContext context, string residencia)
        {
            return (from g in context.Gasoductos
                    join t in context.Tramos on g.Ut_Gasoducto equals t.Ut_Gasoducto
                    where t.Residencia.Equals(residencia)
                    select new Gasoductos { 
                        Ut_Gasoducto = g.Ut_Gasoducto,
                        Gasoducto = g.Gasoducto
                    }).Distinct().ToList();
        }
        public static IEnumerable<Tramos> getTramos(ApplicationDbContext context, string ut_gasoducto)
        {
            return (from g in context.Gasoductos
                    join t in context.Tramos on g.Ut_Gasoducto equals t.Ut_Gasoducto
                    where t.Ut_Gasoducto.Equals(ut_gasoducto)
                    select new Tramos { 
                        Ut_Tramo = t.Ut_Tramo,
                        Tramo = t.Tramo
                    }).Distinct().ToList();
        }

        


    }
}
