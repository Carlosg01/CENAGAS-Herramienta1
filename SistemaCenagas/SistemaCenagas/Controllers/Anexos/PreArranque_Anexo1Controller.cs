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
    public class PreArranque_Anexo1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        public PreArranque_Anexo1Controller(ApplicationDbContext context)
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
            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ADC_Anexo1 anexo1)
        {
            if (ModelState.IsValid)
            {
                ADC_Anexo2 anexo2 = new ADC_Anexo2
                {
                    Id_Proyecto = Global.proyectos.Id
                };

                _context.Add(anexo2);
                await _context.SaveChangesAsync();

                anexo1.Estatus = "Pendiente";
                anexo1.Id_Anexo2 = anexo2.Id;
                _context.Add(anexo1);
                await _context.SaveChangesAsync();

                string x = anexo1.Tipo_Cambio.ToUpper()[0].ToString();
                string xx = "NV";
                string xxx = (anexo1.Id % 10 == 0) ? "0" + anexo1.Id.ToString() : "00" + anexo1.Id.ToString();
                string xxxx = DateTime.Now.Year.ToString();

                string stringFolio = $"{x}-{xx}-{xxx}-{xxxx}";

                ADC adc = new ADC
                {
                    Id = anexo1.Id,
                    Id_Proyecto = Global.proyectos.Id,
                    Folio = stringFolio,
                    Id_ProponenteCambio = Global.session_usuario.user.Id,
                    Id_LiderEquipoVerificador = 1,
                    Id_ResponsableADC = 1,
                    Id_Suplente = 1,
                    Fecha_Actualizacion = anexo1.Fecha,
                    Eliminado = 0
                };

                Proyectos proyecto = _context.Proyectos.Find(adc.Id_Proyecto);
                Usuarios user = _context.Usuarios.Find(adc.Id_ProponenteCambio);

                //Notificacion por email a proponente de cambio
                try
                {                       
                    //verificar si el usuario tiene activo las notificaciones de proyecto
                    if (user.Notificacion_ADC.Equals("true"))
                    {
                        string emailText = $"<h1>Proyecto: <b>{proyecto.Nombre}</b></h1>" +
                        $"<p>Haz realizado una solicitud de cambio para el proyecto <b>{proyecto.Nombre}</b></p>" +
                        "<p>Inicia sesión en tu cuenta y revisa tu lista de propuestas de cambio para ver los detalles.</p>";

                        ServicioEmail.SendEmailNotification(user, "Proponente de cambio", emailText);
                    }

                }
                catch (Exception ex){}

                //Notificacion por email a los administradores
                IEnumerable<Usuarios> administradores = _context.Usuarios.Where(a => a.Id_Rol >= 5).ToList();
                foreach (var admin in administradores)
                {
                    try
                    {
                        if (admin.Notificacion_ADC.Equals("true"))
                        {
                            string emailText = $"<h1>Proyecto: <b>{proyecto.Nombre}</b></h1>" +
                            $"<p>Se ha realizado una nueva solicitud de cambio para el proyecto <b>{proyecto.Nombre}</b> por el siguiente usuario:</p><hr>" +
                            $"<p>Nombre: {user.Nombre} {user.Paterno} {user.Materno}</p><hr>" +
                            $"<p>Nombre de usuario: {user.Username}</p><hr>" +
                            $"<p>Email: {user.Email}</p><hr><hr>" +
                            "<p>Inicia sesión en tu cuenta y revisa tu lista de propuestas de cambio para ver los detalles.</p>";

                            ServicioEmail.SendEmailNotification(admin, "Nueva solicitud de cambio", emailText);
                        }
                    }
                    catch (Exception ex) { }
                    
                }

                _context.Add(adc);

                await _context.SaveChangesAsync();

                //int id = _context.ADC.OrderByDescending(a => a.Id_ADC).FirstOrDefault().Id_ADC;
                Global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id == anexo1.Id).FirstOrDefault();

                for (int i = 0; i < Global.vista_actividadesADC.Count(); i++)
                {
                    //return Content(JsonConvert.SerializeObject(a));
                    ADC_Procesos tarea = new ADC_Procesos
                    {
                        Id_Actividad = Global.vista_actividadesADC.ElementAt(i).Id,
                        Id_ADC = anexo1.Id,
                        Avance = 0,//(i == 0) ? (9.0f/12)*100 : 0, //primeros 9 atributos necesarios por primera vez de 12 posibles
                        Faltante_Comentarios = "N/A",
                        Plan_Accion = "N/A",
                        Terminado = "false",
                        Confirmado = "false"

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
            ViewBag.liderEV = Global.prearranque.lider;

            int idEV = _context.PreArranque_Equipo_Verificador.Where(e => e.Id_PreArranque == id).FirstOrDefault().Id;

            var equipoVerificador = Consultas.PreArranqueVistaEquipoVerificador(_context, idEV);


            PreArranque_Anexo1_Model model = new PreArranque_Anexo1_Model();
            model.anexo1 = _context.PreArranque_Anexo1.Where(a => a.Id_Prearranque == id).OrderBy(a => a.Id).LastOrDefault();
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol == Global.EQUIPO_VERIFICADOR && u.user.Eliminado == 0).ToList();

            if (equipoVerificador != null)
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
        public async Task<IActionResult> Edit(int id, PreArranque_Anexo1_Model model)
        {
            //return Content(""+id);
            if (id != model.anexo1.Id_Prearranque)
            {
                return NotFound();
            }


            //return Content(JsonConvert.SerializeObject(anexo1));

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(model.anexo1);
                    //await _context.SaveChangesAsync();

                    //int idEV = _context.ADC_Equipo_Verificador.Where(e => e.Id_ADC == id).FirstOrDefault().Id;
                    var EV = _context.PreArranque_Equipo_Verificador.Where(e => e.Id_PreArranque == id).FirstOrDefault();

                    //var equipoVerificador = Consultas.VistaEquipoVerificador(_context, idEV);

                    if (Consultas.PreArranqueVistaEquipoVerificador(_context, EV.Id).Count() > 0)
                    {
                        for (int i = 0; i < model.miembros.Count(); i++)
                        {
                            PreArranque_Equipo_Verificador_Integrantes integrante = _context.PreArranque_Equipo_Verificador_Integrantes
                                .Where(e => e.Id_Equipo_Verificador_PreArranque == EV.Id && e.Id_Usuario == model.idMiembro[i]).FirstOrDefault();


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
                                integrante = new PreArranque_Equipo_Verificador_Integrantes
                                {
                                    Id_Equipo_Verificador_PreArranque = EV.Id,
                                    Id_Usuario = model.idMiembro[i],
                                    Estatus = "Agregado"
                                };
                                _context.Add(integrante);

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
                                PreArranque_Equipo_Verificador_Integrantes integrante = new PreArranque_Equipo_Verificador_Integrantes
                                {
                                    Id_Equipo_Verificador_PreArranque = EV.Id,
                                    Id_Usuario = model.idMiembro[i],
                                    Estatus = "Agregado"
                                };

                                _context.Add(integrante);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }

                    Global.prearranque.prearranque.Fecha_Actualizacion = DateTime.Now.ToString();
                    _context.Update(Global.prearranque.prearranque);


                    PreArranque_Procesos a = _context.PreArranque_Procesos.Where(a => a.Id_PreArranque == model.anexo1.Id_Prearranque && a.Id_Actividad == 1).FirstOrDefault();
                    a.Avance = 100;
                    _context.Update(a);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(model.anexo1.Id))
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
            return PartialView(model);
        }

        public async Task<IActionResult> Edit2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int ROWS_ACCIONES = 10;
            int ROWS_ACTIVIDADES = 10;

            PreArranque_Anexo1_Model model = new PreArranque_Anexo1_Model();
            model.anexo1 = _context.PreArranque_Anexo1.Where(a => a.Id_Prearranque == id).OrderBy(a => a.Id).LastOrDefault();
            model.Proyecto = (from pa1 in _context.PreArranque_Anexo1
                              join p in _context.PreArranque on pa1.Id_Prearranque equals p.Id
                              join pro in _context.Proyectos on p.Id_Proyecto equals pro.Id
                              select pro.Nombre).FirstOrDefault();


            var _actividades_anexo1 = _context.PreArranque_Anexo1_Actividades
                .Where(a => a.Id_Anexo1 == model.anexo1.Id).ToList();
            var existen_actividades = _actividades_anexo1.Where(a => a.Id_Anexo1 == model.anexo1.Id).Count() > 0;
            model.actividadesModel = new List<PreArranque_Anexo1_Avtividades_Model>();

            if (!existen_actividades)
            {
                
                for (int i = 0; i < ROWS_ACCIONES; i++)
                {
                    var _accion = new PreArranque_Anexo1_Actividades
                    {
                        Id_Anexo1 = model.anexo1.Id
                    };
                    var _actividades = new List<PreArranque_Anexo1_Actividades_Acciones>();

                    for (int j = 0; j < ROWS_ACTIVIDADES; j++)
                    {
                        _actividades.Add(new PreArranque_Anexo1_Actividades_Acciones());
                    }

                    model.actividadesModel.Add(new PreArranque_Anexo1_Avtividades_Model
                    {
                        accion = _accion,
                        actividaes = _actividades
                    });
                }
            }
            else
            {
                
                foreach(var acciones in _actividades_anexo1)
                {
                    var act = _context.PreArranque_Anexo1_Actividades_Acciones
                        .Where(a => a.Id_Anexo1_Actividades == acciones.Id).ToList();

                    var act_ = act;

                    var _size = act.Count();

                    for (int i = 0; i < ROWS_ACTIVIDADES - _size ; i++)
                    {
                        act.Add(new PreArranque_Anexo1_Actividades_Acciones());
                    }

                    model.actividadesModel.Add(new PreArranque_Anexo1_Avtividades_Model
                    {
                        accion = acciones,
                        actividaes = act,
                        Num_actividades_actuales = _size

                    });
                }

                var size_acciones = model.actividadesModel.Count();

                for (int i = 0; i < ROWS_ACCIONES - size_acciones; i++)
                {
                    var _accion = new PreArranque_Anexo1_Actividades
                    {
                        Id_Anexo1 = model.anexo1.Id
                    };
                    var _actividades = new List<PreArranque_Anexo1_Actividades_Acciones>();

                    for (int j = 0; j < ROWS_ACTIVIDADES; j++)
                    {
                        _actividades.Add(new PreArranque_Anexo1_Actividades_Acciones());
                    }

                    model.actividadesModel.Add(new PreArranque_Anexo1_Avtividades_Model
                    {
                        accion = _accion,
                        actividaes = _actividades
                    });
                }
            }
            //var IdEV = _context.PreArranque_Equipo_Verificador
              //  .Where(a => a.Id_PreArranque == model.anexo1.Id_Prearranque).FirstOrDefault().Id;

            //Global.equipoVerificador_PreArranque = Consultas.PreArranqueVistaEquipoVerificador(_context, IdEV).ToList();

            return View(model);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, PreArranque_Anexo1_Model model)
        {
            if (id != model.anexo1.Id_Prearranque)
            {
                return NotFound();
            }

            try
            {
                var acciones_actuales = _context.PreArranque_Anexo1_Actividades
                    .Where(a => a.Id_Anexo1 == model.anexo1.Id).ToList();

                foreach (var acciones in model.actividadesModel)
                {
                    bool existe_accion = false;
                    if (acciones.accion.Accion_Descriptiva != null && acciones.accion.Accion_Descriptiva.Length > 0 &&
                        acciones.accion.Responsable != null && acciones.accion.Responsable.Length > 0)
                    {
                        existe_accion = _context.PreArranque_Anexo1_Actividades
                        .Where(a => a.Id_Anexo1 == acciones.accion.Id_Anexo1 &&
                                    a.Accion_Descriptiva.Equals(acciones.accion.Accion_Descriptiva)).Count() > 0;
                    }
                    

                    if (!existe_accion && acciones.accion.Id == 0 && acciones.accion.Accion_Descriptiva != null && acciones.accion.Accion_Descriptiva.Length > 0)
                    {
                        _context.Add(acciones.accion);
                    }
                    else
                    {
                        var local = _context.Set<PreArranque_Anexo1_Actividades>()
                            .Local.FirstOrDefault(e => e.Id == acciones.accion.Id);
                        if (local != null)
                        {
                            _context.Entry(local).State = EntityState.Detached;
                        }

                        if (acciones.accion.Accion_Descriptiva != null && acciones.accion.Accion_Descriptiva.Length > 0)
                        {
                            //modificar accion
                            _context.Entry(acciones.accion).State = EntityState.Modified;
                        }
                        else
                        {
                            if(acciones.accion.Id != 0)
                            {
                                //primero se eliminan las actividades de dicha accion
                                foreach (var items in acciones.actividaes)
                                {
                                    var exist = _context.PreArranque_Anexo1_Actividades_Acciones
                                        .Where(a => a.Id == items.Id).Count() > 0;
                                    if (exist)
                                    {
                                        var local2 = _context.Set<PreArranque_Anexo1_Actividades_Acciones>()
                                            .Local.FirstOrDefault(e => e.Id == items.Id);
                                        if (local2 != null)
                                        {
                                            _context.Entry(local2).State = EntityState.Detached;
                                        }
                                        _context.Entry(items).State = EntityState.Deleted;
                                        await _context.SaveChangesAsync();
                                        items.Id = 0;
                                    }

                                }

                                //eliminar accion
                                _context.Entry(acciones.accion).State = EntityState.Deleted;
                            }
                        }


                        //_context.Update(acciones.accion);
                    }


                    await _context.SaveChangesAsync();

                    
                    foreach (var actividades in acciones.actividaes)
                    {
                        bool existe_actividad = false;
                        if (actividades.Actividad != null && actividades.Actividad.Length > 0)
                        {
                            existe_actividad = _context.PreArranque_Anexo1_Actividades_Acciones
                                .Where(a => a.Id == actividades.Id &&
                                            a.Actividad.Equals(actividades.Actividad)).Count() > 0;
                        }
                        

                        actividades.Id_Anexo1_Actividades = acciones.accion.Id;

                        if (!existe_actividad && actividades.Id == 0 && actividades.Actividad != null && actividades.Actividad.Length > 0)
                        {
                            _context.Add(actividades);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var local = _context.Set<PreArranque_Anexo1_Actividades_Acciones>()
                                    .Local.FirstOrDefault(e => e.Id == actividades.Id);
                            if (local != null)
                            {
                                _context.Entry(local).State = EntityState.Detached;
                            }

                            if (actividades.Actividad != null && actividades.Actividad.Length > 0)
                            {
                                _context.Entry(actividades).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                if (actividades.Id != 0)
                                {
                                    var local2 = _context.Set<PreArranque_Anexo1_Actividades_Acciones>()
                                    .Local.FirstOrDefault(e => e.Id == actividades.Id);
                                    if (local2 != null)
                                    {
                                        _context.Entry(local2).State = EntityState.Detached;
                                    }
                                    _context.Entry(actividades).State = EntityState.Deleted;
                                    await _context.SaveChangesAsync();
                                }

                            }
                            
                                
                        }
                    }

                }

                Global.prearranque.prearranque.Fecha_Actualizacion = DateTime.Now.ToString();
                _context.Update(Global.prearranque.prearranque);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Anexo1Exists(model.anexo1.Id))
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
            return _context.PreArranque_Anexo1.Any(e => e.Id == id);
        }


        [HttpPost]
        public void addAccion(string accionn, string responsable)
        {

            var model = new PreArranque_Anexo1_Avtividades_Model
            {
                accion = new PreArranque_Anexo1_Actividades
                {
                    Accion_Descriptiva = accionn,
                    Responsable = responsable
                },
                actividaes = new List<PreArranque_Anexo1_Actividades_Acciones>()


            };
            Global.modelActividades.Add(model);

            //return Json(model.accion);
        }
        

        
    }
}
