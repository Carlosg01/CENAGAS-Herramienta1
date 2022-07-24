using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class PreArranque_ProyectoController : Controller
    {
        //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
        private readonly ApplicationDbContext _context;
        private Global global;

        public PreArranque_ProyectoController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
        }

        // GET: ADC
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            global.vista_prearranque = Consultas.PreArranqueVista(_context).Where(a => a.id_proyecto == global.proyectos.Id);

            //Vista adc a cargo
            if (global.session_usuario.user.Id_Rol == global.ADMINISTRADOR)
            {
                global.vista_prearranque_cargo = global.vista_prearranque
                    .Where(a => a.prearranque.Id_LiderEquipoVerificador == global.session_usuario.user.Id).ToList();
            }
            else if(global.session_usuario.user.Id_Rol == global.RESPONSABLE_ADC || global.session_usuario.user.Id_Rol == global.RESPONSABLE_PREARRANQUE)
            {
                global.vista_prearranque_cargo = global.vista_prearranque
                    .Where(a => a.prearranque.Id_Responsable == global.session_usuario.user.Id).ToList();
            }
            else if (global.session_usuario.user.Id_Rol == global.SUPLENTE)
            {
                global.vista_prearranque_cargo = global.vista_prearranque
                    .Where(a => a.prearranque.Id_Suplente == global.session_usuario.user.Id).ToList();
            }

            //global.resumenADC = Consultas.VistaResumenADC(_context);

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }
        public async Task<IActionResult> PreArranque(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id == id).FirstOrDefault();

            //HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            //ViewBag.global = global;
            //return RedirectToAction("Index", "ADCProyecto");
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Create", "PreArranque_Anexo2");
        }
        public async Task<IActionResult> Tareas(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            global.prearranque = global.vista_prearranque.Where(a => a.prearranque.Id == id).FirstOrDefault();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "PreArranque_Procesos");
        }

        // GET: ADC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var aDC = await _context.ADC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(aDC);
        }

        // GET: ADC/Create
        public IActionResult Create()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        // POST: ADC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_ADC,Id_Proyecto,Folio,Id_ProponenteCambio,Id_Lider,Id_ResponsableADC,Id_Suplente,Fecha_Actualizacion,Registro_Eliminado")] ADC aDC)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (ModelState.IsValid)
            {
                _context.Add(aDC);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(aDC);
        }

        // GET: ADC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var aDC = await _context.ADC.FindAsync(id);
            if (aDC == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            global.adc = global.vista_adc.Where(a => a.adc.Id == id).FirstOrDefault();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(aDC);
        }

        // POST: ADC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC aDC)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != aDC.Id)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
                    ViewBag.global = global;
                    //return Content(JsonConvert.SerializeObject(aDC));
                    _context.Update(aDC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADCExists(aDC.Id))
                    {
                        HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                        ViewBag.global = global;
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(aDC);
        }

        // GET: ADC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var aDC = await _context.ADC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(aDC);
        }

        // POST: ADC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var aDC = await _context.ADC.FindAsync(id);
            aDC.Eliminado = 1;
            _context.ADC.Update(aDC);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        private bool ADCExists(int id)
        {
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return _context.ADC.Any(e => e.Id == id);
        }
    }
}
