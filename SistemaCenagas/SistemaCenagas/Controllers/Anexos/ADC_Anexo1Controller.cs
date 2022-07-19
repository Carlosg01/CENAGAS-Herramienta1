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
    public class ADC_Anexo1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ADC_Anexo1Controller(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
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

        // GET: Anexo1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var anexo1 = await _context.ADC_Anexo1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anexo1 == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return View(anexo1);
        }

        // GET: Anexo1/Create
        public IActionResult Create()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            ViewBag.fecha = DateTime.Now.ToString();
            ViewBag.global = global;
            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ADC_Anexo1 anexo1)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (ModelState.IsValid)
            {
                ADC_Anexo2 anexo2 = new ADC_Anexo2
                {
                    Id_Proyecto = global.proyectos.Id
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
                    Id_Proyecto = global.proyectos.Id,
                    Folio = stringFolio,
                    Id_ProponenteCambio = global.session_usuario.user.Id,
                    Id_LiderEquipoVerificador = 1,
                    Id_ResponsableADC = 1,
                    Id_Suplente = 1,
                    Fecha_Actualizacion = anexo1.Fecha,
                    PreArranque = "No",
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
                global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id == anexo1.Id).FirstOrDefault();

                for (int i = 0; i < global.vista_actividadesADC.Count(); i++)
                {
                    ViewBag.global = global;//
                    //return Content(JsonConvert.SerializeObject(a));
                    ADC_Procesos tarea = new ADC_Procesos
                    {
                        Id_Actividad = global.vista_actividadesADC.ElementAt(i).Id,
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

                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));


                //global.proyectos = global.vista_proyectos.Where(p => p.Id == id).FirstOrDefault();


                ViewBag.global = global;
                return RedirectToAction("Index", "ADC_Procesos");
            }
            ViewBag.global = global;
            return PartialView(anexo1);
        }

        public IActionResult Create2()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            ViewBag.fecha = DateTime.Now.ToString();
            global.lista_proyectos_adc = _context.Proyectos.Where(p => p.Estado_ADC.Equals("Habilitado")).ToList();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(ADC_Anexo1 anexo1)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (ModelState.IsValid)
            {
                ADC_Anexo2 anexo2 = new ADC_Anexo2
                {
                    Id_Proyecto = anexo1.Id_Proyecto
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
                    Id_Proyecto = anexo1.Id_Proyecto,
                    Folio = stringFolio,
                    Id_ProponenteCambio = global.session_usuario.user.Id,
                    Id_LiderEquipoVerificador = 1,
                    Id_ResponsableADC = 1,
                    Id_Suplente = 1,
                    Fecha_Actualizacion = anexo1.Fecha,
                    PreArranque = "No",
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
                catch (Exception ex) { }

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
                global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id == anexo1.Id).FirstOrDefault();

                for (int i = 0; i < global.vista_actividadesADC.Count(); i++)
                {
                    ViewBag.global = global;//
                    //return Content(JsonConvert.SerializeObject(a));
                    ADC_Procesos tarea = new ADC_Procesos
                    {
                        Id_Actividad = global.vista_actividadesADC.ElementAt(i).Id,
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
                
                global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id == anexo1.Id_Proyecto).FirstOrDefault();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "ADC_Procesos");
            }
            ViewBag.global = global;
            return PartialView(anexo1);
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
            var anexo1 = await _context.ADC_Anexo1.Where(
                a => a.Id == global.anexo1.anexo1.Id).FirstOrDefaultAsync();

            global.proyectos = Consultas.VistaProyectos(_context).Where(
                p => p.Id == global.anexo1.anexo1.Id_Proyecto).FirstOrDefault();

            global.tarea = Consultas.VistaTareas(_context)
                .Where(t => t.proceso.Id_ADC == id).FirstOrDefault();

            //ViewBag.
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));

            ViewBag.global = global;
            return PartialView(anexo1);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Anexo1 anexo1)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            
            if (id != anexo1.Id)
            {
                ViewBag.global = global;
                return NotFound();
            }

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

                    ADC adc = _context.ADC.Where(a => a.Id == global.adc.adc.Id).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    adc.Folio = stringFolio;
                    global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    global.adc.adc.Folio = adc.Folio;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();

                    ADC_Procesos a = _context.ADC_Procesos.Where(a => a.Id_ADC == adc.Id).FirstOrDefault();

                    if(global.tarea.proceso.Id_Actividad == 1)
                    {
                        a.Avance = anexo1.Estatus.Equals("Aceptado") || anexo1.Estatus.Equals("Rechazado") ? 100 : 0;
                        /*
                        a.Avance = 9.0f / 12 * 100;
                        a.Avance += (anexo1.Resultados_Analisis != null && anexo1.Resultados_Analisis.Length > 0) ? (1.0f / 12 * 100) : 0;
                        a.Avance += (anexo1.Resultados_Propuesta != null && anexo1.Resultados_Propuesta.Length > 0) ? (1.0f / 12 * 100) : 0;
                        a.Avance += (anexo1.Estatus != null && anexo1.Estatus.Length > 0) ? (1.0f / 12 * 100) : 0;
                        */
                    }
                    
                    _context.Update(a);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(anexo1.Id))
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
            return PartialView(anexo1);
        }

        // GET: Anexo1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var anexo1 = await _context.ADC_Anexo1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anexo1 == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return View(anexo1);
        }

        // POST: Anexo1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var anexo1 = await _context.ADC_Anexo1.FindAsync(id);
            _context.ADC_Anexo1.Remove(anexo1);
            await _context.SaveChangesAsync();
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        private bool Anexo1Exists(int id)
        {
            ViewBag.global = global;
            return _context.ADC_Anexo1.Any(e => e.Id == id);
        }

        [HttpPost]
        public JsonResult getGasoductos(int id_residencia)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            string residencia = global.residencias.Where(r => r.Id == id_residencia)
                .FirstOrDefault().Nombre;

            global.gasoductos = Consultas.getGasoductos(_context, residencia);
            //ViewBag.global = global;//
            //return Json(JsonConvert.SerializeObject(global.gasoductos));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return Json(new SelectList(global.gasoductos, "Ut_Gasoducto", "Gasoducto"));            
        }

        [HttpPost]
        public JsonResult getTramos(string ut_gasoducto)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            global.tramos = Consultas.getTramos(_context, ut_gasoducto);
            //ViewBag.global = global;//
            //return Json(JsonConvert.SerializeObject(global.gasoductos));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return Json(new SelectList(global.tramos, "Ut_Tramo", "Tramo"));
        }
    }
}
