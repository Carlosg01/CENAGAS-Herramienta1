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


using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using Microsoft.Office.Interop.Excel;
using iTextSharp.tool.xml.css.parser.state;
using System.Text;
using SelectPdf;
using SistemaCenagas.Reportes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace SistemaCenagas.Controllers
{
    [Authorize]
    public class PreArranque_ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public PreArranque_ProcesosController(ApplicationDbContext context)
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
        // GET: ADC_Procesos
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.panelTareas = "show active";
            global.panelArchivos = "";

            if (!global.session.Equals("LogIn"))
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            global.vista_tareas_prearranque = Consultas.PreArranqueVistaTareas(_context)
                .Where(t => t.proceso.Id_PreArranque == global.prearranque.prearranque.Id).ToList();

            global.anexo2_prearranque = Consultas.PreArranqueVistaAnexo2(_context, global.prearranque.prearranque.Id);
            ViewBag.avance_total = global.vista_tareas_prearranque.Sum(t => t.proceso.Avance);
            //ViewBag.anexo3_action = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == global.adc.adc.Id).ToList().Count == 0 ? "create" : "edit";

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        public async Task<IActionResult> PropuestaCambio()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //global.anexo1 = Consultas.VistaAnexo1(_context, global.adc.adc.Id_ADC);
            //global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == id).FirstOrDefault();

            if (global.anexo1.anexo1 == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Edit", "Anexo1");
        }

        public async Task<IActionResult> Normativas(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.actividadPreArranque = await _context.PreArranque_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);

            if (global.actividadPreArranque == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "PreArranque_Normativas");
        }

        // GET: ADC_Procesos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var prearranque_Procesos = await _context.PreArranque_Procesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prearranque_Procesos == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(prearranque_Procesos);
        }

        public async Task<IActionResult> CrearAnexo3(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            //global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id_Proyecto == id).FirstOrDefault();

            global.anexo1 = Consultas.VistaAnexo1(_context, id);

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return RedirectToAction("Index", "ADCProyecto");
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Create", "Anexo3");
        }

        // GET: ADC_Procesos/Create
        public IActionResult Create()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        // POST: ADC_Procesos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proceso,Id_Actividad,Id_ADC,Avance,Faltante_Comentarios,Plan_Accion,Registro_Eliminado")] ADC_Procesos aDC_Procesos)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Procesos);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(aDC_Procesos);
        }

        // GET: ADC_Procesos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var prearranque_Procesos = await _context.PreArranque_Procesos.FindAsync(id);
            if (prearranque_Procesos == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            global.tarea_prearranque = global.vista_tareas_prearranque
                .Where(t => t.proceso.Id_Actividad == prearranque_Procesos.Id_Actividad).FirstOrDefault();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(prearranque_Procesos);
        }

        // POST: ADC_Procesos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreArranque_Procesos prearranque_Procesos)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != prearranque_Procesos.Id)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    prearranque_Procesos.Avance = prearranque_Procesos.Terminado.Equals("true") && prearranque_Procesos.Confirmado.Equals("true") ? 100 : prearranque_Procesos.Avance;
                    _context.Update(prearranque_Procesos);
                    await _context.SaveChangesAsync();

                    PreArranque pre = _context.PreArranque.Where(a => a.Id == global.prearranque.prearranque.Id).FirstOrDefault();
                    pre.Fecha_Actualizacion = DateTime.Now.ToString();
                    global.prearranque.prearranque.Fecha_Actualizacion = pre.Fecha_Actualizacion;
                    _context.Update(pre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADC_ProcesosExists(prearranque_Procesos.Id))
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
            return View(prearranque_Procesos);
        }

        // GET: ADC_Procesos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Procesos == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(aDC_Procesos);
        }

        //REPORTES
        public async Task<IActionResult> ReporteAnexo1(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            string ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return Ok(ubicacion + "\\Anexo2.xlsx");

            string html = System.IO.File.ReadAllText(ubicacion + "\\Formato_Anexo2_2.html");

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            SelectPdf. PdfDocument doc = htmlToPdf.ConvertHtmlString(html);
            //doc.
            
            byte[] pdf = doc.Save();
            doc.Close();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return File(pdf, "applicaction/pdf", "TEST.pdf");

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return Ok("REPORTE ANEXO2");
        }

        // POST: ADC_Procesos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var aDC_Procesos = await _context.ADC_Procesos.FindAsync(id);
            _context.ADC_Procesos.Remove(aDC_Procesos);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_ProcesosExists(int id)
        {
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return _context.ADC_Procesos.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Terminado(string check)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return Ok(check);
        }

        public async Task<IActionResult> Archivos(int? Id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (Id == null)
            {
                //HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                //ViewBag.global = global;
                return NotFound();
            }

            global.proceso_prearranque = _context.PreArranque_Procesos.Find(Id);
            global.tarea_prearranque = Consultas.PreArranqueVistaTareas(_context).Where(a => a.proceso.Id == Id).FirstOrDefault();
            global.vista_archivos_prearranque = Consultas.PreArranqueVistaArchivos(_context, global.proceso_prearranque.Id);

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FileUpload(PreArranqueModel_SubirArchivo uploadFile)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            //return Content("Filename: " + uploadFile.Archivo.FileName);
            await UploadFile(uploadFile);
            global.SUCCESS_MSJ = "El archivo se subió correctamente!";
            global.panelTareas = "";
            global.panelArchivos = "show active";
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "PreArranque_Procesos");
        }
        // Upload file on server
        public async Task<bool> UploadFile(PreArranqueModel_SubirArchivo upload)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            string path = "";
            bool iscopied = false;
            try
            {
                if (upload.Archivo.Length > 0)
                {

                    PreArranque_Archivos archivo = new PreArranque_Archivos
                    {
                        Id_PreArranque = upload.Id_PreArranque,
                        Id_Proceso = upload.Id_Proceso,
                        Actividad = upload.Actividad,
                        Id_Usuario = upload.Id_Usuario,
                        Clave = string.Format("[PreArranque{0}-{1}]_[FILENAME-{2}]{3}", upload.Id_PreArranque, upload.Actividad, upload.Archivo.FileName, Path.GetExtension(upload.Archivo.FileName)),
                        Nombre = upload.Archivo.FileName,
                        Extension = Path.GetExtension(upload.Archivo.FileName),
                        Size = (upload.Archivo.Length / 1000000),
                        Ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles"))
                    };

                    _context.PreArranque_Archivos.Add(archivo);
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

            PreArranque_Archivos archivo = _context.PreArranque_Archivos.Find(idFile);

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
            PreArranque_Archivos archivo = _context.PreArranque_Archivos.Find(idFile);
            _context.PreArranque_Archivos.Remove(archivo);
            await _context.SaveChangesAsync();
            global.panelTareas = "";
            global.panelArchivos = "show active";
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "PreArranque_Procesos");
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
    }
}
