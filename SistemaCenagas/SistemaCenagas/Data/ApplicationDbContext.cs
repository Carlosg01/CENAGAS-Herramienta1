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
        public DbSet<SistemaCenagas.Models.Asignacion> Asignacion { get; set; }
        public DbSet<SistemaCenagas.Models.DetalleProyecto> DetalleProyecto { get; set; }
        public DbSet<SistemaCenagas.Models.Empleado> Empleado { get; set; }
        public DbSet<SistemaCenagas.Models.Proyectos> Proyectos { get; set; }
        public DbSet<SistemaCenagas.Models.Usuario> Usuario { get; set; }
    }
}
