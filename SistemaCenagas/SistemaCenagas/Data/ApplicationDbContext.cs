using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SistemaCenagas.Models;

namespace SistemaCenagas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region TABLAS INICIALES
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Puestos> Puestos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Residencias> Residencias { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Proyecto_Miembros> Proyecto_Miembros { get; set; }

        #endregion

        

        #region CATALOGOS
        public DbSet<Instalaciones> Instalaciones { get; set; }
        public DbSet<Gasoductos> Gasoductos { get; set; }
        public DbSet<Tramos> Tramos { get; set; }
        public DbSet<ADC_Anexos> ADC_Anexos { get; set; }
        public DbSet<ElementoS3S> ElementoS3S { get; set; }
        public DbSet<Fuente_Deteccion> Fuente_Deteccion { get; set; }
        public DbSet<Etapa_Realizada> Etapa_Realizada { get; set; }
        public DbSet<Zonas> Zonas { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<Sistema> Sistema { get; set; }
        public DbSet<DDV> DDV { get; set; }
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<Direccion_Ejecutiva> Direccion_Ejecutiva { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Tipo> Tipo { get; set; }

        #endregion

        #region ADMINISTRACION DE CAMBIO
        public DbSet<ADC> ADC { get; set; }
        public DbSet<ADC_Actividades> ADC_Actividades { get; set; }
        public DbSet<ADC_Procesos> ADC_Procesos { get; set; }
        public DbSet<ADC_Archivos> ADC_Archivos { get; set; }
        public DbSet<ADC_Normativas> ADC_Normativas { get; set; }
        public DbSet<ADC_Equipo_Verificador> ADC_Equipo_Verificador { get; set; }
        public DbSet<ADC_Equipo_Verificador_Integrantes> ADC_Equipo_Verificador_Integrantes { get; set; }

        #endregion

        #region PRE ARRANQUE
        public DbSet<PreArranque> PreArranque { get; set; }
        public DbSet<PreArranque_Actividades> PreArranque_Actividades { get; set; }
        public DbSet<PreArranque_Procesos> PreArranque_Procesos { get; set; }
        public DbSet<PreArranque_Archivos> PreArranque_Archivos { get; set; }
        public DbSet<PreArranque_Normativas> PreArranque_Normativas { get; set; }
        public DbSet<PreArranque_Equipo_Verificador> PreArranque_Equipo_Verificador { get; set; }
        public DbSet<PreArranque_Equipo_Verificador_Integrantes> PreArranque_Equipo_Verificador_Integrantes { get; set; }

        #endregion

        #region ANEXOS
        public DbSet<ADC_Anexo2> ADC_Anexo2 { get; set; }
        public DbSet<ADC_Anexo1> ADC_Anexo1 { get; set; }
        public DbSet<ADC_Anexo3> ADC_Anexo3 { get; set; }
        public DbSet<ADC_Anexo3_CatalogoTipoDocumentacion> ADC_Anexo3_CatalogoTipoDocumentacion { get; set; }
        public DbSet<ADC_Anexo3_Documentacion> ADC_Anexo3_Documentacion { get; set; }
        public DbSet<ADC_Anexo3_DocumentacionResponsable> ADC_Anexo3_DocumentacionResponsable { get; set; }
        public DbSet<ADC_Anexo4> ADC_Anexo4 { get; set; }
        public DbSet<ADC_Anexo5> ADC_Anexo5 { get; set; }
        public DbSet<ADC_Anexo6> ADC_Anexo6 { get; set; }
        public DbSet<ADC_Anexo6_Documentacion> ADC_Anexo6_Documentacion { get; set; }


        public DbSet<PreArranque_Anexo2> PreArranque_Anexo2 { get; set; }
        public DbSet<PreArranque_Anexo2_Seccion2> PreArranque_Anexo2_Seccion2 { get; set; }
        public DbSet<PreArranque_Anexo2_Seccion2_ElementosRevision> PreArranque_Anexo2_Seccion2_ElementosRevision { get; set; }
        public DbSet<PreArranque_Anexo2_Seccion3> PreArranque_Anexo2_Seccion3 { get; set; }


        public DbSet<PreArranque_Anexo1> PreArranque_Anexo1 { get; set; }
        public DbSet<PreArranque_Anexo1_Actividades> PreArranque_Anexo1_Actividades { get; set; }
        public DbSet<PreArranque_Anexo1_Actividades_Acciones> PreArranque_Anexo1_Actividades_Acciones { get; set; }


        #endregion

        

    }
}
