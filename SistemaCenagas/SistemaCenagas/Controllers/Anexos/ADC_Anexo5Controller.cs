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
    public class ADC_Anexo5Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADC_Anexo5Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.anexo1 = Consultas.VistaAnexo1(_context, Global.adc.adc.Id);
            Global.anexo3 = (from a in _context.ADC_Anexo3
                             join r in _context.Usuarios on a.Id_Responsable_ADC equals r.Id
                             join dsi in _context.Usuarios on a.Id_Director_Seguridad_Industrial equals dsi.Id
                             join deo in _context.Usuarios on a.Id_Director_Ejecutivo_Operacion equals deo.Id
                             join dems in _context.Usuarios on a.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad equals dems.Id
                             where a.Id_Anexo1 == Global.adc.adc.Id
                             select new Global.V_Anexo3
                             {
                                 anexo3 = a,
                                 Responsable = $"{r.Nombre} {r.Paterno} {r.Materno}",
                                 Director_Seguridad_Industrial = $"{dsi.Nombre} {dsi.Paterno} {dsi.Materno}",
                                 Director_Ejecutivo_Operacion = $"{deo.Nombre} {deo.Paterno} {deo.Materno}",
                                 Director_Ejecutivo_Mantenimiento = $"{dems.Nombre} {dems.Paterno} {dems.Materno}",
                             }
                             ).FirstOrDefault();


            ViewBag.fechaRetiroCambioTemporal = _context.ADC_Anexo4.Where(a => a.Id_Anexo1 == Global.adc.adc.Id).FirstOrDefault().Fecha_Retiro_Cambio_Temporal;

            var model = _context.ADC_Anexo5.Where(a => a.Id_Anexo1 == Global.adc.adc.Id).FirstOrDefault();
            
            
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Anexo5 model)
        {
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.Id_Anexo1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(model);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.Id_Anexo1))
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
            return PartialView(model);
        }


        private bool Anexo3Exists(int id)
        {
            return _context.ADC_Anexo3.Any(e => e.Id == id);
        }
    }
}
