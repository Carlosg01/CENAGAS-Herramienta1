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
    public class ADC_ActividadesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Global global;

        public ADC_ActividadesController(ApplicationDbContext context)
        {
            _context = context;
            //if(!await getGlobal()) return RedirectToAction("Index", "Home");

            
        }
        public async Task<bool> getGlobal()
        {
            var json = HttpContext.Session.GetString("Global");
            if (json == null || json.Length == 0)
            {
                return false;
            }
            global = JsonConvert.DeserializeObject<Global>(json);
            return true;
        }

        // GET: ADC_Actividades
        public async Task<IActionResult> Index()
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (!global.session.Equals("LogIn"))
            {
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }
            global.vista_actividadesADC = Consultas.VistaActividadesADC(_context);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            ViewBag.global = global;
            return View();
        }

        public async Task<IActionResult> Normativas(int? id)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            global.actividadADC = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (global.actividadADC == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ADC_Normativas");
        }

        // GET: ADC_Actividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var aDC_Actividades = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Actividades == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return View(aDC_Actividades);
        }

        // GET: ADC_Actividades/Create
        public async Task<IActionResult> Create()
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            ViewBag.global = global;
            return View();
        }

        // POST: ADC_Actividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Actividad,Actividad,Registro_Eliminado")] ADC_Actividades aDC_Actividades)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Actividades);
                await _context.SaveChangesAsync();
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.global = global;
            return View(aDC_Actividades);
        }

        // GET: ADC_Actividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var aDC_Actividades = await _context.ADC_Actividades.FindAsync(id);
            if (aDC_Actividades == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            ViewBag.global = global;
            return PartialView(aDC_Actividades);
        }

        // POST: ADC_Actividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Actividad,Actividad,Registro_Eliminado")] ADC_Actividades aDC_Actividades)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (id != aDC_Actividades.Id)
            {
                ViewBag.global = global;
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
            return PartialView(aDC_Actividades);
        }

        // GET: ADC_Actividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var aDC_Actividades = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Actividades == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return View(aDC_Actividades);
        }

        // POST: ADC_Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");
            var aDC_Actividades = await _context.ADC_Actividades.FindAsync(id);
            aDC_Actividades.Eliminado = 1;
            _context.ADC_Actividades.Update(aDC_Actividades);
            await _context.SaveChangesAsync();
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_ActividadesExists(int id)
        {
            ViewBag.global = global;
            return _context.ADC_Actividades.Any(e => e.Id == id);
        }
    }
}
