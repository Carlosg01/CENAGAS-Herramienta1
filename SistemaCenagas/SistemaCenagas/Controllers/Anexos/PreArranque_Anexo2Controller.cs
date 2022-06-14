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
    public class PreArranque_Anexo2Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreArranque_Anexo2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anexo1
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }



            return View();
        }

        // GET: Anexo1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1 = await _context.ADC_Anexo1
                .FirstOrDefaultAsync(m => m.Id == id);
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

            Global.adcs_con_prearranque =
                (from adc in Consultas.VistaADC(_context)
                 join pro in _context.ADC_Procesos on adc.adc.Id equals pro.Id_ADC
                 join p in _context.Proyectos on adc.id_proyecto equals p.Id
                 where pro.Id_Actividad == 5 && pro.Terminado == "true" && pro.Confirmado == "true" && p.Id == Global.proyectos.Id
                 select adc).ToList();

            //Global.adcs_con_prearranque = Consultas.VistaADC(_context)
              //  .Where(a => a.adc.PreArranque == "No" && a.adc.Id_Proyecto == Global.proyectos.Id);
            
            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreArranqueModel_Crear model)
        {
            if (ModelState.IsValid)
            {
                PreArranque pre = new PreArranque
                {
                    Con_ADC = model.preArranque.Con_ADC,
                    Id_ADC = model.preArranque.Con_ADC == "Si" ? model.preArranque.Id_ADC : 0,
                    Fecha_Actualizacion = model.preArranque.Fecha_Actualizacion,
                    Id_Proyecto = model.preArranque.Id_Proyecto,
                    Folio = "N/A",
                    Id_Responsable = Global.session_usuario.user.Id,
                    Id_Suplente = Global.session_usuario.user.Id,
                    //Id_LiderEquipoVerificador = model.preArranque.Id_LiderEquipoVerificador
                };
                PreArranque_Anexo2 anexo2 = null;

                if (model.preArranque.Con_ADC == "No")
                {
                    pre.Id_LiderEquipoVerificador = model.preArranque.Id_LiderEquipoVerificador;
                    _context.Add(pre);
                    await _context.SaveChangesAsync();

                    anexo2 = new PreArranque_Anexo2
                    {
                        Id_Residencia = model.anexo2.Id_Residencia,
                        Ut_Gasoducto = model.anexo2.Ut_Gasoducto,
                        Ut_Tramo = model.anexo2.Ut_Tramo,
                        Id_Prearranque = pre.Id,
                        Fecha_Elaboracion = model.preArranque.Fecha_Actualizacion
                    };
                    _context.Add(anexo2);
                    await _context.SaveChangesAsync();

                    PreArranque_Equipo_Verificador pre_ev = new PreArranque_Equipo_Verificador
                    {
                        Id_PreArranque = pre.Id,
                        Id_LiderEquipoVerificador = pre.Id_LiderEquipoVerificador
                    };
                    _context.Add(pre_ev);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    ADC_Anexo1 adc_anexo1 = _context.ADC_Anexo1
                        .Where(a => a.Id == model.preArranque.Id_ADC).FirstOrDefault();

                    ADC_Anexo3 adc_anexo3 = _context.ADC_Anexo3
                        .Where(a => a.Id_Anexo1 == model.preArranque.Id_ADC).FirstOrDefault();

                    ADC_Equipo_Verificador adc_ev = _context.ADC_Equipo_Verificador
                        .Where(a => a.Id_ADC == model.preArranque.Id_ADC).FirstOrDefault();

                    List<ADC_Equipo_Verificador_Integrantes> adc_ev_integrantes = _context.ADC_Equipo_Verificador_Integrantes
                        .Where(a => a.Id_Equipo_Verificador_ADC == adc_ev.Id).ToList();


                    pre.Id_LiderEquipoVerificador = adc_ev.Id_LiderEquipoVerificador;
                    _context.Add(pre);
                    await _context.SaveChangesAsync();

                    anexo2 = new PreArranque_Anexo2
                    {
                        Id_Residencia = adc_anexo1.Id_Residencia,
                        Ut_Gasoducto = adc_anexo1.Ut_Gasoducto,
                        Ut_Tramo = adc_anexo1.Ut_Tramo,
                        Id_Prearranque = pre.Id,
                        Fecha_Elaboracion = model.preArranque.Fecha_Actualizacion

                    };
                    _context.Add(anexo2);
                    await _context.SaveChangesAsync();

                    PreArranque_Equipo_Verificador pre_ev = new PreArranque_Equipo_Verificador
                    {
                        Id_PreArranque = pre.Id,
                        Id_LiderEquipoVerificador = adc_ev.Id_LiderEquipoVerificador
                    };
                    _context.Add(pre_ev);
                    await _context.SaveChangesAsync();

                    foreach (var integrante in adc_ev_integrantes)
                    {
                        PreArranque_Equipo_Verificador_Integrantes pre_evi = new PreArranque_Equipo_Verificador_Integrantes
                        {
                            Id_Equipo_Verificador_PreArranque = pre_ev.Id,
                            Id_Usuario = integrante.Id_Usuario,
                            Estatus = integrante.Estatus
                        };
                        _context.Add(pre_evi);
                        await _context.SaveChangesAsync();
                    }
                }

                PreArranque_Anexo1 anexo1 = new PreArranque_Anexo1
                {
                    Fecha_Elaboracion = pre.Fecha_Actualizacion,
                    Id_Prearranque = pre.Id
                };
                _context.Add(anexo1);
                await _context.SaveChangesAsync();


                Global.prearranque = Consultas.PreArranqueVista(_context)
                    .Where(a => a.prearranque.Id == pre.Id).FirstOrDefault();

                for (int i = 0; i < Global.vista_actividadesPreArranque.Count(); i++)
                {
                    //return Content(JsonConvert.SerializeObject(a));
                    PreArranque_Procesos tarea = new PreArranque_Procesos
                    {
                        Id_Actividad = Global.vista_actividadesADC.ElementAt(i).Id,
                        Id_PreArranque = pre.Id,
                        Avance = i == 0 && model.preArranque.Con_ADC == "Si" ? 100 : 0,
                        Faltante_Comentarios = "N/A",
                        Plan_Accion = "N/A",
                        Terminado = i == 0 && model.preArranque.Con_ADC == "Si" ? "true" : "false",
                        Confirmado = "false"

                    };
                    _context.Add(tarea);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "PreArranque_Procesos");
            }
            return PartialView(model);
        }

        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anexo1 = await _context.ADC_Anexo1.Where(
                a => a.Id == Global.anexo1.anexo1.Id).FirstOrDefaultAsync();

            Global.proyectos = Consultas.VistaProyectos(_context).Where(
                p => p.Id == Global.anexo1.anexo1.Id_Proyecto).FirstOrDefault();

            Global.tarea = Consultas.VistaTareas(_context)
                .Where(t => t.proceso.Id_ADC == id).FirstOrDefault();

            //ViewBag.


            return PartialView(anexo1);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Anexo1 anexo1)
        {
            //return Content(""+id);
            if (id != anexo1.Id)
            {
                return NotFound();
            }


            //return Content(JsonConvert.SerializeObject(anexo1));

            if (ModelState.IsValid)
            {
                try
                {
                    string x = anexo1.Tipo_Cambio.ToUpper()[0].ToString();
                    string xx = "MO";
                    string xxx = (anexo1.Id % 10 == 0) ? "0" + anexo1.Id.ToString() : "00" + anexo1.Id.ToString();
                    string xxxx = DateTime.Now.Year.ToString();

                    string stringFolio = $"{x}-{xx}-{xxx}-{xxxx}";

                    _context.Update(anexo1);                    
                    await _context.SaveChangesAsync();

                    ADC adc = _context.ADC.Where(a => a.Id == Global.adc.adc.Id).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    adc.Folio = stringFolio;
                    Global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    Global.adc.adc.Folio = adc.Folio;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == adc.Id).FirstOrDefault();

                    //return Content(JsonConvert.SerializeObject(anexo1));
                    if(Global.tarea.proceso.Id_Actividad == 1)
                    {
                        a.Avance = anexo1.Estatus.Equals("Aceptado") || anexo1.Estatus.Equals("Rechazado") ? 100 : 0;
                        /*
                        a.Avance = 9.0f / 12 * 100;
                        a.Avance += (anexo1.Resultados_Analisis != null && anexo1.Resultados_Analisis.Length > 0) ? (1.0f / 12 * 100) : 0;
                        a.Avance += (anexo1.Resultados_Propuesta != null && anexo1.Resultados_Propuesta.Length > 0) ? (1.0f / 12 * 100) : 0;
                        a.Avance += (anexo1.Estatus != null && anexo1.Estatus.Length > 0) ? (1.0f / 12 * 100) : 0;
                        */
                    }
                    //return Content(JsonConvert.SerializeObject(a)); 

                    _context.Update(a);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(anexo1.Id))
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

            var anexo1 = await _context.ADC_Anexo1
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var anexo1 = await _context.ADC_Anexo1.FindAsync(id);
            _context.ADC_Anexo1.Remove(anexo1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Anexo1Exists(int id)
        {
            return _context.ADC_Anexo1.Any(e => e.Id == id);
        }

        [HttpPost]
        public JsonResult getGasoductos(int id_residencia)
        {
            string residencia = Global.residencias.Where(r => r.Id == id_residencia)
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
