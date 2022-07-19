using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

        public static IEnumerable<V_Usuarios> VistaUsuarios(ApplicationDbContext context)
        {
            IEnumerable<V_Usuarios> vu = (from u in context.Usuarios
                                                 join r in context.Roles on u.Id_Rol equals r.Id
                                                 where u.Id_Rol > 1
                                                 select new V_Usuarios
                                                 {
                                                     user = u,
                                                     nombre_completo = u.Nombre + " " + u.Paterno + " " + u.Materno,
                                                     Rol = r.Nombre
                                                 }).ToList();
            return vu;
        }
        public static IEnumerable<Proyectos> VistaProyectos(ApplicationDbContext context)
        {
            return context.Proyectos.ToList();
        }
        public static IEnumerable<V_MiembrosProyecto> VistaMiembrosProyecto(ApplicationDbContext context, int idProyecto)
        {
            return (from m in context.Proyecto_Miembros
                    join u in context.Usuarios on m.Id_Usuario equals u.Id
                    where m.Id_Proyecto == idProyecto
                    select new V_MiembrosProyecto
                    {
                        pm = m,
                        nombre_miembro = u.Nombre + " " + u.Paterno,
                        email = u.Email
                    }).ToList();
        }
        public static IEnumerable<Gasoductos> getGasoductos(ApplicationDbContext context, string residencia)
        {
            return (from g in context.Gasoductos
                    join t in context.Tramos on g.Ut_Gasoducto equals t.Ut_Gasoducto
                    where t.Residencia.Equals(residencia)
                    select new Gasoductos
                    {
                        Ut_Gasoducto = g.Ut_Gasoducto,
                        Gasoducto = g.Gasoducto
                    }).Distinct().ToList();
        }
        public static IEnumerable<Tramos> getTramos(ApplicationDbContext context, string ut_gasoducto)
        {
            return (from g in context.Gasoductos
                    join t in context.Tramos on g.Ut_Gasoducto equals t.Ut_Gasoducto
                    where t.Ut_Gasoducto.Equals(ut_gasoducto)
                    select new Tramos
                    {
                        Ut_Tramo = t.Ut_Tramo,
                        Tramo = t.Tramo
                    }).Distinct().ToList();
        }


        #region ADC Vistas
        public static IEnumerable<ADC_Actividades> VistaActividadesADC(ApplicationDbContext context)
        {
            return context.ADC_Actividades.Where(a => a.Eliminado == 0).ToList();
        }
        public static IEnumerable<V_Normativas> VistaNormativasADC(ApplicationDbContext context, Global global)
        {
            
            return (from n in context.ADC_Normativas
                    join a in context.ADC_Actividades on n.Id_Actividad equals a.Id
                    //join an in context.ADC_Anexos on n.Id equals an.Id
                    where n.Eliminado == 0 &&
                          n.Id_Actividad == global.actividadADC.Id
                    select new V_Normativas
                    {
                        adc_normativas = n,
                        actividad = a.Actividad,
                        anexo = n.Registro
                    }).ToList();
        }
        public static IEnumerable<V_ADC> VistaADC(ApplicationDbContext context)
        {
            return (from a in context.ADC
                    join a1 in context.ADC_Anexo1 on a.Id equals a1.Id
                    join res in context.Residencias on a1.Id_Residencia equals res.Id
                    join p in context.Proyectos on a.Id_Proyecto equals p.Id
                    join pc in context.Usuarios on a.Id_ProponenteCambio equals pc.Id
                    join l in context.Usuarios on a.Id_LiderEquipoVerificador equals l.Id
                    join r in context.Usuarios on a.Id_ResponsableADC equals r.Id
                    join s in context.Usuarios on a.Id_Suplente equals s.Id
                    where a.Eliminado == 0
                    select new V_ADC
                    {
                        adc = a,
                        anexo1 = a1,
                        residencia = res.Nombre,
                        id_proyecto = p.Id,
                        proyecto = p.Nombre,
                        proponente = pc.Titulo + " " + pc.Nombre + " " + pc.Paterno + " " + pc.Materno,
                        lider = l.Titulo + " " + l.Nombre + " " + l.Paterno + " " + l.Materno,
                        responsable = r.Titulo + " " + r.Nombre + " " + r.Paterno + " " + r.Materno,
                        suplente = s.Titulo + " " + s.Nombre + " " + s.Paterno + " " + s.Materno
                    }).ToList();
        }
        public static IEnumerable<V_ADC> VistaADC_EV(ApplicationDbContext context)
        {
            var _vista_adc = Consultas.VistaADC(context);
            return (from v in _vista_adc
                    join ev in context.ADC_Equipo_Verificador on v.adc.Id equals ev.Id_ADC
                    //join evi in context.ADC_Equipo_Verificador_Integrantes on ev.Id equals evi.Id_Equipo_Verificador_ADC
                    select v
                    ).ToList();

        }
        public static V_Anexo1 VistaAnexo1(ApplicationDbContext context, int? id_adc)
        {
            return (from a in context.ADC_Anexo1
                    join p in context.Proyectos on a.Id_Proyecto equals p.Id
                    join r in context.Residencias on a.Id_Residencia equals r.Id
                    join g in context.Gasoductos on a.Ut_Gasoducto equals g.Ut_Gasoducto
                    join t in context.Tramos on a.Ut_Tramo equals t.Ut_Tramo
                    join i in context.Instalaciones on t.Ut_Tramo equals i.Ut_Tramo
                    where a.Registro_Eliminado == 0 && a.Id == id_adc &&
                          i.Ut_Tramo.Equals(a.Ut_Tramo) && i.Residencia.Equals(t.Residencia)
                    select new V_Anexo1
                    {
                        anexo1 = a,
                        proyecto = p.Nombre,
                        residencia = r.Nombre,
                        instalacion = i.Instalacion,
                        gasoducto = g.Gasoducto,
                        tramo = t.Tramo

                    }).FirstOrDefault();
        }

        

        public static IEnumerable<V_Tareas> VistaTareas(ApplicationDbContext context)
        {
            return (from t in context.ADC_Procesos
                    join a in context.ADC_Actividades on t.Id_Actividad equals a.Id
                    where t.Eliminado == 0// && a.Id_Actividad == 1
                    select new V_Tareas
                    {
                        proceso = t,
                        actividad = a
                    }).ToList();
        }
        public static IEnumerable<V_ADC_ResponsablesDocumentacionAnexo3> VistaADCResponsablesDocumentacionAnexo3(ApplicationDbContext context, int idAnexo3)
        {
            return (from docr in context.ADC_Anexo3_DocumentacionResponsable
                    join u in context.Usuarios on docr.Id_Responsable equals u.Id
                    join p in context.Puestos on u.Id_Puesto equals p.Id
                    where docr.Estatus.Equals("Agregado") && docr.Id_Anexo3 == idAnexo3
                    orderby docr.Id_Documentacion
                    select new V_ADC_ResponsablesDocumentacionAnexo3
                    {
                        responsable = docr,
                        nombre = $"{u.Nombre} {u.Paterno} {u.Materno}",
                        puesto = p.Nombre,
                        email = u.Email
                    }).ToList();
        }
        public static IEnumerable<V_EquipoVerificador> VistaEquipoVerificador(ApplicationDbContext context, int idEV)
        {
            return (from integrantes in context.ADC_Equipo_Verificador_Integrantes
                    join equipo in context.ADC_Equipo_Verificador on integrantes.Id_Equipo_Verificador_ADC equals equipo.Id
                    join usuarios in context.Usuarios on integrantes.Id_Usuario equals usuarios.Id
                    join puestos in context.Puestos on usuarios.Id_Puesto equals puestos.Id
                    where integrantes.Id_Equipo_Verificador_ADC == idEV
                    select new V_EquipoVerificador
                    {
                        integrante = integrantes,
                        nombre = $"{usuarios.Nombre} {usuarios.Paterno}",
                        puesto = puestos.Nombre,
                        email = usuarios.Email
                    }).ToList();
        }
        public static IEnumerable<V_Resumen> VistaResumenADC(ApplicationDbContext context)
        {
            IEnumerable<V_Resumen> resumen = (from a1 in context.ADC_Anexo1
                                                     join adc in context.ADC on a1.Id equals adc.Id
                                                     join r in context.Residencias on a1.Id_Residencia equals r.Id
                                                     join p in context.Proyectos on a1.Id_Proyecto equals p.Id
                                                     join proc in context.ADC_Procesos on adc.Id equals proc.Id_ADC
                                                     where adc.Eliminado == 0 //&& proc.Id_Actividad == 1

                                                     select new V_Resumen
                                                     {
                                                         id_adc = adc.Id,
                                                         folio_adc = adc.Folio,
                                                         id_residencia = r.Id,
                                                         residencia = r.Nombre,
                                                         id_proyecto = p.Id,
                                                         proyecto = p.Nombre,
                                                         avance_ADC = proc.Avance
                                                     }).ToList();


            return resumen
                .GroupBy(r => r.id_adc)
                .Select(s => new V_Resumen
                {
                    id_adc = s.First().id_adc,
                    folio_adc = s.First().folio_adc,
                    id_residencia = s.First().id_residencia,
                    residencia = s.First().residencia,
                    id_proyecto = s.First().id_proyecto,
                    proyecto = s.First().proyecto,
                    avance_ADC = (int)(s.Sum(a => a.avance_ADC) / s.Count()),
                    avance_Pre = 0,
                    avance_Fisico = 0
                }).ToList();
        }
        public static IEnumerable<V_Archivos> VistaArchivosADC(ApplicationDbContext context, int Id_Proceso)
        {
            return (from a in context.ADC_Archivos
                    join u in context.Usuarios on a.Id_Usuario equals u.Id
                    where a.Id_Proceso == Id_Proceso
                    select new V_Archivos
                    {
                        archivo = a,
                        usuario = $"{u.Nombre} {u.Paterno}"
                    }).ToList();

            //context.ADC_Archivos.Where(a => a.Registro_Eliminado == 0 && a.Id_ADC == Id_ADC).ToList();
        }

        #endregion

        #region PreArranque Vistas

        public static IEnumerable<PreArranque_Actividades> PreArranqueVistaActividades(ApplicationDbContext context)
        {
            return context.PreArranque_Actividades.Where(a => a.Eliminado == 0).ToList();
        }
        public static IEnumerable<V_Normativas_PreArranque> PreArranqueVistaNormativas(ApplicationDbContext context, Global global)
        {
            return (from n in context.PreArranque_Normativas
                    join a in context.PreArranque_Actividades on n.Id_Actividad equals a.Id
                    //join an in context.ADC_Anexos on n.Id equals an.Id
                    where n.Eliminado == 0 &&
                          n.Id_Actividad == global.actividadPreArranque.Id
                    select new V_Normativas_PreArranque
                    {
                        prearranque_normativas = n,
                        actividad = a.Actividad,
                        registro = n.Registro
                    }).ToList();
        }
        public static IEnumerable<V_PreArranque> PreArranqueVista(ApplicationDbContext context)
        {
            return (from a in context.PreArranque
                    join a2 in context.PreArranque_Anexo2 on a.Id equals a2.Id_Prearranque
                    join res in context.Residencias on a2.Id_Residencia equals res.Id
                    join p in context.Proyectos on a.Id_Proyecto equals p.Id
                    join l in context.Usuarios on a.Id_LiderEquipoVerificador equals l.Id
                    join r in context.Usuarios on a.Id_Responsable equals r.Id
                    join s in context.Usuarios on a.Id_Suplente equals s.Id
                    where a.Eliminado == 0
                    select new V_PreArranque
                    {
                        prearranque = a,
                        anexo2 = a2,
                        residencia = res.Nombre,
                        id_proyecto = p.Id,
                        proyecto = p.Nombre,
                        lider = l.Titulo + " " + l.Nombre + " " + l.Paterno + " " + l.Materno,
                        responsable = r.Titulo + " " + r.Nombre + " " + r.Paterno + " " + r.Materno,
                        suplente = s.Titulo + " " + s.Nombre + " " + s.Paterno + " " + s.Materno
                    }).ToList();
        }
        public static IEnumerable<V_PreArranque> PreArranqueVista_EV(ApplicationDbContext context)
        {
            var _vista_prearranque = Consultas.PreArranqueVista(context);
            return (from v in _vista_prearranque
                    join ev in context.PreArranque_Equipo_Verificador on v.prearranque.Id equals ev.Id_PreArranque
                    //join evi in context.ADC_Equipo_Verificador_Integrantes on ev.Id equals evi.Id_Equipo_Verificador_ADC
                    select v
                    ).ToList();

        }
        public static V_Anexo2_PreArranque PreArranqueVistaAnexo2(ApplicationDbContext context, int? id_adc)
        {
            return (from a in context.PreArranque_Anexo2
                    join r in context.Residencias on a.Id_Residencia equals r.Id
                    join g in context.Gasoductos on a.Ut_Gasoducto equals g.Ut_Gasoducto
                    join t in context.Tramos on a.Ut_Tramo equals t.Ut_Tramo
                    join i in context.Instalaciones on t.Ut_Tramo equals i.Ut_Tramo
                    join pre in context.PreArranque on a.Id_Prearranque equals pre.Id
                    join p in context.Proyectos on pre.Id_Proyecto equals p.Id
                    where a.Id_Prearranque == id_adc &&
                          i.Ut_Tramo.Equals(a.Ut_Tramo) && i.Residencia.Equals(t.Residencia)
                    select new V_Anexo2_PreArranque
                    {
                        anexo2 = a,
                        proyecto = p.Nombre,
                        residencia = r.Nombre,
                        instalacion = i.Instalacion,
                        gasoducto = g.Gasoducto,
                        tramo = t.Tramo

                    }).FirstOrDefault();
        }
        public static IEnumerable<V_Tareas_PreArranque> PreArranqueVistaTareas(ApplicationDbContext context)
        {
            return (from t in context.PreArranque_Procesos
                    join a in context.PreArranque_Actividades on t.Id_Actividad equals a.Id
                    where t.Eliminado == 0// && a.Id_Actividad == 1
                    select new V_Tareas_PreArranque
                    {
                        proceso = t,
                        actividad = a
                    }).ToList();
        }
        public static IEnumerable<V_EquipoVerificador_PreArranque> PreArranqueVistaEquipoVerificador(ApplicationDbContext context, int idEV)
        {
            return (from integrantes in context.PreArranque_Equipo_Verificador_Integrantes
                    join equipo in context.PreArranque_Equipo_Verificador on integrantes.Id_Equipo_Verificador_PreArranque equals equipo.Id
                    join usuarios in context.Usuarios on integrantes.Id_Usuario equals usuarios.Id
                    where integrantes.Id_Equipo_Verificador_PreArranque == idEV
                    select new V_EquipoVerificador_PreArranque
                    {
                        integrante = integrantes,
                        nombre = $"{usuarios.Nombre} {usuarios.Paterno}",
                        email = usuarios.Email
                    }).ToList();
        }
        public static IEnumerable<V_Archivos_PreArranque> PreArranqueVistaArchivos(ApplicationDbContext context, int Id_Proceso)
        {
            return (from a in context.PreArranque_Archivos
                    join u in context.Usuarios on a.Id_Usuario equals u.Id
                    where a.Id_Proceso == Id_Proceso
                    select new V_Archivos_PreArranque
                    {
                        archivo = a,
                        usuario = $"{u.Nombre} {u.Paterno}"
                    }).ToList();

            //context.ADC_Archivos.Where(a => a.Registro_Eliminado == 0 && a.Id_ADC == Id_ADC).ToList();
        }

        #endregion        
    }
}
