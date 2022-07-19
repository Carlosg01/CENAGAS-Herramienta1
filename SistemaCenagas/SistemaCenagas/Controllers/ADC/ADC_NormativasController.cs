using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    [Authorize]
    public class ADC_NormativasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ADC_NormativasController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
        }

        // GET: ADC_Normativas
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }
            global.vista_normativas = Consultas.VistaNormativasADC(_context, global);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        // GET: ADC_Normativas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            global.normativas = global.vista_normativas.Where(
                n => n.adc_normativas.Id == id).FirstOrDefault();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));

            if (global.normativas.adc_normativas == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return View();
        }

        // GET: ADC_Normativas/Create
        public IActionResult Create()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            int num_normativas = _context.ADC_Normativas.Where(n => n.Id_Actividad == global.actividadADC.Id)
                .Count() + 1;
            ViewBag.clave_normativa = global.actividadADC.Id.ToString() + "." + num_normativas.ToString();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        // POST: ADC_Normativas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Normativa,Id_Actividad,Clave,Responsable,Descripcion,Id_Anexo,Registro_Eliminado")] ADC_Normativas aDC_Normativas)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Normativas);
                await _context.SaveChangesAsync();
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.global = global;
            return View(aDC_Normativas);
        }

        // GET: ADC_Normativas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var aDC_Normativas = await _context.ADC_Normativas.FindAsync(id);
            if (aDC_Normativas == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            ViewBag.global = global;
            return PartialView(aDC_Normativas);
        }

        // POST: ADC_Normativas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Normativa,Id_Actividad,Clave,Responsable,Descripcion,Id_Anexo,Registro_Eliminado")] ADC_Normativas aDC_Normativas)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != aDC_Normativas.Id)
            {
                ViewBag.global = global;
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
                        ViewBag.global = global;
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.global = global;
            return PartialView(aDC_Normativas);
        }

        // GET: ADC_Normativas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var aDC_Normativas = await _context.ADC_Normativas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Normativas == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return View(aDC_Normativas);
        }

        // POST: ADC_Normativas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var aDC_Normativas = await _context.ADC_Normativas.FindAsync(id);
            _context.ADC_Normativas.Remove(aDC_Normativas);
            await _context.SaveChangesAsync();
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_NormativasExists(int id)
        {
            ViewBag.global = global;
            return _context.ADC_Normativas.Any(e => e.Id == id);
        }
    }
}
