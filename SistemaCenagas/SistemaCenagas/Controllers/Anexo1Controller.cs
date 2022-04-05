using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class Anexo1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Anexo1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anexo1
        public async Task<IActionResult> Index()
        {   
            return View();
        }

        // GET: Anexo1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1 = await _context.Anexo1
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1 == null)
            {
                return NotFound();
            }

            return View(anexo1);
        }

        // GET: Anexo1/Create
        public IActionResult Create()
        {

            ViewBag.fecha = DateTime.Now.ToString();
            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anexo1 anexo1)
        {
            if (ModelState.IsValid)
            {
                anexo1.Estatus = "Pendiente";
                _context.Add(anexo1);
                await _context.SaveChangesAsync();

                ADC adc = new ADC
                {
                    Id_ADC = anexo1.Id_PropuestaCambio,
                    Id_Proyecto = Global.proyectos.Id_Proyecto,
                    Id_ProponenteCambio = Global.session_usuario.user.Id_Usuario,
                    Id_Lider = 1,
                    Id_ResponsableADC = 1,
                    Id_Suplente = 1,
                    Fecha_Actualizacion = anexo1.Fecha,
                    Registro_Eliminado = 0
                };
                _context.Add(adc);

                await _context.SaveChangesAsync();

                //int id = _context.ADC.OrderByDescending(a => a.Id_ADC).FirstOrDefault().Id_ADC;
                Global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == anexo1.Id_PropuestaCambio).FirstOrDefault();

                for (int i = 0; i < Global.vista_actividadesADC.Count(); i++)
                {
                    //return Content(JsonConvert.SerializeObject(a));
                    ADC_Procesos tarea = new ADC_Procesos
                    {
                        Id_Actividad = Global.vista_actividadesADC.ElementAt(i).Id_Actividad,
                        Id_ADC = anexo1.Id_PropuestaCambio,
                        Avance = (i == 0) ? (9.0f/12)*100 : 0, //primeros 9 atributos necesarios por primera vez de 12 posibles
                        Faltante_Comentarios = "N/A",
                        Plan_Accion = "N/A"
                    };
                    _context.Add(tarea);
                    await _context.SaveChangesAsync();
                }
                

                return RedirectToAction("Index", "ADC_Procesos");
            }
            return PartialView(anexo1);
        }

        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anexo1 = await _context.Anexo1.Where(
                a => a.Id_PropuestaCambio == Global.anexo1.anexo1.Id_PropuestaCambio).FirstOrDefaultAsync();

            Global.proyectos = Consultas.VistaProyectos(_context).Where(
                p => p.Id_Proyecto == Global.anexo1.anexo1.Id_Proyecto).FirstOrDefault();

            Global.tarea = Consultas.VistaTareas(_context)
                .Where(t => t.proceso.Id_ADC == id).FirstOrDefault();
            return PartialView(anexo1);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Anexo1 anexo1)
        {
            //return Content(""+id);
            if (id != anexo1.Id_PropuestaCambio)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                try
                {   
                    _context.Update(anexo1);                    
                    await _context.SaveChangesAsync();

                    ADC adc = _context.ADC.Where(a => a.Id_ADC == Global.adc.adc.Id_ADC).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    Global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == adc.Id_ADC).FirstOrDefault();

                    //return Content(JsonConvert.SerializeObject(anexo1));
                    if(Global.tarea.proceso.Id_Actividad == 1)
                    {
                        a.Avance = 9.0f / 12 * 100;
                        a.Avance += (anexo1.Resultados_Analisis != null && anexo1.Resultados_Analisis.Length > 0) ? (1.0f / 12 * 100) : 0;
                        a.Avance += (anexo1.Resultados_Propuesta != null && anexo1.Resultados_Propuesta.Length > 0) ? (1.0f / 12 * 100) : 0;
                        a.Avance += (anexo1.Estatus != null && anexo1.Estatus.Length > 0) ? (1.0f / 12 * 100) : 0;
                    }
                    //return Content(JsonConvert.SerializeObject(a)); 

                    _context.Update(a);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(anexo1.Id_PropuestaCambio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ADC_Procesos");
            }
            return PartialView(anexo1);
        }

        // GET: Anexo1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1 = await _context.Anexo1
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1 == null)
            {
                return NotFound();
            }

            return View(anexo1);
        }

        // POST: Anexo1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anexo1 = await _context.Anexo1.FindAsync(id);
            _context.Anexo1.Remove(anexo1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Anexo1Exists(int id)
        {
            return _context.Anexo1.Any(e => e.Id_PropuestaCambio == id);
        }

        [HttpPost]
        public JsonResult getGasoductos(int id_residencia)
        {
            string residencia = Global.residencias.Where(r => r.Id_Residencia == id_residencia)
                .FirstOrDefault().Nombre;

            Global.gasoductos = Consultas.getGasoductos(_context, residencia);
            //return Json(JsonConvert.SerializeObject(Global.gasoductos));
            
            return Json(new SelectList(Global.gasoductos, "Ut_Gasoducto", "Gasoducto"));            
        }

        [HttpPost]
        public JsonResult getTramos(string ut_gasoducto)
        {

            Global.tramos = Consultas.getTramos(_context, ut_gasoducto);
            //return Json(JsonConvert.SerializeObject(Global.gasoductos));

            return Json(new SelectList(Global.tramos, "Ut_Tramo", "Tramo"));
        }
    }
}
