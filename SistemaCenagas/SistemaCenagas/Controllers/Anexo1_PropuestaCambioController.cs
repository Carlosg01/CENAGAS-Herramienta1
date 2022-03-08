using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class Anexo1_PropuestaCambioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Anexo1_PropuestaCambioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anexo1_PropuestaCambio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Anexo1_PropuestaCambio.ToListAsync());
        }

        // GET: Anexo1_PropuestaCambio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1_PropuestaCambio == null)
            {
                return NotFound();
            }

            return View(anexo1_PropuestaCambio);
        }

        // GET: Anexo1_PropuestaCambio/Create
        public IActionResult Create()
        {
            Global.gasoductos = _context.Gasoductos.ToList();    
            
            return View();
        }


        public JsonResult getResidencias(string utGasoducto)
        {
            Global.tramos = _context.Tramos.Where(t => t.Ut_Gasoducto.Equals(utGasoducto));
            return Json(new SelectList(Global.tramos, "Residencia", "Residencia"));
        }

        // POST: Anexo1_PropuestaCambio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_PropuestaCambio,Id_Proyecto,Tipo_Cambio,Fecha,Id_Residencia,Sector_Area,Planta_Instalacion,Proceso,Prestacion_Servicio,Descripcion,Resultados_Analisis,Id_ProponenteCambio,Id_ResponsableADC,Resultados_Propuesta,Estatus")] Anexo1_PropuestaCambio anexo1_PropuestaCambio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anexo1_PropuestaCambio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anexo1_PropuestaCambio);
        }

        // GET: Anexo1_PropuestaCambio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio.FindAsync(id);
            if (anexo1_PropuestaCambio == null)
            {
                return NotFound();
            }
            return View(anexo1_PropuestaCambio);
        }

        // POST: Anexo1_PropuestaCambio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_PropuestaCambio,Id_Proyecto,Tipo_Cambio,Fecha,Id_Residencia,Sector_Area,Planta_Instalacion,Proceso,Prestacion_Servicio,Descripcion,Resultados_Analisis,Id_ProponenteCambio,Id_ResponsableADC,Resultados_Propuesta,Estatus")] Anexo1_PropuestaCambio anexo1_PropuestaCambio)
        {
            if (id != anexo1_PropuestaCambio.Id_PropuestaCambio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anexo1_PropuestaCambio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1_PropuestaCambioExists(anexo1_PropuestaCambio.Id_PropuestaCambio))
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
            return View(anexo1_PropuestaCambio);
        }

        // GET: Anexo1_PropuestaCambio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1_PropuestaCambio == null)
            {
                return NotFound();
            }

            return View(anexo1_PropuestaCambio);
        }

        // POST: Anexo1_PropuestaCambio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio.FindAsync(id);
            _context.Anexo1_PropuestaCambio.Remove(anexo1_PropuestaCambio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Anexo1_PropuestaCambioExists(int id)
        {
            return _context.Anexo1_PropuestaCambio.Any(e => e.Id_PropuestaCambio == id);
        }
    }
}
