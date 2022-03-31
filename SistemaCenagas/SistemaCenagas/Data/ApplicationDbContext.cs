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

        //TABLAS
        public DbSet<SistemaCenagas.Models.Roles> Roles { get; set; }
        public DbSet<SistemaCenagas.Models.Usuarios> Usuarios { get; set; }
        public DbSet<SistemaCenagas.Models.Proyectos> Proyectos { get; set; }
        public DbSet<SistemaCenagas.Models.ADC> ADC { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Actividades> ADC_Actividades { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Procesos> ADC_Procesos { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Archivos> ADC_Archivos { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Normativas> ADC_Normativas { get; set; }
        public DbSet<SistemaCenagas.Models.Anexo1> Anexo1 { get; set; }
        public DbSet<SistemaCenagas.Models.Proyecto_Miembros> Proyecto_Miembros { get; set; }


        //CATALOGOS
        public DbSet<SistemaCenagas.Models.Instalaciones> Instalaciones { get; set; }
        public DbSet<SistemaCenagas.Models.Gasoductos> Gasoductos { get; set; }
        public DbSet<SistemaCenagas.Models.Tramos> Tramos { get; set; }
        public DbSet<SistemaCenagas.Models.Residencias> Residencias { get; set; }
        public DbSet<SistemaCenagas.Models.Anexos> Anexos { get; set; }




    }
}
