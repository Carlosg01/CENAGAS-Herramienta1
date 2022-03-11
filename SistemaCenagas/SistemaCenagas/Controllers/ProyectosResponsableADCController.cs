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
    public class ProyectosResponsableADCController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosResponsableADCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProyectosResponsableADC
        public async Task<IActionResult> Index()
        {
            Global.vistaProyectos = (from p in _context.Proyectos
                                     join a in _context.Anexo1_PropuestaCambio on p.Id_Proyecto equals a.Id_Proyecto
                                     join u in _context.Usuarios on p.Id_Lider equals u.Id_Usuario
                                     where a.Id_ResponsableADC == Global.usuario.Id_Usuario
                                     select new Global.VistaProyectos
                                     {
                                         id_proyecto = p.Id_Proyecto,
                                         clave = p.Clave,
                                         nombre = p.Nombre,
                                         descripcion = p.Descripcion,
                                         lider = u.Titulo + " " + u.Nombre + " " + u.Paterno + " " + u.Materno
                                     }).Distinct();

            return View(await _context.Proyectos.ToListAsync());
        }

        public async Task<IActionResult> SolicitudesCambio(int? id)
        {
            Global.proyecto = await _context.Proyectos.FindAsync(id);
            return RedirectToAction("Index", "Anexo1_SolicitudesResponsableADC");
        }

        // GET: ProyectosResponsableADC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.Id_Proyecto == id);
            if (proyectos == null)
            {
                return NotFound();
            }

            return View(proyectos);
        }

        // GET: ProyectosResponsableADC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProyectosResponsableADC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyectos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyectos);
        }

        // GET: ProyectosResponsableADC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos.FindAsync(id);
            if (proyectos == null)
            {
                return NotFound();
            }
            return View(proyectos);
        }

        // POST: ProyectosResponsableADC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proyectos proyectos)
        {
            if (id != proyectos.Id_Proyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyectos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectosExists(proyectos.Id_Proyecto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proyectos);
        }

        // GET: ProyectosResponsableADC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.Id_Proyecto == id);
            if (proyectos == null)
            {
                return NotFound();
            }

            return View(proyectos);
        }

        // POST: ProyectosResponsableADC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyectos = await _context.Proyectos.FindAsync(id);
            _context.Proyectos.Remove(proyectos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectosExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id_Proyecto == id);
        }
    }
}
