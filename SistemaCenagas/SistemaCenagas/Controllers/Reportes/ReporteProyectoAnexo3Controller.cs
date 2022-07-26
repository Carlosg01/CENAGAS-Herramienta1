using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using SistemaCenagas.Reportes;
using System.Web;
using Microsoft.JSInterop;

namespace SistemaCenagas.Controllers
{
    public class ReporteProyectoAnexo3Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ReporteProyectoAnexo3Controller(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
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

            global.vista_adc = Consultas.VistaADC(_context).Where(a => a.id_proyecto == global.proyectos.Id);

            //Vista adc propuestas
            global.vista_adc_propuestas = global.vista_adc
                .Where(a => a.adc.Id_ProponenteCambio == global.session_usuario.user.Id).ToList();

            //Vista adc a cargo
            if (global.session_usuario.user.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR)
            {
                global.vista_adc_cargo = global.vista_adc
                    .Where(a => a.adc.Id_LiderEquipoVerificador == global.session_usuario.user.Id).ToList();
            }
            else if(global.session_usuario.user.Id_Rol == global.RESPONSABLE_ADC)
            {
                global.vista_adc_cargo = global.vista_adc
                    .Where(a => a.adc.Id_ResponsableADC == global.session_usuario.user.Id).ToList();
            }
            else if (global.session_usuario.user.Id_Rol == global.SUPLENTE)
            {
                global.vista_adc_cargo = global.vista_adc
                    .Where(a => a.adc.Id_Suplente == global.session_usuario.user.Id).ToList();
            }

            global.resumenADC = Consultas.VistaResumenADC(_context);

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }
        public async Task<IActionResult> Descargar(int idADC)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            int id_anexo3 = _context.ADC_Anexo3.Where(a => a.Id_Anexo1 == idADC).OrderBy(a=>a.Id).LastOrDefault().Id;

            ReporteAnexos reporte = new ReporteAnexos(_context, global);
            byte[] pdf = reporte.Anexo3_PDF(id_anexo3);
            //var dir = reporte.Anexo3_PDF(id_anexo3);
            //reporte.Anexo3_PDF(id_anexo3);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            //return RedirectToAction(nameof(PDF));
            //return Redirect(dir);
            return File(pdf, "application/pdf", $"Anexo 3 - {global.proyectos.Nombre}.pdf");
            //return Ok();
            //return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PDF()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            //global.VISTA_PDF = true;

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;

            //await js.InvokeVoidAsync("setTimeout(function(){window.print()}, 3000);");
            return View();

        }



    }
}
