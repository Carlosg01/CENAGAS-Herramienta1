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

                #region Prearranque Anexo2 seccion 2

                var tareas = _context.PreArranque_Anexo2_Seccion2_Catalogo.ToList();

                foreach(var tarea in tareas)
                {
                    var item = new PreArranque_Anexo2_Seccion2
                    {
                        Nombre = tarea.Tarea,
                        Id_Anexo2 = Global.prearranque.anexo2.Id
                    };
                    _context.Add(item);
                    await _context.SaveChangesAsync();

                    var subtareas = _context.PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo.Where(a => a.Id_Tarea == tarea.Id).ToList();

                    foreach(var subtarea in subtareas)
                    {
                        _context.Add(new PreArranque_Anexo2_Seccion2_ElementosRevision
                        {
                            Elemento_Revision = subtarea.Subtarea,
                            Atendido = "",
                            Observacion = "",
                            Id_Anexo2_Seccion2 = item.Id
                        });
                    }
                }

                #endregion



                return RedirectToAction("Index", "PreArranque_Procesos");
            }
            return PartialView(model);
        }

        public async Task<IActionResult> Create2()
        {
            PreArranque pre = _context.PreArranque.Where(a => a.Id_ADC == Global.adc.adc.Id).FirstOrDefault();
            if (pre == null)
            {
                pre = new PreArranque
                {
                    Con_ADC = "Si",//model.preArranque.Con_ADC,
                    Id_ADC = Global.adc.adc.Id,
                    Fecha_Actualizacion = DateTime.Now.ToString(),
                    Id_Proyecto = Global.adc.adc.Id_Proyecto,
                    Folio = "N/A",
                    Id_Responsable = Global.adc.adc.Id_ResponsableADC,
                    Id_Suplente = Global.adc.adc.Id_ResponsableADC,
                    //Id_LiderEquipoVerificador = model.preArranque.Id_LiderEquipoVerificador
                };
                PreArranque_Anexo2 anexo2 = null;

                ADC_Anexo1 adc_anexo1 = _context.ADC_Anexo1
                        .Where(a => a.Id == pre.Id_ADC).FirstOrDefault();

                ADC_Anexo3 adc_anexo3 = _context.ADC_Anexo3
                    .Where(a => a.Id_Anexo1 == pre.Id_ADC).FirstOrDefault();

                ADC_Equipo_Verificador adc_ev = _context.ADC_Equipo_Verificador
                    .Where(a => a.Id_ADC == pre.Id_ADC).FirstOrDefault();

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
                    Fecha_Elaboracion = pre.Fecha_Actualizacion

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

                PreArranque_Anexo1 anexo1 = new PreArranque_Anexo1
                {
                    Fecha_Elaboracion = pre.Fecha_Actualizacion,
                    Id_Prearranque = pre.Id
                };
                _context.Add(anexo1);
                await _context.SaveChangesAsync();

                for (int i = 0; i < Global.vista_actividadesPreArranque.Count(); i++)
                {
                    //return Content(JsonConvert.SerializeObject(a));
                    PreArranque_Procesos tarea = new PreArranque_Procesos
                    {
                        Id_Actividad = Global.vista_actividadesADC.ElementAt(i).Id,
                        Id_PreArranque = pre.Id,
                        Avance =  i == 0 ? 100 : 0,
                        Faltante_Comentarios = "N/A",
                        Plan_Accion = "N/A",
                        Terminado = i == 0 ? "true" : "false",
                        Confirmado = "false"

                    };
                    _context.Add(tarea);
                    await _context.SaveChangesAsync();
                }
            }

            Global.prearranque = Consultas.PreArranqueVista(_context)
                    .Where(a => a.prearranque.Id == pre.Id).FirstOrDefault();

            return RedirectToAction("Index", "PreArranque_Procesos");
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(PreArranqueModel_Crear model)
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

            Global.adcs_con_prearranque =
                (from adc in Consultas.VistaADC(_context)
                 join pro in _context.ADC_Procesos on adc.adc.Id equals pro.Id_ADC
                 join p in _context.Proyectos on adc.id_proyecto equals p.Id
                 where pro.Id_Actividad == 5 && pro.Terminado == "true" && pro.Confirmado == "true" && p.Id == Global.proyectos.Id
                 select adc).ToList();


            PreArranqueModel_Crear model = new PreArranqueModel_Crear();

            model.anexo1 = _context.PreArranque_Anexo1.Where(a => a.Id_Prearranque == id).FirstOrDefault();
            model.anexo2 = _context.PreArranque_Anexo2.Where(a => a.Id_Prearranque == id).FirstOrDefault();
            model.preArranque = _context.PreArranque.Find(id);

            Global.proyectos = Consultas.VistaProyectos(_context)
                .Where(p => p.Id == model.preArranque.Id_Proyecto).FirstOrDefault();

            string residencia = Global.residencias.Where(r => r.Id == model.anexo2.Id_Residencia)
                .FirstOrDefault().Nombre;

            Global.gasoductos = Consultas.getGasoductos(_context, residencia);
            Global.tramos = Consultas.getTramos(_context, model.anexo2.Ut_Gasoducto);


            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreArranqueModel_Crear model)
        {
            //return Content(""+id);
            if (id != model.preArranque.Id)
            {
                return NotFound();
            }


            //return Content(JsonConvert.SerializeObject(anexo1));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.anexo1);
                    _context.Update(model.anexo2);
                    _context.Update(model.preArranque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(model.preArranque.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PreArranque_Admin");
            }
            return PartialView(model);
        }

        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit_Seccion2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new PreArranque_Anexo2_Model();
            model.Id_Anexo2 = Global.prearranque.anexo2.Id;
            model.seccion2 = new List<PreArranque_Anexo2_Seccion2_Model>();

            var tareas = _context.PreArranque_Anexo2_Seccion2.Where(a => a.Id_Anexo2 == Global.prearranque.anexo2.Id).ToList();
            foreach(var tarea in tareas)
            {

                //var subtareas = _context.PreArranque_Anexo2_Seccion2_ElementosRevision.Where(a => a.Id_Anexo2_Seccion2 == tarea.Id).ToList();
                var subtareas = (from a in _context.PreArranque_Anexo2_Seccion2_ElementosRevision
                                 where a.Id_Anexo2_Seccion2 == tarea.Id
                                 select new PreArranque_Anexo2_Seccion2_ElementosRevision_Model
                                 {
                                     elemento = a,
                                     TareaPrincipal = tarea.Nombre
                                 }).ToList();

                model.seccion2.Add(new PreArranque_Anexo2_Seccion2_Model
                {
                    tareas = tarea,
                    subtareas = subtareas
                });
            }
            



            return View(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Seccion2(int id, PreArranque_Anexo2_Model model)
        {
            //return Content(""+id);
            if (id != model.Id_Anexo2)
            {
                return NotFound();
            }


            //return Content(JsonConvert.SerializeObject(anexo1));

            if (ModelState.IsValid)
            {
                try
                {
                    
                    foreach(var tarea in model.seccion2)
                    {
                        foreach(var subtarea in tarea.subtareas)
                        {
                            subtarea.elemento.Atendido = subtarea.RadioAtendido["atendido"].ToString();
                            subtarea.elemento.Id_Anexo2_Seccion2 = tarea.tareas.Id;
                            _context.Update(subtarea.elemento);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(model.Id_Anexo2))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PreArranque_Procesos");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit_Seccion2_Elementos(int? Id)
        {
            var item = _context.PreArranque_Anexo2_Seccion2_ElementosRevision.Find(Id);
            var tarea = _context.PreArranque_Anexo2_Seccion2.Where(a => a.Id == item.Id_Anexo2_Seccion2).FirstOrDefault();
            var model = new PreArranque_Anexo2_Seccion2_ElementosRevision_Model
            {
                elemento = item,
                TareaPrincipal = tarea.Nombre
            };

            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Seccion2_Elementos(int id, PreArranque_Anexo2_Seccion2_ElementosRevision_Model model)
        {
            //return Content(""+id);
            if (id != model.elemento.Id)
            {
                return NotFound();
            }


            //return Content(JsonConvert.SerializeObject(anexo1));

            if (ModelState.IsValid)
            {
                try
                {
                    model.elemento.Tipo_Revision = model.RadioTipoRevision["tipoRevision"].ToString();
                    model.elemento.Tipo_Hallazgo = model.RadioTipoHallazgo["tipoHallazgo"].ToString();
                    model.elemento.Atendido = model.RadioAtendido["atendido"].ToString();
                    _context.Update(model.elemento);

                    Global.prearranque.prearranque.Fecha_Actualizacion = DateTime.Now.ToString();
                    _context.Update(Global.prearranque.prearranque);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(model.elemento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit_Seccion2", new { id = Global.prearranque.prearranque.Id });
            }
            return PartialView(model);
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
            return _context.PreArranque.Any(e => e.Id == id);
        }

        [HttpPost]
        public JsonResult getAdcPrearranque(int idProyecto)
        {
            Global.adcsPrearranque =
                (from vistaAdc in Consultas.VistaADC(_context)
                 join adc in _context.ADC on vistaAdc.adc.Id equals adc.Id
                 join pro in _context.ADC_Procesos on vistaAdc.adc.Id equals pro.Id_ADC
                 join p in _context.Proyectos on vistaAdc.id_proyecto equals p.Id
                 where pro.Id_Actividad == 5 && pro.Terminado == "true" && pro.Confirmado == "true" && p.Id == idProyecto
                 select adc).ToList();

            //return Json(JsonConvert.SerializeObject(Global.gasoductos));
            
            return Json(new SelectList(Global.adcsPrearranque, "Id", "Folio"));
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
