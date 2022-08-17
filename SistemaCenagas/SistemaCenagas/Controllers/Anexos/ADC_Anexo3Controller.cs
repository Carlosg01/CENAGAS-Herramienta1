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
    public class ADC_Anexo3Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ADC_Anexo3Controller(ApplicationDbContext context)
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
        // GET: Anexo1
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.global = global;
            return View();
        }

        // GET: Anexo1/Create
        public IActionResult Create()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            ViewBag.fecha = DateTime.Now.ToString();

            ViewBag.folio = global.adc.adc.Folio;
            ViewBag.TipoCambio1 = global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{global.adc.adc.Folio[2]}{global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = global.anexo1.residencia;
            ViewBag.liderEV = global.adc.lider;

            global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Eliminado == 0).ToList();
            ViewBag.clave = "PRO-CEN-UTA-022";
            /*
            Model_ProyectoMiembro model_ProyectoMiembro = new Model_ProyectoMiembro();
            model_ProyectoMiembro.miembros = new List<string>();
            model_ProyectoMiembro.idMiembro = new List<int>();
            for (int i = 0; i < global.vista_usuarios.Count(); i++)
            {
                model_ProyectoMiembro.miembros.Add("false");
                model_ProyectoMiembro.idMiembro.Add(global.vista_usuarios.ElementAt(i).user.Id_Usuario);
            }
            */
            //Documentación
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));

            ViewBag.global = global;
            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ADC_Anexo3Model_EquipoVerificador anexo3_model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (ModelState.IsValid)
            {
                anexo3_model.anexo3.Tipo_ADC = anexo3_model.RadioTipo["tipo"].ToString();
                anexo3_model.anexo3.Otro_Elemento = anexo3_model.OtroElemento_CheckValue.Equals("true") ?
                    $"{anexo3_model.OtroElemento_CheckValue}:{anexo3_model.OtroElemento}" : anexo3_model.OtroElemento_CheckValue;

                //Anexo3 anexo = anexo3.anexo33
                anexo3_model.anexo3.Id_Anexo2 = global.anexo1.anexo1.Id_Anexo2;
                anexo3_model.anexo3.Id_Responsable_ADC = global.adc.adc.Id_ResponsableADC;
                anexo3_model.anexo3.Id_Director_Seguridad_Industrial = 1;
                anexo3_model.anexo3.Id_Director_Ejecutivo_Operacion = 1;
                anexo3_model.anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad = 1;
                _context.Add(anexo3_model.anexo3);

                ADC_Equipo_Verificador ev = new ADC_Equipo_Verificador
                {
                    Id_ADC = global.anexo1.anexo1.Id,
                    Id_LiderEquipoVerificador = global.adc.adc.Id_LiderEquipoVerificador
                };

                _context.Add(ev);

                await _context.SaveChangesAsync();

                for (int i = 0; i < anexo3_model.miembros.Count(); i++)
                {
                    if (anexo3_model.miembros[i].Equals("true"))
                    {
                        ADC_Equipo_Verificador_Integrantes integrante = new ADC_Equipo_Verificador_Integrantes
                        {
                            Id_Equipo_Verificador_ADC = ev.Id,
                            Id_Usuario = anexo3_model.idMiembro[i],
                            Estatus = "Agregado"
                        };

                        _context.Add(integrante);
                        await _context.SaveChangesAsync();
                    }
                }

                //DOCUMENTACION
                anexo3_model.documentacion = new ADC_Anexo3_Documentacion[22]; //new List<Anexo3_Documentacion>();
                var catalogoDoc = _context.ADC_Anexo3_CatalogoTipoDocumentacion.ToList();

                List<ADC_Equipo_Verificador_Integrantes> integrantes = _context.ADC_Equipo_Verificador_Integrantes
                    .Where(a => a.Id_Equipo_Verificador_ADC == ev.Id).ToList();
                int numIntegrantes = integrantes.Count();
                anexo3_model.responsables = new ADC_Anexo3_DocumentacionResponsable[numIntegrantes * 22];

                int i_integrante = 0;
                for(int i = 1; i <= 22; i++)
                {
                    anexo3_model.documentacion[i-1] = (new ADC_Anexo3_Documentacion
                    {
                        Id_Anexo3 = anexo3_model.anexo3.Id,
                        Id_Tipo = i,//catalogoDoc.ElementAt(i-1).Id,
                        Check = "false",
                        //Id_Responsable = global.adc.adc.Id_LiderEquipoVerificador
                    });
                    _context.Update(anexo3_model.documentacion.ElementAt(i-1));
                    await _context.SaveChangesAsync();

                    for (int j = 0; j < numIntegrantes; j++)
                    {
                        anexo3_model.responsables[i_integrante] = new ADC_Anexo3_DocumentacionResponsable
                        {
                            Id_Responsable = integrantes[j].Id_Usuario,
                            Check = "false",
                            Estatus = integrantes[j].Estatus,
                            Id_Anexo3 = anexo3_model.anexo3.Id,
                            Id_Documentacion = anexo3_model.documentacion[i-1].Id
                        };

                        _context.Add(anexo3_model.responsables[i_integrante]);

                        i_integrante++;
                    }
                }
                await _context.SaveChangesAsync();

                ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == anexo3_model.anexo3.Id_Anexo1 && a.Id_Actividad == 2).FirstOrDefault();
                a.Avance = 100;
                _context.Update(a);
                await _context.SaveChangesAsync();

                //ANEXO 4
                _context.Add(new ADC_Anexo4
                {
                    Id_Anexo1 = anexo3_model.anexo3.Id_Anexo1,
                    Id_Anexo3 = anexo3_model.anexo3.Id,
                    Id_Residente = global.ADMINISTRADOR,
                    Autorizacion_Inicio_Operacion = "Pendiente"
                });

                //ANEXO 5
                _context.Add(new ADC_Anexo5
                {
                    Id_Anexo1 = anexo3_model.anexo3.Id_Anexo1,
                    Id_Responsable_Cambio_Temporal = global.ADMINISTRADOR,
                    Id_Anexo3 = anexo3_model.anexo3.Id,
                    Confirmacion_Retiro_Cambios_Temporales = "Pendiente"
                });

                //ANEXO 6
                var a6 = new ADC_Anexo6
                {
                    Id_Anexo1 = anexo3_model.anexo3.Id_Anexo1,
                    Id_anexo3 = anexo3_model.anexo3.Id
                };
                _context.Add(a6);

                await _context.SaveChangesAsync();

                var a6_catalogo = _context.ADC_Anexo6_Documentacion_Catalogo.ToList();
                foreach(var item in a6_catalogo)
                {
                    _context.Add(new ADC_Anexo6_Documentacion
                    {
                        Id_Anexo6 = a6.Id,
                        Id_Elemento_Catalogo = item.Id,
                        Check = "false"
                    });
                    await _context.SaveChangesAsync();
                }

                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "ADC_Procesos");
            }
            ViewBag.global = global;
            return PartialView(anexo3_model);
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
            ViewBag.folio = global.adc.adc.Folio;
            ViewBag.TipoCambio1 = global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{global.adc.adc.Folio[2]}{global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = global.anexo1.residencia;
            ViewBag.liderEV = global.adc.lider;
            ViewBag.clave = "PRO-CEN-UTA-022";

            int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;

            var equipoVerificador = Consultas.VistaEquipoVerificador(_context, idEV);


            ADC_Anexo3Model_EquipoVerificador model = new ADC_Anexo3Model_EquipoVerificador();
            model.anexo3 = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == id).OrderBy(a => a.Id).LastOrDefault();
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            if (model.anexo3.Otro_Elemento.Contains(":"))
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento.Split(":")[0];
                model.OtroElemento = model.anexo3.Otro_Elemento.Split(":")[1];
            }
            else
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento;
            }

            //model.RadioNuevo = "false";
            //model.RadioRehabilitado = "false";
            //model.RadioModificado = "false";

            global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Eliminado == 0).ToList();

            if(equipoVerificador != null)
            {
                for (int i = 0; i < global.vista_usuarios.Count(); i++)
                {
                    model.miembros.Add("false");
                    model.idMiembro.Add(global.vista_usuarios.ElementAt(i).user.Id);

                    for (int j = 0; j < equipoVerificador.Count(); j++)
                    {
                        if (global.vista_usuarios.ElementAt(i).user.Id == equipoVerificador.ElementAt(j).integrante.Id_Usuario &&
                            equipoVerificador.ElementAt(j).integrante.Estatus.Equals("Agregado"))
                        {
                            model.miembros[i] = "true";
                            break;
                        }
                    }
                }
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));

            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            ViewBag.global = global;//
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    model.anexo3.Tipo_ADC = model.RadioTipo["tipo"].ToString();
                    model.anexo3.Otro_Elemento = model.OtroElemento_CheckValue.Equals("true") ?
                        $"{model.OtroElemento_CheckValue}:{model.OtroElemento}" : model.OtroElemento_CheckValue;

                    _context.Update(model.anexo3);
                    await _context.SaveChangesAsync();

                    //int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;
                    var EV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault();

                    //var equipoVerificador = Consultas.VistaEquipoVerificador(_context, idEV);

                    if(Consultas.VistaEquipoVerificador(_context, EV.Id).Count() > 0)
                    {
                        for (int i = 0; i < model.miembros.Count(); i++)
                        {
                            ADC_Equipo_Verificador_Integrantes integrante = _context.ADC_Equipo_Verificador_Integrantes
                                .Where(e => e.Id_Equipo_Verificador_ADC == EV.Id && e.Id_Usuario == model.idMiembro[i]).FirstOrDefault();


                            if (integrante != null)
                            {
                                //p.Id_Proyecto = proyectos.proyecto.Id_Proyecto;
                                //p.Id_Usuario = proyectos.idMiembro[i];
                                integrante.Estatus = (model.miembros[i].Equals("true") ? "Agregado" : "Eliminado");
                                _context.Update(integrante);
                                ViewBag.global = global;//
                                //return Content(JsonConvert.SerializeObject(p));

                            }
                            else if (model.miembros[i].Equals("true"))
                            {
                                integrante = new ADC_Equipo_Verificador_Integrantes
                                {
                                    Id_Equipo_Verificador_ADC = EV.Id,
                                    Id_Usuario = model.idMiembro[i],
                                    Estatus = "Agregado"
                                };
                                _context.Add(integrante);

                                List<ADC_Anexo3_Documentacion> documentacion = _context.ADC_Anexo3_Documentacion
                                    .Where(a => a.Id_Anexo3 == model.anexo3.Id).ToList();

                                for(int j = 0; j < documentacion.Count(); j++)
                                {
                                    _context.Add(new ADC_Anexo3_DocumentacionResponsable
                                    {
                                        Id_Responsable = integrante.Id_Usuario,
                                        Check = "false",
                                        Estatus = integrante.Estatus,
                                        Id_Anexo3 = model.anexo3.Id,
                                        Id_Documentacion = documentacion[j].Id
                                    });
                                }

                                ViewBag.global = global;//
                                //return Content(JsonConvert.SerializeObject(pm));
                            }
                            await _context.SaveChangesAsync();
                        }

                    }
                    else
                    {
                        for (int i = 0; i < model.miembros.Count(); i++)
                        {
                            if (model.miembros[i].Equals("true"))
                            {
                                ADC_Equipo_Verificador_Integrantes integrante = new ADC_Equipo_Verificador_Integrantes
                                {
                                    Id_Equipo_Verificador_ADC = EV.Id,
                                    Id_Usuario = model.idMiembro[i],
                                    Estatus = "Agregado"
                                };

                                _context.Add(integrante);
                                await _context.SaveChangesAsync();
                            }
                        }

                        model.documentacion = _context.ADC_Anexo3_Documentacion.ToArray(); //new List<Anexo3_Documentacion>();
                        var catalogoDoc = _context.ADC_Anexo3_CatalogoTipoDocumentacion.ToList();

                        List<ADC_Equipo_Verificador_Integrantes> integrantes = _context.ADC_Equipo_Verificador_Integrantes
                            .Where(a => a.Id_Equipo_Verificador_ADC == EV.Id).ToList();
                        int numIntegrantes = integrantes.Count();
                        model.responsables = new ADC_Anexo3_DocumentacionResponsable[numIntegrantes * 22];

                        int i_integrante = 0;
                        for (int i = 1; i <= model.documentacion.Count(); i++)
                        {   
                            for (int j = 0; j < numIntegrantes; j++)
                            {
                                model.responsables[i_integrante] = new ADC_Anexo3_DocumentacionResponsable
                                {
                                    Id_Responsable = integrantes[j].Id_Usuario,
                                    Check = "false",
                                    Estatus = integrantes[j].Estatus,
                                    Id_Anexo3 = model.anexo3.Id,
                                    Id_Documentacion = model.documentacion[i - 1].Id
                                };

                                _context.Add(model.responsables[i_integrante]);

                                i_integrante++;
                            }
                        }
                        await _context.SaveChangesAsync();
                    }


                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == model.anexo3.Id_Anexo1 && a.Id_Actividad == 2).FirstOrDefault();
                    a.Avance = 100;
                    _context.Update(a);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.anexo3.Id))
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

        public async Task<IActionResult> Edit2(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            ViewBag.folio = global.adc.adc.Folio;
            ViewBag.TipoCambio1 = global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{global.adc.adc.Folio[2]}{global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = global.anexo1.residencia;
            ViewBag.liderEV = global.adc.lider;
            ViewBag.clave = "PRO-CEN-UTA-022";

            int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;

            var equipoVerificador = Consultas.VistaEquipoVerificador(_context, idEV);

            ADC_Anexo3Model_EquipoVerificador model = new ADC_Anexo3Model_EquipoVerificador();
            model.anexo3 = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == id).OrderBy(a => a.Id).LastOrDefault();
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            if (model.anexo3.Otro_Elemento.Contains(":"))
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento.Split(":")[0];
                model.OtroElemento = model.anexo3.Otro_Elemento.Split(":")[1];
            }
            else
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento;
            }

            global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Eliminado == 0).ToList();
            for (int i = 0; i < global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(global.vista_usuarios.ElementAt(i).user.Id);

                for (int j = 0; j < equipoVerificador.Count(); j++)
                {
                    if (global.vista_usuarios.ElementAt(i).user.Id == equipoVerificador.ElementAt(j).integrante.Id_Usuario &&
                        equipoVerificador.ElementAt(j).integrante.Estatus.Equals("Agregado"))
                    {
                        model.miembros[i] = "true";
                        break;
                    }
                }
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //ViewBag.global = global;//
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.anexo3.Otro_Elemento = model.OtroElemento_CheckValue.Equals("true") ?
                        $"{model.OtroElemento_CheckValue}:{model.OtroElemento}" : model.OtroElemento_CheckValue;

                    _context.Update(model.anexo3);
                    await _context.SaveChangesAsync();

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == model.anexo3.Id_Anexo1 && a.Id_Actividad == 3).FirstOrDefault();
                    a.Avance = 100;
                    _context.Update(a);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.anexo3.Id))
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

        public async Task<IActionResult> Edit3(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            ViewBag.folio = global.adc.adc.Folio;
            ViewBag.TipoCambio1 = global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{global.adc.adc.Folio[2]}{global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = global.anexo1.residencia;
            ViewBag.liderEV = global.adc.lider;
            ViewBag.clave = "PRO-CEN-UTA-022";

            int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;

            var equipoVerificador = Consultas.VistaEquipoVerificador(_context, idEV);

            ADC_Anexo3Model_EquipoVerificador model = new ADC_Anexo3Model_EquipoVerificador();
            model.anexo3 = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == id).OrderBy(a => a.Id).LastOrDefault();
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            if (model.anexo3.Otro_Elemento.Contains(":"))
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento.Split(":")[0];
                model.OtroElemento = model.anexo3.Otro_Elemento.Split(":")[1];
            }
            else
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento;
            }

            global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Eliminado == 0).ToList();
            for (int i = 0; i < global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(global.vista_usuarios.ElementAt(i).user.Id);

                for (int j = 0; j < equipoVerificador.Count(); j++)
                {
                    if (global.vista_usuarios.ElementAt(i).user.Id == equipoVerificador.ElementAt(j).integrante.Id_Usuario &&
                        equipoVerificador.ElementAt(j).integrante.Estatus.Equals("Agregado"))
                    {
                        model.miembros[i] = "true";
                        break;
                    }
                }
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit3(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //ViewBag.global = global;//
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.anexo3.Otro_Elemento = model.OtroElemento_CheckValue.Equals("true") ?
                        $"{model.OtroElemento_CheckValue}:{model.OtroElemento}" : model.OtroElemento_CheckValue;

                    _context.Update(model.anexo3);
                    await _context.SaveChangesAsync();

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == model.anexo3.Id_Anexo1 && a.Id_Actividad == 4).FirstOrDefault();
                    a.Avance = 100;
                    _context.Update(a);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.anexo3.Id))
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

        public async Task<IActionResult> Edit4(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            ViewBag.folio = global.adc.adc.Folio;
            ViewBag.TipoCambio1 = global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{global.adc.adc.Folio[2]}{global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = global.anexo1.residencia;
            ViewBag.liderEV = global.adc.lider;
            ViewBag.clave = "PRO-CEN-UTA-022";

            int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;

            ADC_Anexo3Model_EquipoVerificador model = new ADC_Anexo3Model_EquipoVerificador();
            model.anexo3 = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == id).OrderBy(a => a.Id).LastOrDefault();

            global.anexo3_CatalogoTipoDocumentacion = _context.ADC_Anexo3_CatalogoTipoDocumentacion.ToList();

            var documentacion = _context.ADC_Anexo3_Documentacion.Where(a => a.Id_Anexo3 == model.anexo3.Id).ToList();
            model.documentacion = new ADC_Anexo3_Documentacion[22];
            for(int i = 0; i < 22; i++)
            {
                model.documentacion[i] = documentacion[i];
            }

            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            if (model.anexo3.Otro_Elemento.Contains(":"))
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento.Split(":")[0];
                model.OtroElemento = model.anexo3.Otro_Elemento.Split(":")[1];
            }
            else
            {
                model.OtroElemento_CheckValue = model.anexo3.Otro_Elemento;
            }

            global.responsablesDocumentacionAnexo3 = Consultas.VistaADCResponsablesDocumentacionAnexo3(
                _context, model.anexo3.Id).ToList();
            ViewBag.numResponsables = _context.ADC_Equipo_Verificador_Integrantes
                .Where(a => a.Estatus.Equals("Agregado")).Count();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));

            ViewBag.global = global;
            return View(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit4(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //ViewBag.global = global;//
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.anexo3.Otro_Elemento = model.OtroElemento_CheckValue.Equals("true") ?
                        $"{model.OtroElemento_CheckValue}:{model.OtroElemento}" : model.OtroElemento_CheckValue;

                    _context.Update(model.anexo3);
                    await _context.SaveChangesAsync();

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == model.anexo3.Id_Anexo1 && a.Id_Actividad == 5).FirstOrDefault();
                    a.Avance = model.anexo3.Justificacion != null && model.anexo3.Descripcion_Cambio != null ? 100 : 0;
                    _context.Update(a);
                    await _context.SaveChangesAsync();

                    var documentacion = _context.ADC_Anexo3_Documentacion.Where(a => a.Id_Anexo3 == model.anexo3.Id).ToList();
                    int i = 0;
                    foreach(var doc in documentacion)
                    {
                        doc.Check = model.documentacion[i].Check;
                        doc.Anotaciones = model.documentacion[i].Anotaciones;
                        doc.Responsable_Area = model.documentacion[i].Responsable_Area;
                        //doc.Id_Responsable = global.session_usuario.user.Id;
                        _context.Update(doc);
                        i++;
                    }
                    
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.anexo3.Id))
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
            return View(model);
        }

        private bool Anexo3Exists(int id)
        {
            ViewBag.global = global;
            return _context.ADC_Anexo3.Any(e => e.Id == id);
        }
    }
}
