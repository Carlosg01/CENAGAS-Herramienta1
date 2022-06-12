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
    public class PreArranque_NormativasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreArranque_NormativasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ADC_Normativas
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }
            Global.vista_normativas = Consultas.VistaNormativasADC(_context);
            return View();
        }

        // GET: ADC_Normativas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.normativas = Global.vista_normativas.Where(
                n => n.adc_normativas.Id == id).FirstOrDefault();

            if (Global.normativas.adc_normativas == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: ADC_Normativas/Create
        public IActionResult Create()
        {
            int num_normativas = _context.ADC_Normativas.Where(n => n.Id_Actividad == Global.actividadADC.Id)
                .Count() + 1;
            ViewBag.clave_normativa = Global.actividadADC.Id.ToString() + "." + num_normativas.ToString();

            return View();
        }

        // POST: ADC_Normativas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Normativa,Id_Actividad,Clave,Responsable,Descripcion,Id_Anexo,Registro_Eliminado")] ADC_Normativas aDC_Normativas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Normativas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aDC_Normativas);
        }

        // GET: ADC_Normativas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Normativas = await _context.ADC_Normativas.FindAsync(id);
            if (aDC_Normativas == null)
            {
                return NotFound();
            }
            return PartialView(aDC_Normativas);
        }

        // POST: ADC_Normativas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Normativa,Id_Actividad,Clave,Responsable,Descripcion,Id_Anexo,Registro_Eliminado")] ADC_Normativas aDC_Normativas)
        {
            if (id != aDC_Normativas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aDC_Normativas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADC_NormativasExists(aDC_Normativas.Id))
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
            return PartialView(aDC_Normativas);
        }

        // GET: ADC_Normativas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Normativas = await _context.ADC_Normativas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Normativas == null)
            {
                return NotFound();
            }

            return View(aDC_Normativas);
        }

        // POST: ADC_Normativas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aDC_Normativas = await _context.ADC_Normativas.FindAsync(id);
            _context.ADC_Normativas.Remove(aDC_Normativas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_NormativasExists(int id)
        {
            return _context.ADC_Normativas.Any(e => e.Id == id);
        }
    }
}
