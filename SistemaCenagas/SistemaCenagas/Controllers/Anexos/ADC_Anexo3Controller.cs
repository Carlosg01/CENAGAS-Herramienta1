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
    public class ADC_Anexo3Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADC_Anexo3Controller(ApplicationDbContext context)
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

        // GET: Anexo1/Create
        public IActionResult Create()
        {
            ViewBag.fecha = DateTime.Now.ToString();

            ViewBag.folio = Global.adc.adc.Folio;
            ViewBag.TipoCambio1 = Global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{Global.adc.adc.Folio[2]}{Global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = Global.anexo1.residencia;
            ViewBag.liderEV = Global.adc.lider;

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol == Global.EQUIPO_VERIFICADOR && u.user.Eliminado == 0).ToList();
            ViewBag.clave = "PRO-CEN-UTA-022";
            /*
            Model_ProyectoMiembro model_ProyectoMiembro = new Model_ProyectoMiembro();
            model_ProyectoMiembro.miembros = new List<string>();
            model_ProyectoMiembro.idMiembro = new List<int>();
            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model_ProyectoMiembro.miembros.Add("false");
                model_ProyectoMiembro.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id_Usuario);
            }
            */
            //Documentación


            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ADC_Anexo3Model_EquipoVerificador anexo3_model)
        {
            if (ModelState.IsValid)
            {
                anexo3_model.anexo3.Tipo_ADC = anexo3_model.RadioTipo["tipo"].ToString();
                anexo3_model.anexo3.Otro_Elemento = anexo3_model.OtroElemento_CheckValue.Equals("true") ?
                    $"{anexo3_model.OtroElemento_CheckValue}:{anexo3_model.OtroElemento}" : anexo3_model.OtroElemento_CheckValue;

                //Anexo3 anexo = anexo3.anexo33
                anexo3_model.anexo3.Id_Anexo2 = Global.anexo1.anexo1.Id_Anexo2;
                anexo3_model.anexo3.Id_Responsable_ADC = Global.adc.adc.Id_ResponsableADC;
                anexo3_model.anexo3.Id_Director_Seguridad_Industrial = 1;
                anexo3_model.anexo3.Id_Director_Ejecutivo_Operacion = 1;
                anexo3_model.anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad = 1;
                _context.Add(anexo3_model.anexo3);

                ADC_Equipo_Verificador ev = new ADC_Equipo_Verificador
                {
                    Id_ADC = Global.anexo1.anexo1.Id,
                    Id_LiderEquipoVerificador = Global.adc.adc.Id_LiderEquipoVerificador
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
                        //Check = "false",
                        //Id_Responsable = Global.adc.adc.Id_LiderEquipoVerificador
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


                //_context.Add(anexo2);
                //await _context.SaveChangesAsync();



                return RedirectToAction("Index", "ADC_Procesos");
            }
            return PartialView(anexo3_model);
        }

        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.folio = Global.adc.adc.Folio;
            ViewBag.TipoCambio1 = Global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{Global.adc.adc.Folio[2]}{Global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = Global.anexo1.residencia;
            ViewBag.liderEV = Global.adc.lider;
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

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol == Global.EQUIPO_VERIFICADOR && u.user.Eliminado == 0).ToList();

            if(equipoVerificador != null)
            {
                for (int i = 0; i < Global.vista_usuarios.Count(); i++)
                {
                    model.miembros.Add("false");
                    model.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id);

                    for (int j = 0; j < equipoVerificador.Count(); j++)
                    {
                        if (Global.vista_usuarios.ElementAt(i).user.Id == equipoVerificador.ElementAt(j).integrante.Id_Usuario &&
                            equipoVerificador.ElementAt(j).integrante.Estatus.Equals("Agregado"))
                        {
                            model.miembros[i] = "true";
                            break;
                        }
                    }
                }
            }

            
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
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

        public async Task<IActionResult> Edit2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.folio = Global.adc.adc.Folio;
            ViewBag.TipoCambio1 = Global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{Global.adc.adc.Folio[2]}{Global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = Global.anexo1.residencia;
            ViewBag.liderEV = Global.adc.lider;
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

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol == 6 && u.user.Eliminado == 0).ToList();
            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id);

                for (int j = 0; j < equipoVerificador.Count(); j++)
                {
                    if (Global.vista_usuarios.ElementAt(i).user.Id == equipoVerificador.ElementAt(j).integrante.Id_Usuario &&
                        equipoVerificador.ElementAt(j).integrante.Estatus.Equals("Agregado"))
                    {
                        model.miembros[i] = "true";
                        break;
                    }
                }
            }
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
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

        public async Task<IActionResult> Edit3(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.folio = Global.adc.adc.Folio;
            ViewBag.TipoCambio1 = Global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{Global.adc.adc.Folio[2]}{Global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = Global.anexo1.residencia;
            ViewBag.liderEV = Global.adc.lider;
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

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol == 6 && u.user.Eliminado == 0).ToList();
            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id);

                for (int j = 0; j < equipoVerificador.Count(); j++)
                {
                    if (Global.vista_usuarios.ElementAt(i).user.Id == equipoVerificador.ElementAt(j).integrante.Id_Usuario &&
                        equipoVerificador.ElementAt(j).integrante.Estatus.Equals("Agregado"))
                    {
                        model.miembros[i] = "true";
                        break;
                    }
                }
            }
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit3(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
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

        public async Task<IActionResult> Edit4(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.folio = Global.adc.adc.Folio;
            ViewBag.TipoCambio1 = Global.anexo1.anexo1.Tipo_Cambio;
            ViewBag.TipoCambio2 = $"{Global.adc.adc.Folio[2]}{Global.adc.adc.Folio[3]}";
            ViewBag.TipoCambio2 = ViewBag.TipoCambio2 == "NV" ? "Nuevo" : "Modificado";
            ViewBag.residencia = Global.anexo1.residencia;
            ViewBag.liderEV = Global.adc.lider;
            ViewBag.clave = "PRO-CEN-UTA-022";

            int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;

            ADC_Anexo3Model_EquipoVerificador model = new ADC_Anexo3Model_EquipoVerificador();
            model.anexo3 = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == id).OrderBy(a => a.Id).LastOrDefault();

            Global.anexo3_CatalogoTipoDocumentacion = _context.ADC_Anexo3_CatalogoTipoDocumentacion.ToList();

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

            Global.responsablesDocumentacionAnexo3 = Consultas.VistaADCResponsablesDocumentacionAnexo3(
                _context, model.anexo3.Id).ToList();
            ViewBag.numResponsables = _context.ADC_Equipo_Verificador_Integrantes
                .Where(a => a.Estatus.Equals("Agregado")).Count();
            
            return PartialView(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit4(int id, ADC_Anexo3Model_EquipoVerificador model)
        {
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != model.anexo3.Id_Anexo1)
            {
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
                        //doc.Check = model.documentacion[i].Check;
                        //doc.Id_Responsable = Global.session_usuario.user.Id;
                        _context.Update(doc);
                        i++;
                    }
                    
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo3Exists(model.anexo3.Id))
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
