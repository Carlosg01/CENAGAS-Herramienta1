using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.util;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SelectPdf;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using SistemaCenagas.Reportes;
using Windows.UI.Xaml.Media.Imaging;

namespace SistemaCenagas.Controllers
{
    [Authorize]
    public class ProyectoAnexosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;
        
        public ProyectoAnexosController(ApplicationDbContext context)
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
        // GET: ADC_Actividades
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            global.anexos = _context.ADC_Anexos.ToList();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        public async Task<IActionResult> Anexo1(int? idProyecto)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ReporteProyectoAnexo1");
        }

        public async Task<IActionResult> Anexo2()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            ReporteAnexos reporte = new ReporteAnexos(_context, global);
            byte[] pdf = reporte.Anexo2_PDF(global.proyectos);
            //reporte.Anexo2_PDF(global.proyectos);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            //return RedirectToAction(nameof(Index));
            return File(pdf, "application/pdf", $"Anexo 2 - {global.proyectos.Nombre}.pdf");
        }

        public async Task<IActionResult> PDF_Viewer()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            global.resumenADC = Consultas.VistaResumenADC(_context).Where(r => r.id_proyecto == global.proyectos.Id);

            var model = (from p in _context.Proyectos
                         join adc in _context.ADC on p.Id equals adc.Id_Proyecto
                         join a1 in _context.ADC_Anexo1 on adc.Id equals a1.Id
                         join res in _context.Usuarios on adc.Id_ResponsableADC equals res.Id
                         join lider in _context.Usuarios on adc.Id_LiderEquipoVerificador equals lider.Id
                         join r in _context.Residencias on a1.Id_Residencia equals r.Id
                         //join resumen in global.resumenADC on p.Id_Proyecto equals resumen.id_proyecto
                         where p.Id == global.proyectos.Id
                         select new Anexo2_Registro
                         {
                             NoFolio = adc.Folio,
                             tipoCambio1 = a1.Tipo_Cambio,
                             tipoCambio2 = adc.Folio.Contains("-NV-") ? "Nuevo" : "Modificado",
                             lugar = r.Nombre,
                             descripcion = a1.Descripcion,
                             estatus = a1.Estatus,
                             fechaRegistro = a1.Fecha,
                             fechaCierre = adc.Fecha_Actualizacion,
                             responsable = $"{res.Nombre} {res.Paterno}",
                             lider = $"{lider.Nombre} {lider.Paterno}",
                             estatusADC = "En elaboración",
                             //estatusADC = resumen.avance_ADC >= 100 || a1.Estatus.Equals("Rechazado") ? "Concluida" : "En elaboración",
                             presentoARP = "N/A"

                         }).ToList();


            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(model);
        }

        public async Task<IActionResult> Anexo3(int? idProyecto)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ReporteProyectoAnexo3");
        }
        public async Task<IActionResult> Anexo4(int? idProyecto)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ReporteProyectoAnexo4");
        }
        public async Task<IActionResult> Anexo5(int? idProyecto)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ReporteProyectoAnexo5");
        }
        public async Task<IActionResult> Anexo6(int? idProyecto)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction("Index", "ReporteProyectoAnexo6");
        }


    }
}
