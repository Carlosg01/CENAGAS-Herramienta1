using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class ProyectosADCController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosADCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            Global.vistaProyectos = (from p in _context.Proyectos
                                     join u in _context.Usuarios on p.Id_Lider equals u.Id_Usuario
                                     where p.ADC.Equals("Activado")
                                     select new Global.VistaProyectos
                                     {
                                         id_proyecto = p.Id_Proyecto,
                                         clave = p.Clave,
                                         nombre = p.Nombre,
                                         descripcion = p.Descripcion,
                                         lider = u.Titulo + " " + u.Nombre + " " + u.Paterno + " " + u.Materno
                                     });
            return View();
        }

        //GET: Proyectos/PropuestaCambio
        public async Task<IActionResult> PropuestaCambio(int? id)
        {
            Global.proyecto = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.Id_Proyecto == id);
            return RedirectToAction("Index", "Anexo1_PropuestaCambio");
        }        
    }
}
