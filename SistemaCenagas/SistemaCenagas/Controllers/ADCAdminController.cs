using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class ADCAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADCAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ADC
        public async Task<IActionResult> Index()
        {
            Global.vista_adc = Consultas.VistaADC(_context);

            if (Global.session_usuario.user.Id_Rol == 2)
            {
                Global.vista_adc = Global.vista_adc.Where(a => a.adc.Id_Lider == Global.session_usuario.user.Id_Usuario).ToList();
            }
            else if(Global.session_usuario.user.Id_Rol == 3)
            {
                Global.vista_adc = Global.vista_adc.Where(a => a.adc.Id_ResponsableADC == Global.session_usuario.user.Id_Usuario).ToList();
            }
            else if (Global.session_usuario.user.Id_Rol == 4)
            {
                Global.vista_adc = Global.vista_adc.Where(a => a.adc.Id_Suplente == Global.session_usuario.user.Id_Usuario).ToList();
            }

            return View();
        }

        public async Task<IActionResult> Resumen()
        {
            Global.resumenADC = Consultas.VistaResumenADC(_context);
            return View();
        }
        public async Task<IActionResult> Tareas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.adc = Global.vista_adc.Where(a => a.adc.Id_ADC == id).FirstOrDefault();

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

            var aDC = await _context.ADC.FindAsync(id);
            if (aDC == null)
            {
                return NotFound();
            }

            Global.adc = Global.vista_adc.Where(a => a.adc.Id_ADC == id).FirstOrDefault();

            return PartialView(aDC);
        }

        // POST: ADC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC aDC)
        {
            if (id != aDC.Id_ADC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //return Content(JsonConvert.SerializeObject(aDC));
                    _context.Update(aDC);
                    await _context.SaveChangesAsync();

                    ADC adc = _context.ADC.Where(a => a.Id_ADC == Global.adc.adc.Id_ADC).FirstOrDefault();
                    adc.Fecha_Actualizacion = DateTime.Now.ToString();
                    Global.adc.adc.Fecha_Actualizacion = adc.Fecha_Actualizacion;
                    _context.Update(adc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADCExists(aDC.Id_ADC))
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
            return PartialView(aDC);
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
                        Clave = string.Format("[ADC-{0}]_[FILENAME-{1}]{2}", upload.Id_ADC, upload.Archivo.FileName, Path.GetExtension(upload.Archivo.FileName)),
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

    }
}
