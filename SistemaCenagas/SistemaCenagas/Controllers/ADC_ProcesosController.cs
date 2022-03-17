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
    public class ADC_ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADC_ProcesosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ADC_Procesos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ADC_Procesos.ToListAsync());
        }

        // GET: ADC_Procesos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos
                .FirstOrDefaultAsync(m => m.Id_Proceso == id);
            if (aDC_Procesos == null)
            {
                return NotFound();
            }

            return View(aDC_Procesos);
        }

        // GET: ADC_Procesos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ADC_Procesos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proceso,Id_Actividad,Id_ADC,Avance,Faltante_Comentarios,Plan_Accion,Registro_Eliminado")] ADC_Procesos aDC_Procesos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Procesos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aDC_Procesos);
        }

        // GET: ADC_Procesos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos.FindAsync(id);
            if (aDC_Procesos == null)
            {
                return NotFound();
            }
            return View(aDC_Procesos);
        }

        // POST: ADC_Procesos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Proceso,Id_Actividad,Id_ADC,Avance,Faltante_Comentarios,Plan_Accion,Registro_Eliminado")] ADC_Procesos aDC_Procesos)
        {
            if (id != aDC_Procesos.Id_Proceso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aDC_Procesos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADC_ProcesosExists(aDC_Procesos.Id_Proceso))
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
            return View(aDC_Procesos);
        }

        // GET: ADC_Procesos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos
                .FirstOrDefaultAsync(m => m.Id_Proceso == id);
            if (aDC_Procesos == null)
            {
                return NotFound();
            }

            return View(aDC_Procesos);
        }

        // POST: ADC_Procesos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aDC_Procesos = await _context.ADC_Procesos.FindAsync(id);
            _context.ADC_Procesos.Remove(aDC_Procesos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_ProcesosExists(int id)
        {
            return _context.ADC_Procesos.Any(e => e.Id_Proceso == id);
        }
    }
}
