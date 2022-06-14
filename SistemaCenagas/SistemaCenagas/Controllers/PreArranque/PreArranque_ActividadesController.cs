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
    public class PreArranque_ActividadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreArranque_ActividadesController(ApplicationDbContext context)
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
            Global.vista_actividadesPreArranque = Consultas.PreArranqueVistaActividades(_context);
            return View();
        }

        public async Task<IActionResult> Normativas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.actividadPreArranque = await _context.PreArranque_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Global.actividadPreArranque == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "PreArranque_Normativas");
        }

        // GET: ADC_Actividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prearranque_actividades = await _context.PreArranque_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prearranque_actividades == null)
            {
                return NotFound();
            }

            return View(prearranque_actividades);
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
        public async Task<IActionResult> Create(PreArranque_Actividades prearranque_Actividades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prearranque_Actividades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prearranque_Actividades);
        }

        // GET: ADC_Actividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prearranque_Actividades = await _context.PreArranque_Actividades.FindAsync(id);
            if (prearranque_Actividades == null)
            {
                return NotFound();
            }
            return PartialView(prearranque_Actividades);
        }

        // POST: ADC_Actividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreArranque_Actividades prearranque_Actividades)
        {
            if (id != prearranque_Actividades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prearranque_Actividades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADC_ActividadesExists(prearranque_Actividades.Id))
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
            return PartialView(prearranque_Actividades);
        }

        // GET: ADC_Actividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prearranque_Actividades = await _context.PreArranque_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prearranque_Actividades == null)
            {
                return NotFound();
            }

            return View(prearranque_Actividades);
        }

        // POST: ADC_Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prearranque_Actividades = await _context.PreArranque_Actividades.FindAsync(id);
            prearranque_Actividades.Eliminado = 1;
            _context.Update(prearranque_Actividades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_ActividadesExists(int id)
        {
            return _context.PreArranque_Actividades.Any(e => e.Id == id);
        }
    }
}
