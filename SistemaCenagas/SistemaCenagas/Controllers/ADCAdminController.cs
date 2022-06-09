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
    public class ADCAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADCAdminController(ApplicationDbContext context)
        {
            _context = context;
            //Global.busqueda = null;
        }

        // GET: ADC
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            Global.vista_adc = Consultas.VistaADC(_context);

            if (Global.session_usuario.user.Id_Rol == 5)
            {
                Global.vista_adc = Global.vista_adc.Where(a => a.adc.Id_Lider == Global.session_usuario.user.Id_Usuario).ToList();
            }
            else if(Global.session_usuario.user.Id_Rol == 4)
            {
                Global.vista_adc = Global.vista_adc.Where(a => a.adc.Id_ResponsableADC == Global.session_usuario.user.Id_Usuario).ToList();
            }
            else if (Global.session_usuario.user.Id_Rol == 4)
            {
                Global.vista_adc = Global.vista_adc.Where(a => a.adc.Id_Suplente == Global.session_usuario.user.Id_Usuario).ToList();
            }
            else if (Global.session_usuario.user.Id_Rol == 6)
            {
                Global.vista_adc = Consultas.VistaADC_EV(_context);
            }

            Global.resumenADC = Consultas.VistaResumenADC(_context);


            return View();
        }


        private static HttpClient client = new HttpClient();
            

        public async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<IActionResult> Resumen()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            

            if (Global.busqueda != null)
                return View(Global.busqueda);
            else
                Global.resumenADC = Consultas.VistaResumenADC(_context);


            //var x = new PowerBIClient
            /*
            var datos_PowerBi = new ResumenADC
            {
                adc = 50,// Global.resumenADC.ElementAt(0).avance_ADC,
                pre_arranque = Global.resumenADC.ElementAt(0).avance_Pre,
                avance_fisico = Global.resumenADC.ElementAt(0).avance_Fisico
            };


            var DatosJson = JsonConvert.SerializeObject(datos_PowerBi);

            string PowerBI_URL = "https://api.powerbi.com/beta/d52c1142-6ef6-4eae-93c5-8a072d168eab/datasets/6baee643-f067-4d38-88c7-ed78f7d0ec91/rows?noSignUpCheck=1&cmpid=pbi-glob-head-snn-signin&key=tyy6IhkMHMeDUuGktl%2BAFvl0ZcW6EjjTNgM0aE8%2FVIsgQgjGQnDoMKdWvYJc46sykctlnjb8sPjuvDtFOXF8ww%3D%3D";
            var PostToPowerBI = HttpPostAsync(PowerBI_URL, "[" + DatosJson + "]");
            */


            return View();
        }
        public async Task<IActionResult> Tareas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.adc = Global.vista_adc.Where(a => a.adc.Id_ADC == id).FirstOrDefault();
            Global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id_Proyecto == Global.adc.adc.Id_Proyecto).FirstOrDefault();

            return RedirectToAction("Index", "ADC_Procesos");
        }

        // GET: ADC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC = await _context.ADC
                .FirstOrDefaultAsync(m => m.Id_ADC == id);
            if (aDC == null)
            {
                return NotFound();
            }

            return View(aDC);
        }

        // GET: ADC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ADC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_ADC,Id_Proyecto,Folio,Id_ProponenteCambio,Id_Lider,Id_ResponsableADC,Id_Suplente,Fecha_Actualizacion,Registro_Eliminado")] ADC aDC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aDC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aDC);
        }

        // GET: ADC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol <= 4 && u.user.Registro_Eliminado == 0).ToList();
            Model_EquipoVerificadorADC model = new Model_EquipoVerificadorADC();
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();
            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id_Usuario);
            }*/

            /*Global.adc.adc = await _context.ADC.FindAsync(id);

            Global.equipoVerificador = Consultas.VistaEquipoVerificadorADC(_context, Global.adc.adc.Id_ADC);
            

            Model_EquipoVerificadorADC model = new Model_EquipoVerificadorADC();
            model.adc = Global.adc.adc;
            model.miembros = new List<string>();
            model.idMiembro = new List<int>();

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol <= 4 && u.user.Registro_Eliminado == 0).ToList();

            

            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model.miembros.Add("false");
                model.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id_Usuario);

                for (int j = 0; j < Global.equipoVerificador.Count(); j++)
                {
                    if (Global.vista_usuarios.ElementAt(i).user.Id_Usuario == Global.equipoVerificador.ElementAt(j).ev.Id_Usuario &&
                        Global.equipoVerificador.ElementAt(j).ev.Estatus.Equals("Agregado"))
                    {
                        model.miembros[i] = "true";
                        break;
                    }
                }
            }*/

            //Global.adc.adc = await _context.ADC.FindAsync(id);

            Global.adc = Global.vista_adc.Where(a => a.adc.Id_ADC == id).FirstOrDefault();

            if (Global.adc.adc == null)
            {
                return NotFound();
            }

            return PartialView(Global.adc.adc);
        }

        // POST: ADC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC model)
        {
            if (id != model.Id_ADC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Usuarios user;
                    Proyectos proyecto = _context.Proyectos.Find(model.Id_Proyecto);

                    if(model.Id_Lider != 1)
                    {
                        //Notificacion por email a proponente de cambio
                        try
                        {
                            user = _context.Usuarios.Find(model.Id_Lider);
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
                    if (model.Id_Lider != 1)
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


                    model.Registro_Eliminado = 0;
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
                            //return Content(JsonConvert.SerializeObject(p));

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
                            //return Content(JsonConvert.SerializeObject(pm));
                        }
                        await _context.SaveChangesAsync();
                    }*/



                    ADC adc = _context.ADC.Where(a => a.Id_ADC == Global.adc.adc.Id_ADC).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    Global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADCExists(model.Id_ADC))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView(model);
        }

        // GET: ADC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC = await _context.ADC
                .FirstOrDefaultAsync(m => m.Id_ADC == id);
            if (aDC == null)
            {
                return NotFound();
            }

            return PartialView(aDC);
        }

        // POST: ADC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aDC = await _context.ADC.FindAsync(id);

            aDC.Registro_Eliminado = 1;
            _context.ADC.Update(aDC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADCExists(int id)
        {
            return _context.ADC.Any(e => e.Id_ADC == id);
        }

        [HttpPost]
        public async Task<ActionResult> FileUpload(SubirArchivo uploadFile)
        {
            //return Content("Filename: " + uploadFile.Archivo.FileName);
            await UploadFile(uploadFile);
            Global.SUCCESS_MSJ = "El archivo se subió correctamente!";
            Global.panelTareas = "";
            Global.panelArchivos = "show active";
            return RedirectToAction("Index", "ADC_Procesos");
        }
        // Upload file on server
        public async Task<bool> UploadFile(SubirArchivo upload)
        {
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


                    //string filename = "ADC-" + Global.adc.adc.Id_ADC.ToString() + "_" + upload.Archivo.FileName + Path.GetExtension(upload.Archivo.FileName);
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
            return iscopied;
        }

        [HttpGet]
        public async Task<ActionResult> DownloadFile(int idFile)
        {

            ADC_Archivos archivo = _context.ADC_Archivos.Find(idFile);

            var path = archivo.Ubicacion + "\\" + archivo.Clave;
           
            var memoria = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memoria);
            }
            memoria.Position = 0;
            var ext = archivo.Extension.ToLowerInvariant();

            Global.panelTareas = "";
            Global.panelArchivos = "show active";

            return File(memoria, GetMimeTypes()[ext], archivo.Nombre);            
            //return RedirectToAction("Index", "ADC_Procesos");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteFile(int? idFile)
        {
            ADC_Archivos archivo = _context.ADC_Archivos.Find(idFile);
            _context.ADC_Archivos.Remove(archivo);
            await _context.SaveChangesAsync();
            Global.panelTareas = "";
            Global.panelArchivos = "show active";
            return RedirectToAction("Index", "ADC_Procesos");
        }

        private Dictionary<string, string> GetMimeTypes()
        {
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
            Global.TipoBusqueda = idBusqueda;
            if (idBusqueda == 1)
            {
                Global.vista_proyectos = Consultas.VistaProyectos(_context);
                return Json(new SelectList(Global.vista_proyectos, "Id_Proyecto", "Nombre"));
            }
            else if (idBusqueda == 2)
            {
                Global.residencias = _context.Residencias.ToList();
                return Json(new SelectList(Global.residencias, "Id_Residencia", "Nombre"));
            }
            else// if (idBusqueda == 3)
            {
                Global.adcs = _context.ADC.ToList();
                return Json(new SelectList(Global.adcs, "Id_ADC", "Folio"));
            }

            //return Json(new SelectList(Global.gasoductos, "Id_Residencia", "Nombre"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar(BusquedaReporte model)
        {
            Global.busqueda = model;

            if (model.Id_Busqueda == 1)
                Global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.id_proyecto == model.Id_Filtro);

            else if (model.Id_Busqueda == 2)
                Global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.id_residencia == model.Id_Filtro);

            else if (model.Id_Busqueda == 3)
                Global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.id_adc == model.Id_Filtro);

            else if (model.Id_Busqueda == 4)
            {
                Global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.avance_ADC >= 100);
            }
            else if (model.Id_Busqueda == 5)
            {
                Global.resumenADC = Consultas.VistaResumenADC(_context).Where(a => a.avance_ADC < 100);
            }
            else if (model.Id_Busqueda == 6)
            {
                Global.resumenADC = Consultas.VistaResumenADC(_context);//.Where(a => a.avance_ADC < 100);
            }

            return RedirectToAction(nameof(Resumen));
        }

    }
}
