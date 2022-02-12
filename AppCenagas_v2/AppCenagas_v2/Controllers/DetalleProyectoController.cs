using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCenagas_v2.Data;
using AppCenagas_v2.Models;

namespace AppCenagas_v2.Controllers
{
    public class DetalleProyectoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleProyectoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleProyecto
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetalleProyecto.ToListAsync());
        }

        // GET: DetalleProyecto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyecto
                .FirstOrDefaultAsync(m => m.IdDetalleProyecto == id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }

            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalleProyecto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleProyecto,IdProyecto,IdResidencia,IdAsignacion,NoDesarrollo,Desarrollo,DescripcionActividad,Avance,FaltanteComentarios,Comentarios,PlanAccion,Anexos,TipoProyecto")] DetalleProyecto detalleProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyecto.FindAsync(id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }
            return View(detalleProyecto);
        }

        // POST: DetalleProyecto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleProyecto,IdProyecto,IdResidencia,IdAsignacion,NoDesarrollo,Desarrollo,DescripcionActividad,Avance,FaltanteComentarios,Comentarios,PlanAccion,Anexos,TipoProyecto")] DetalleProyecto detalleProyecto)
        {
            if (id != detalleProyecto.IdDetalleProyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleProyectoExists(detalleProyecto.IdDetalleProyecto))
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
            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyecto
                .FirstOrDefaultAsync(m => m.IdDetalleProyecto == id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }

            return View(detalleProyecto);
        }

        // POST: DetalleProyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleProyecto = await _context.DetalleProyecto.FindAsync(id);
            _context.DetalleProyecto.Remove(detalleProyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleProyectoExists(int id)
        {
            return _context.DetalleProyecto.Any(e => e.IdDetalleProyecto == id);
        }
    }
}
