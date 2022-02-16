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
    public class AsignacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsignacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Asignacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Asignacion.ToListAsync());
        }

        // GET: Asignacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignacion
                .FirstOrDefaultAsync(m => m.Id_Asignacion == id);
            if (asignacion == null)
            {
                return NotFound();
            }

            return View(asignacion);
        }

        // GET: Asignacion/Create
        public IActionResult Create()
        {
            ViewBag.idEmpleado = Global.sesionEmpleado.Id_Empleado;
            return View();
        }

        // POST: Asignacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Asignacion,Id_Empleado,Id_Proyecto,Id_Residencia,Fecha_alta,Fecha_baja,Funcion")] Asignacion asignacion)
        {
            if (ModelState.IsValid)
            {
                asignacion.Id_Empleado = Global.sesionEmpleado.Id_Empleado;
                _context.Add(asignacion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Proyectos");
            }
            return View(asignacion);
        }

        // GET: Asignacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignacion.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }
            return View(asignacion);
        }

        // POST: Asignacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Asignacion,Id_Empleado,Id_Proyecto,Id_Residencia,Fecha_alta,Fecha_baja,Funcion")] Asignacion asignacion)
        {
            if (id != asignacion.Id_Asignacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionExists(asignacion.Id_Asignacion))
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
            return View(asignacion);
        }

        // GET: Asignacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignacion
                .FirstOrDefaultAsync(m => m.Id_Asignacion == id);
            if (asignacion == null)
            {
                return NotFound();
            }

            return View(asignacion);
        }

        // POST: Asignacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacion = await _context.Asignacion.FindAsync(id);
            _context.Asignacion.Remove(asignacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionExists(int id)
        {
            return _context.Asignacion.Any(e => e.Id_Asignacion == id);
        }
    }
}
