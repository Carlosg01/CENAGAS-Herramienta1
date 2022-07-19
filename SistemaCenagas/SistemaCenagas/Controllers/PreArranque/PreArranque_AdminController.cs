using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.PowerBI.Api;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using SistemaCenagas.PowerBI_Models;

namespace SistemaCenagas.Controllers
{
    public class PreArranque_AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public PreArranque_AdminController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //global.busqueda = null;
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

            global.vista_prearranque = Consultas.PreArranqueVista(_context);

            if (global.session_usuario.user.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR)
            {
                global.vista_prearranque = global.vista_prearranque.Where(a => a.prearranque.Id_LiderEquipoVerificador == global.session_usuario.user.Id).ToList();
            }
            else if(global.session_usuario.user.Id_Rol == global.RESPONSABLE_ADC)
            {
                global.vista_prearranque = global.vista_prearranque.Where(a => a.prearranque.Id_Responsable == global.session_usuario.user.Id).ToList();
            }
            else if (global.session_usuario.user.Id_Rol == global.SUPLENTE)
            {
                global.vista_prearranque = global.vista_prearranque.Where(a => a.prearranque.Id_Suplente == global.session_usuario.user.Id).ToList();
            }
            else if (global.session_usuario.user.Id_Rol == global.EQUIPO_VERIFICADOR)
            {
                global.vista_prearranque = Consultas.PreArranqueVista_EV(_context);
            }

