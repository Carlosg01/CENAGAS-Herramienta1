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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using SistemaCenagas.Reportes;
using Windows.UI.Xaml.Media.Imaging;

namespace SistemaCenagas.Controllers
{
    public class ProyectoAnexosController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ProyectoAnexosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ADC_Actividades
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            Global.anexos = _context.Anexos.Where(a => a.Id_Anexo <= 3).ToList();
            return View();
        }

        public async Task<IActionResult> Anexo1(int? idProyecto)
        {
            return RedirectToAction("Index", "ReporteProyectoAnexo1");
        }

        public async Task<IActionResult> Anexo2()
        {
            ReporteAnexos reporte = new ReporteAnexos(_context);
            byte[] pdf = reporte.Anexo2_PDF(Global.proyectos);
            return File(pdf, "application/pdf", $"Anexo 2 - {Global.proyectos.Nombre}.pdf");
        }

        public async Task<IActionResult> Anexo3(int? idProyecto)
        {
            return RedirectToAction("Index", "ReporteProyectoAnexo3");
        }


    }
}
