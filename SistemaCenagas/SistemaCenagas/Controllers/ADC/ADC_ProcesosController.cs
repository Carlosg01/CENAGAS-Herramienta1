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

namespace SistemaCenagas.Controllers
{
    public class ADC_ProcesosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADC_ProcesosController(ApplicationDbContext context)
        {
            _context = context;
            Global.panelTareas = "show active";
            Global.panelArchivos = "";

        }

        // GET: ADC_Procesos
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            Global.vista_tareas = Consultas.VistaTareas(_context)
                .Where(t => t.proceso.Id_ADC == Global.adc.adc.Id).ToList();
            Global.anexo1 = Consultas.VistaAnexo1(_context, Global.adc.adc.Id);
            ViewBag.avance_total = Global.vista_tareas.Sum(t => t.proceso.Avance);
            ViewBag.anexo3_action = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == Global.adc.adc.Id).ToList().Count == 0 ? "create" : "edit";

            return View();
        }

        public async Task<IActionResult> PropuestaCambio()
        {

            //Global.anexo1 = Consultas.VistaAnexo1(_context, Global.adc.adc.Id_ADC);
            //Global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == id).FirstOrDefault();

            if (Global.anexo1.anexo1 == null)
            {
                return NotFound();
            }

            return RedirectToAction("Edit", "Anexo1");
        }

        public async Task<IActionResult> Normativas(int? id)
        {
            Global.actividadADC = await _context.ADC_Actividades
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Global.actividadADC == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "ADC_Normativas");
        }

        // GET: ADC_Procesos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Procesos == null)
            {
                return NotFound();
            }

            return View(aDC_Procesos);
        }

        public async Task<IActionResult> CrearAnexo3(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id_Proyecto == id).FirstOrDefault();

            Global.anexo1 = Consultas.VistaAnexo1(_context, id);

            //return RedirectToAction("Index", "ADCProyecto");
            return RedirectToAction("Create", "Anexo3");
        }

        // GET: ADC_Procesos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ADC_Procesos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proceso,Id_Actividad,Id_ADC,Avance,Faltante_Comentarios,Plan_Accion,Registro_Eliminado")] ADC_Procesos aDC_Procesos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aDC_Procesos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aDC_Procesos);
        }

        // GET: ADC_Procesos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos.FindAsync(id);
            if (aDC_Procesos == null)
            {
                return NotFound();
            }

            Global.tarea = Global.vista_tareas
                .Where(t => t.proceso.Id_Actividad == aDC_Procesos.Id_Actividad).FirstOrDefault();

            return View(aDC_Procesos);
        }

        // POST: ADC_Procesos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC_Procesos aDC_Procesos)
        {
            if (id != aDC_Procesos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    aDC_Procesos.Avance = aDC_Procesos.Terminado.Equals("true") && aDC_Procesos.Confirmado.Equals("true") ? 100 : aDC_Procesos.Avance;
                    _context.Update(aDC_Procesos);
                    await _context.SaveChangesAsync();

                    ADC adc = _context.ADC.Where(a => a.Id == Global.adc.adc.Id).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    Global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADC_ProcesosExists(aDC_Procesos.Id))
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
            return View(aDC_Procesos);
        }

        // GET: ADC_Procesos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC_Procesos = await _context.ADC_Procesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aDC_Procesos == null)
            {
                return NotFound();
            }

            return View(aDC_Procesos);
        }

        //REPORTES
        public async Task<IActionResult> ReporteAnexo1(int? id)
        {

            string ubicacion = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Plantillas"));
            //return Ok(ubicacion + "\\Anexo2.xlsx");

            string html = System.IO.File.ReadAllText(ubicacion + "\\Formato_Anexo2_2.html");

            HtmlToPdf htmlToPdf = new HtmlToPdf();
            SelectPdf. PdfDocument doc = htmlToPdf.ConvertHtmlString(html);
            //doc.
            
            byte[] pdf = doc.Save();
            doc.Close();

            return File(pdf, "applicaction/pdf", "TEST.pdf");

            //return Ok("REPORTE ANEXO2");
        }

        // POST: ADC_Procesos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aDC_Procesos = await _context.ADC_Procesos.FindAsync(id);
            _context.ADC_Procesos.Remove(aDC_Procesos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADC_ProcesosExists(int id)
        {
            return _context.ADC_Procesos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Anexo1_Descargar(int idADC)
        {
            ReporteAnexos reporte = new ReporteAnexos(_context);
            byte[] pdf = reporte.Anexo1_PDF(Global.proyectos, idADC);
            return File(pdf, "application/pdf", $"Anexo 1 - {Global.proyectos.Nombre}.pdf");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Terminado(string check)
        {
            return Ok(check);
        }

        public async Task<IActionResult> Archivos(int? Id)
        {
            if (Id == null)
                return NotFound();

            Global.proceso = _context.ADC_Procesos.Find(Id);
            Global.tarea = Consultas.VistaTareas(_context).Where(a => a.proceso.Id == Id).FirstOrDefault();
            Global.vista_archivos = Consultas.VistaArchivosADC(_context, Global.proceso.Id);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FileUpload(ADCModel_SubirArchivo uploadFile)
        {
            //return Content("Filename: " + uploadFile.Archivo.FileName);
            await UploadFile(uploadFile);
            Global.SUCCESS_MSJ = "El archivo se subió correctamente!";
            Global.panelTareas = "";
            Global.panelArchivos = "show active";
            return RedirectToAction("Index", "ADC_Procesos");
        }
        // Upload file on server
        public async Task<bool> UploadFile(ADCModel_SubirArchivo upload)
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
                        Id_Proceso = upload.Id_Proceso,
                        Actividad = upload.Actividad,
                        Id_Usuario = upload.Id_Usuario,
                        Clave = string.Format("[ADC{0}-{1}]_[FILENAME-{2}]{3}", upload.Id_ADC, upload.Actividad, upload.Archivo.FileName, Path.GetExtension(upload.Archivo.FileName)),
                        Nombre = upload.Archivo.FileName,
                        Extension = Path.GetExtension(upload.Archivo.FileName),
                        Size = (upload.Archivo.Length / 1000000),
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
    }
}
