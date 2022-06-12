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
    public class ADC_ActividadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADC_ActividadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ADC_Actividades
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }
            Global.vista_actividadesADC = Consultas.VistaActividadesADC(_context);
            return View();
        }

        public async Task<IActionResult> Normativas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.actividadADC = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Global.actividadADC == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "ADC_Normativas");
        }

        // GET: ADC_Actividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Actividades = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Actividades == null)
            {
                return NotFound();
            }

            return View(aDC_Actividades);
        }

        // GET: ADC_Actividades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ADC_Actividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Actividad,Actividad,Registro_Eliminado")] ADC_Actividades aDC_Actividades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Actividades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aDC_Actividades);
        }

        // GET: ADC_Actividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Actividades = await _context.ADC_Actividades.FindAsync(id);
            if (aDC_Actividades == null)
            {
                return NotFound();
            }
            return PartialView(aDC_Actividades);
        }

        // POST: ADC_Actividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Actividad,Actividad,Registro_Eliminado")] ADC_Actividades aDC_Actividades)
        {
            if (id != aDC_Actividades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aDC_Actividades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADC_ActividadesExists(aDC_Actividades.Id))
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
            return PartialView(aDC_Actividades);
        }

        // GET: ADC_Actividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Actividades = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Actividades == null)
            {
                return NotFound();
            }

            return View(aDC_Actividades);
        }

        // POST: ADC_Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aDC_Actividades = await _context.ADC_Actividades.FindAsync(id);
            aDC_Actividades.Eliminado = 1;
            _context.ADC_Actividades.Update(aDC_Actividades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_ActividadesExists(int id)
        {
            return _context.ADC_Actividades.Any(e => e.Id == id);
        }
    }
}
