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
    public class ProyectosLiderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosLiderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProyectosLider
        public async Task<IActionResult> Index()
        {
            Global.vistaProyectos = (from p in _context.Proyectos
                                     join u in _context.Usuarios on p.Id_Lider equals u.Id_Usuario
                                     where p.Id_Lider == Global.usuario.Id_Usuario
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

        // GET: ProyectosLider/Edit/5
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

        // POST: ProyectosLider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Proyecto,Clave,Nombre,Descripcion,Id_Lider")] Proyectos proyectos)
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

        // GET: ProyectosLider/Delete/5
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

        // POST: ProyectosLider/Delete/5
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
