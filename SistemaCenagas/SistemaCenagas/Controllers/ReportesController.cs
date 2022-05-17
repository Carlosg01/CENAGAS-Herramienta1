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

namespace SistemaCenagas.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
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

            if (Global.busqueda != null)
                return View(Global.busqueda);
            else
                Global.vista_adc = Consultas.VistaADC(_context);

            return View();
        }

        [HttpPost]
        public JsonResult getFiltro(int idBusqueda)
        {
            Global.TipoBusqueda = idBusqueda;
            if(idBusqueda == 1)
            {
                Global.vista_proyectos = Consultas.VistaProyectos(_context);
                return Json(new SelectList(Global.vista_proyectos, "Id_Proyecto", "Nombre"));
            }
            else if(idBusqueda == 2)
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
                Global.vista_adc = Consultas.VistaADC(_context).Where(a => a.id_proyecto == model.Id_Filtro);

            else if (model.Id_Busqueda == 2)
                Global.vista_adc = Consultas.VistaADC(_context).Where(a => a.anexo1.Id_Residencia == model.Id_Filtro);

            else if (model.Id_Busqueda == 3)
                Global.vista_adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == model.Id_Filtro);

            else if (model.Id_Busqueda == 4)
            {
                Global.resumenADC = Consultas.VistaResumenADC(_context);

                Global.vista_adc = (from adc_ in Consultas.VistaADC(_context)
                                    join resumen in Consultas.VistaResumenADC(_context) on adc_.adc.Id_ADC equals resumen.id_adc
                                    where resumen.avance_ADC >= 100
                                    select adc_);
                                    

                //Global.vista_adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == model.Id_Filtro);
            }
            else if (model.Id_Busqueda == 5)
            {
                Global.resumenADC = Consultas.VistaResumenADC(_context);

                Global.vista_adc = (from adc_ in Consultas.VistaADC(_context)
                                    join resumen in Consultas.VistaResumenADC(_context) on adc_.adc.Id_ADC equals resumen.id_adc
                                    where resumen.avance_ADC < 100
                                    select adc_);


                //Global.vista_adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == model.Id_Filtro);
            }
            else if (model.Id_Busqueda == 6)
            {
                Global.resumenADC = Consultas.VistaResumenADC(_context);//.Where(a => a.avance_ADC < 100);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