            //global.resumenADC = Consultas.VistaResumenADC(_context);


            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }


        private static HttpClient client = new HttpClient();
            

        public async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return response;
        }

        public async Task<IActionResult> Resumen()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            if (global.busqueda != null) { 
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return View(global.busqueda);
            }
            else
                global.resumenADC = Consultas.VistaResumenADC(_context);


            //var x = new PowerBIClient
            /*
            var datos_PowerBi = new ResumenADC
            {
                adc = 50,// global.resumenADC.ElementAt(0).avance_ADC,
                pre_arranque = global.resumenADC.ElementAt(0).avance_Pre,
                avance_fisico = global.resumenADC.ElementAt(0).avance_Fisico
            };


            var DatosJson = JsonConvert.SerializeObject(datos_PowerBi);

            string PowerBI_URL = "https://api.powerbi.com/beta/d52c1142-6ef6-4eae-93c5-8a072d168eab/datasets/6baee643-f067-4d38-88c7-ed78f7d0ec91/rows?noSignUpCheck=1&cmpid=pbi-glob-head-snn-signin&key=tyy6IhkMHMeDUuGktl%2BAFvl0ZcW6EjjTNgM0aE8%2FVIsgQgjGQnDoMKdWvYJc46sykctlnjb8sPjuvDtFOXF8ww%3D%3D";
            var PostToPowerBI = HttpPostAsync(PowerBI_URL, "[" + DatosJson + "]");
            */


            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
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
            global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id == global.prearranque.prearranque.Id_Proyecto).FirstOrDefault();

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

            /*global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol <= 4 && u.user.Registro_Eliminado == 0).ToList();
            Model_EquipoVerificadorADC model = new Model_EquipoVerificadorADC();
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();
            for (int i = 0; i < global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(global.vista_usuarios.ElementAt(i).user.Id_Usuario);
            }*/

            /*global.adc.adc = await _context.ADC.FindAsync(id);

            global.equipoVerificador = Consultas.VistaEquipoVerificadorADC(_context, global.adc.adc.Id_ADC);
            

            Model_EquipoVerificadorADC model = new Model_EquipoVerificadorADC();
            model.adc = global.adc.adc;
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol <= 4 && u.user.Registro_Eliminado == 0).ToList();

            

            for (int i = 0; i < global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(global.vista_usuarios.ElementAt(i).user.Id_Usuario);

                for (int j = 0; j < global.equipoVerificador.Count(); j++)
                {
                    if (global.vista_usuarios.ElementAt(i).user.Id_Usuario == global.equipoVerificador.ElementAt(j).ev.Id_Usuario &&
                        global.equipoVerificador.ElementAt(j).ev.Estatus.Equals("Agregado"))
                    {
                        model.miembros[i] = "true";
                        break;
                    }
                }
            }*/

            //global.adc.adc = await _context.ADC.FindAsync(id);

            global.adc = global.vista_adc.Where(a => a.adc.Id == id).FirstOrDefault();

            if (global.adc.adc == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(global.adc.adc);
        }

        // POST: ADC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != model.Id)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Usuarios user;
                    Proyectos proyecto = _context.Proyectos.Find(model.Id_Proyecto);

                    if(model.Id_LiderEquipoVerificador != 1)
                    {
                        //Notificacion por email a proponente de cambio
                        try
                        {
                            user = _context.Usuarios.Find(model.Id_LiderEquipoVerificador);
                            //verificar si el usuario tiene activo las notificaciones de proyecto
                            if (user.Notificacion_ADC.Equals("true"))
                            {
                                string emailText = $"<h1>Proyecto: <b>{proyecto.Nombre}</b></h1>" +
                                $"<p>Se te ha asignado el rol de <b>Lider de ADC</b> para el proyecto <b>{proyecto.Nombre}</b> por parte del administrador</p>" +
                                "<p>Inicia sesión en tu cuenta y revisa tu lista de ADC a cargo para ver los detalles.</p>";

                                ServicioEmail.SendEmailNotification(user, "Líder de ADC", emailText);
                            }

                        }
                        catch (Exception ex) { }
                    }
                    if (model.Id_ResponsableADC != 1)
                    {
                        //Notificacion por email a proponente de cambio
                        try
                        {
                            user = _context.Usuarios.Find(model.Id_ResponsableADC);
                            //verificar si el usuario tiene activo las notificaciones de proyecto
                            if (user.Notificacion_ADC.Equals("true"))
                            {
                                string emailText = $"<h1>Proyecto: <b>{proyecto.Nombre}</b></h1>" +
                                $"<p>Se te ha asignado el rol de <b>Responsable de ADC</b> para el proyecto <b>{proyecto.Nombre}</b> por parte del líder del ADC</p>" +
                                "<p>Inicia sesión en tu cuenta y revisa tu lista de ADC a cargo para ver los detalles.</p>";

                                ServicioEmail.SendEmailNotification(user, "Responsable de ADC", emailText);
                            }

                        }
                        catch (Exception ex) { }
                    }
                    if (model.Id_LiderEquipoVerificador != 1)
                    {
                        //Notificacion por email a proponente de cambio
                        try
                        {
                            user = _context.Usuarios.Find(model.Id_Suplente);
                            //verificar si el usuario tiene activo las notificaciones de proyecto
                            if (user.Notificacion_ADC.Equals("true"))
                            {
                                string emailText = $"<h1>Proyecto: <b>{proyecto.Nombre}</b></h1>" +
                                $"<p>Se te ha asignado el rol de <b>Suplente de ADC</b> para el proyecto <b>{proyecto.Nombre}</b> por parte del líder del ADC</p>" +
                                "<p>Inicia sesión en tu cuenta y revisa tu lista de ADC a cargo para ver los detalles.</p>";

                                ServicioEmail.SendEmailNotification(user, "Suplente de ADC", emailText);
                            }

                        }
                        catch (Exception ex) { }
                    }


                    model.Eliminado = 0;
                    _context.Update(model);
                    await _context.SaveChangesAsync();

                    /*for (int i = 0; i < model.miembros.Count(); i++)
                    {
                        ADC_EquipoVerificador p = _context.ADC_EquipoVerificador
                            .Where(p => p.Id_ADC == model.adc.Id_ADC &&
                                        p.Id_Usuario == model.idMiembro[i]).FirstOrDefault();


                        if (p != null)
                        {
                            p.Id_ADC = model.adc.Id_ADC;
                            p.Id_Usuario = model.idMiembro[i];
                            p.Estatus = (model.miembros[i].Equals("true") ? "Agregado" : "Eliminado");
                            _context.Update(p);
                            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
                            ViewBag.global = global;
                            return Content(JsonConvert.SerializeObject(p));

                        }
                        else if (model.miembros[i].Equals("true"))
                        {
                            ADC_EquipoVerificador pm = new ADC_EquipoVerificador
                            {
                                Id_ADC = model.adc.Id_ADC,
                                Id_Usuario = model.idMiembro[i],
                                Estatus = "Agregado"
                            };
                            _context.Add(pm);
                            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
                            ViewBag.global = global;
                            return Content(JsonConvert.SerializeObject(pm));
                        }
                        await _context.SaveChangesAsync();
                    }*/



                    ADC adc = _context.ADC.Where(a => a.Id == global.adc.adc.Id).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADCExists(model.Id))
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
            return PartialView(model);
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

        [HttpPost]
        public async Task<ActionResult> FileUpload(ADCModel_SubirArchivo uploadFile)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return Content("Filename: " + uploadFile.Archivo.FileName);
            await UploadFile(uploadFile);
            global.SUCCESS_MSJ = "El archivo se subió correctamente!";
            global.panelTareas = "";
            global.panelArchivos = "show active";
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ADC_Procesos");
        }
        // Upload file on server
        public async Task<bool> UploadFile(ADCModel_SubirArchivo upload)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            string path = "";
            bool iscopied = false;
            try
            {
                if (upload.Archivo.Length > 0)
                {

                    ADC_Archivos archivo = new ADC_Archivos
                    {
                        Id_ADC = upload.Id_ADC,
                        Actividad = "1",//upload.Actividad,
                        Id_Usuario = upload.Id_Usuario,
                        Clave = string.Format("[ADC{0}-{1}]_[FILENAME-{2}]{3}", upload.Id_ADC, upload.Actividad, upload.Archivo.FileName, Path.GetExtension(upload.Archivo.FileName)),
                        Nombre = upload.Archivo.FileName,
                        Extension = Path.GetExtension(upload.Archivo.FileName),
                        Size = ( upload.Archivo.Length / 1000000),
                        Ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles"))
                    };

                    _context.ADC_Archivos.Add(archivo);
                    await _context.SaveChangesAsync();


                    //string filename = "ADC-" + global.adc.adc.Id_ADC.ToString() + "_" + upload.Archivo.FileName + Path.GetExtension(upload.Archivo.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles"));
                    using (var filestream = new FileStream(Path.Combine(archivo.Ubicacion, archivo.Clave), FileMode.Create))
                    {
                        await upload.Archivo.CopyToAsync(filestream);
                    }
                    iscopied = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return iscopied;
        }

        [HttpGet]
        public async Task<ActionResult> DownloadFile(int idFile)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            ADC_Archivos archivo = _context.ADC_Archivos.Find(idFile);

            var path = archivo.Ubicacion + "\\" + archivo.Clave;
           
            var memoria = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memoria);
            }
            memoria.Position = 0;
            var ext = archivo.Extension.ToLowerInvariant();

            global.panelTareas = "";
            global.panelArchivos = "show active";

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return File(memoria, GetMimeTypes()[ext], archivo.Nombre);            
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return RedirectToAction("Index", "ADC_Procesos");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteFile(int? idFile)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            ADC_Archivos archivo = _context.ADC_Archivos.Find(idFile);
            _context.ADC_Archivos.Remove(archivo);
            await _context.SaveChangesAsync();
            global.panelTareas = "";
            global.panelArchivos = "show active";
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ADC_Procesos");
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return new Dictionary<string, string>
            {
                {".txt", "text/plain" },
                {".pdf", "application/pdf" },
                {".doc", "application/vnd.ms-word" },
                {".docx", "application/vnd.ms-word" },
                {".xls", "application/vnd.ms-excel" },
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                {".png", "image/png" },
                {".jpg", "image/jpg" },
                {".jpeg", "image/jpeg" },
                {".gif", "image/gif" },
                {".csv", "image/csv" }

            };
        }

        [HttpPost]
        public JsonResult getFiltro(int idBusqueda)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.TipoBusqueda = idBusqueda;
            if (idBusqueda == 1)
            {
                global.vista_proyectos = Consultas.VistaProyectos(_context);
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return Json(new SelectList(global.vista_proyectos, "Id_Proyecto", "Nombre"));
            }
            else if (idBusqueda == 2)
            {
                global.residencias = _context.Residencias.ToList();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return Json(new SelectList(global.residencias, "Id_Residencia", "Nombre"));
            }
            else// if (idBusqueda == 3)
            {
                global.adcs = _context.ADC.ToList();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return Json(new SelectList(global.adcs, "Id_ADC", "Folio"));
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return Json(new SelectList(global.gasoductos, "Id_Residencia", "Nombre"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar(BusquedaReporte model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.busqueda = model;

            if (model.Id == 1)
                global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.id_proyecto == model.Id_Filtro);

            else if (model.Id == 2)
                global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.id_residencia == model.Id_Filtro);

            else if (model.Id == 3)
                global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.id_adc == model.Id_Filtro);

            else if (model.Id == 4)
            {
                global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.avance_ADC >= 100);
            }
            else if (model.Id == 5)
            {
                global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.avance_ADC < 100);
            }
            else if (model.Id == 6)
            {
                global.resumenADC = Consultas.VistaResumenADC(_context);//.Where(a => a.avance_ADC < 100);
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Resumen));
        }

    }
}
