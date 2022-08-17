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
using Microsoft.AspNetCore.Authorization;

namespace SistemaCenagas.Controllers
{
    [Authorize]
    public class ReporteProyectoAnexo4Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ReporteProyectoAnexo4Controller(ApplicationDbContext context)
        {
            _context = context;
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

            int id_anexo4 = _context.ADC_Anexo4.Where(a => a.Id_Anexo1 == idADC).OrderBy(a=>a.Id).LastOrDefault().Id;

            ReporteAnexos reporte = new ReporteAnexos(_context, global);
            byte[] pdf = reporte.Anexo4_PDF(id_anexo4);
            //var dir = reporte.Anexo3_PDF(id_anexo3);
            //reporte.Anexo4_PDF(id_anexo4);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            //return RedirectToAction(nameof(PDF));
            //return Redirect(dir);
            return File(pdf, "application/pdf", $"Anexo 4 - {global.proyectos.Nombre}.pdf");
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

        public async Task<IActionResult> PDF_Viewer(int idADC)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            int id_anexo4 = _context.ADC_Anexo4.Where(a => a.Id_Anexo1 == idADC).OrderBy(a => a.Id).LastOrDefault().Id;

            var model = new V_Reporte_Anexo4_ADC();
            model.anexo4 = _context.ADC_Anexo4.Find(id_anexo4);
            model.anexo1 = Consultas.VistaAnexo1(_context, model.anexo4.Id_Anexo1);
            model.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id == model.anexo1.anexo1.Id).FirstOrDefault();
            model.anexo3 = _context.ADC_Anexo3.Find(model.anexo4.Id_Anexo3);

            model.responsable = _context.Usuarios.Where(u => u.Id == model.anexo3.Id_Responsable_ADC).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            model.lider = _context.Usuarios.Where(u => u.Id == model.adc.adc.Id_LiderEquipoVerificador).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            model.deo = _context.Usuarios.Where(u => u.Id == model.anexo3.Id_Director_Ejecutivo_Operacion).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            model.dems = _context.Usuarios.Where(u => u.Id == model.anexo3.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();
            model.residente = _context.Usuarios.Where(u => u.Id == model.anexo4.Id_Residente).Select(u => $"{u.Titulo} {u.Nombre} {u.Paterno} {u.Materno}").FirstOrDefault();

            var EV = _context.ADC_Equipo_Verificador.Where(a => a.Id_ADC == model.anexo3.Id_Anexo1).FirstOrDefault();
            model.ev = (from u in _context.Usuarios
                        join ev in _context.ADC_Equipo_Verificador_Integrantes on u.Id equals ev.Id_Usuario
                        where ev.Id_Equipo_Verificador_ADC == EV.Id
                        select u).ToList();
            //ViewBag.anexo1 = Consultas.VistaAnexo1(_context, idADC);
            //ViewBag.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id == idADC).FirstOrDefault();



            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View(model);
        }



    }
}
