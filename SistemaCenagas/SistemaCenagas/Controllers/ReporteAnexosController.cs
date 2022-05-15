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
    public class ReporteAnexosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReporteAnexosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProyectosUsuario
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }
            Global.vista_proyectos = Consultas.VistaProyectos(_context);
            return View();
        }

        public async Task<IActionResult> Anexos(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.proyectos = Global.vista_proyectos.Where(p => p.Id_Proyecto == id).FirstOrDefault();

            return RedirectToAction("Index", "ProyectoAnexos");
            //return RedirectToAction("Create", "Anexo1");
        }

        
    }
}
