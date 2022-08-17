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
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ReportesController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //global.busqueda = null;
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

            if (global.busqueda != null && global.busqueda.Busqueda != null) { 
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return View(global.busqueda);
            }
            else
                global.vista_adc = Consultas.VistaADC(_context);

            global.resumenADC = Consultas.VistaResumenADC(_context);

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        [HttpPost]
        public JsonResult getFiltro(int idBusqueda)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.TipoBusqueda = idBusqueda;
            if(idBusqueda == 1)
            {
                global.vista_proyectos = Consultas.VistaProyectos(_context);
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return Json(new SelectList(global.vista_proyectos, "Id", "Nombre"));
            }
            else if(idBusqueda == 2)
            {
                global.residencias = _context.Residencias.ToList();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return Json(new SelectList(global.residencias, "Id", "Nombre"));
            }
            else// if (idBusqueda == 3)
            {
                global.adcs = _context.ADC.ToList();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return Json(new SelectList(global.adcs, "Id", "Folio"));
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            return Json(new SelectList(global.gasoductos, "Id", "Nombre"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar(BusquedaReporte model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.busqueda = model;

            if (model.Id == 1)
                global.vista_adc = Consultas.VistaADC(_context).Where(a => a.id_proyecto == model.Id_Filtro);

            else if (model.Id == 2)
                global.vista_adc = Consultas.VistaADC(_context).Where(a => a.anexo1.Id_Residencia == model.Id_Filtro);

            else if (model.Id == 3)
                global.vista_adc = Consultas.VistaADC(_context).Where(a => a.adc.Id == model.Id_Filtro);

            else if (model.Id == 4)
            {
                global.resumenADC = Consultas.VistaResumenADC(_context);

                global.vista_adc = (from adc_ in Consultas.VistaADC(_context)
                                    join resumen in Consultas.VistaResumenADC(_context) on adc_.adc.Id equals resumen.id_adc
                                    where resumen.avance_ADC >= 100
                                    select adc_);
                                    

                //global.vista_adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == model.Id_Filtro);
            }
            else if (model.Id == 5)
            {
                global.resumenADC = Consultas.VistaResumenADC(_context);

                global.vista_adc = (from adc_ in Consultas.VistaADC(_context)
                                    join resumen in Consultas.VistaResumenADC(_context) on adc_.adc.Id equals resumen.id_adc
                                    where resumen.avance_ADC < 100
                                    select adc_);


                //global.vista_adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == model.Id_Filtro);
            }
            else if (model.Id == 6)
            {
                global.resumenADC = Consultas.VistaResumenADC(_context);//.Where(a => a.avance_ADC < 100);
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }
    }
}
