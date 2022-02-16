using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCenagas_v2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppCenagas_v2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Asignacion> Asignacion { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<DetalleProyecto> DetalleProyecto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
