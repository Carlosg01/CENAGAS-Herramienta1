using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SistemaCenagas.Models;

namespace SistemaCenagas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //TABLAS
        public DbSet<SistemaCenagas.Models.Usuarios> Usuarios { get; set; }
        public DbSet<SistemaCenagas.Models.Proyectos> Proyectos { get; set; }
        public DbSet<SistemaCenagas.Models.ADC> ADC { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Actividades> ADC_Actividades { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Procesos> ADC_Procesos { get; set; }
        public DbSet<SistemaCenagas.Models.ADC_Normativas> ADC_Normativas { get; set; }
        public DbSet<SistemaCenagas.Models.Anexos> Anexos { get; set; }


        //CATALOGOS
        public DbSet<SistemaCenagas.Models.Instalaciones> Instalaciones { get; set; }
        public DbSet<SistemaCenagas.Models.Gasoductos> Gasoductos { get; set; }
        public DbSet<SistemaCenagas.Models.Tramos> Tramos { get; set; }
        public DbSet<SistemaCenagas.Models.Residencias> Residencias { get; set; }
        public DbSet<SistemaCenagas.Models.Anexo1_PropuestaCambio> Anexo1_PropuestaCambio { get; set; }
        
        

    }
}
