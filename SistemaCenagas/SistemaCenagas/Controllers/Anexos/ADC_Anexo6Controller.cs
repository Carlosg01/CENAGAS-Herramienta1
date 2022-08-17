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
    public class ADC_Anexo6Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ADC_Anexo6Controller(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
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
        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            global.anexo1 = Consultas.VistaAnexo1(_context, global.adc.adc.Id);
            global.anexo3 = (from a in _context.ADC_Anexo3
                             join r in _context.Usuarios on a.Id_Responsable_ADC equals r.Id
                             join dsi in _context.Usuarios on a.Id_Director_Seguridad_Industrial equals dsi.Id
                             join deo in _context.Usuarios on a.Id_Director_Ejecutivo_Operacion equals deo.Id
                             join dems in _context.Usuarios on a.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad equals dems.Id
                             where a.Id_Anexo1 == global.adc.adc.Id
                             select new V_Anexo3
                             {
                                 anexo3 = a,
                                 Responsable = $"{r.Nombre} {r.Paterno} {r.Materno}",
                                 Director_Seguridad_Industrial = $"{dsi.Nombre} {dsi.Paterno} {dsi.Materno}",
                                 Director_Ejecutivo_Operacion = $"{deo.Nombre} {deo.Paterno} {deo.Materno}",
                                 Director_Ejecutivo_Mantenimiento = $"{dems.Nombre} {dems.Paterno} {dems.Materno}",
                             }
                             ).FirstOrDefault();

            var a6 = _context.ADC_Anexo6.Where(a => a.Id_Anexo1 == global.adc.adc.Id).FirstOrDefault();

            var model = new ADC_Anexo6_Model
            {
                anexo6 = a6,
                documentacion = _context.ADC_Anexo6_Documentacion.Where(a => a.Id_Anexo6 == a6.Id).ToList()
            };

            ViewBag.a6_catalogo = _context.ADC_Anexo6_Documentacion_Catalogo.ToList();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Anexo6_Model model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //ViewBag.global = global;//
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo6.Id_Anexo1)
            {
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.anexo6);
                    _context.UpdateRange(model.documentacion);

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == model.anexo6.Id_Anexo1 && a.Id_Actividad == 8).FirstOrDefault();
                    a.Avance = 100;
                    _context.Update(a);
                    
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.anexo6.Id_Anexo1))
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
                return RedirectToAction("Index", "ADC_Procesos");
            }
            ViewBag.global = global;
            return PartialView(model);
        }


        private bool Anexo3Exists(int id)
        {
            ViewBag.global = global;
            return _context.ADC_Anexo3.Any(e => e.Id == id);
        }
    }
}
