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
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Residencias> Residencias { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Proyecto_Miembros> Proyecto_Miembros { get; set; }

        #endregion

        

        #region CATALOGOS
        public DbSet<Instalaciones> Instalaciones { get; set; }
        public DbSet<Gasoductos> Gasoductos { get; set; }
        public DbSet<Tramos> Tramos { get; set; }
        public DbSet<Anexos> Anexos { get; set; }

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

        #region ANEXOS
        public DbSet<Anexo2> Anexo2 { get; set; }
        public DbSet<Anexo1> Anexo1 { get; set; }
        public DbSet<Anexo3> Anexo3 { get; set; }
        public DbSet<Anexo3_CatalogoTipoDocumentacion> Anexo3_CatalogoTipoDocumentacion { get; set; }
        public DbSet<Anexo3_Documentacion> Anexo3_Documentacion { get; set; }
        public DbSet<Anexo4> Anexo4 { get; set; }
        public DbSet<Anexo5> Anexo5 { get; set; }
        public DbSet<Anexo6> Anexo6 { get; set; }
        public DbSet<Anexo6_Documentacion> Anexo6_Documentacion { get; set; }

        #endregion

        

    }
}
