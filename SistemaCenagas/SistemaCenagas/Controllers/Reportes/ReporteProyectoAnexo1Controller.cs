﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using SistemaCenagas.Reportes;

namespace SistemaCenagas.Controllers
{
    public class ReporteProyectoAnexo1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReporteProyectoAnexo1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ADC
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            Global.vista_adc = Consultas.VistaADC(_context).Where(a => a.id_proyecto == Global.proyectos.Id);

            //Vista adc propuestas
            Global.vista_adc_propuestas = Global.vista_adc
                .Where(a => a.adc.Id_ProponenteCambio == Global.session_usuario.user.Id).ToList();

            //Vista adc a cargo
            if (Global.session_usuario.user.Id_Rol == Global.LIDER_EQUIPO_VERIFICADOR)
            {
                Global.vista_adc_cargo = Global.vista_adc
                    .Where(a => a.adc.Id_LiderEquipoVerificador == Global.session_usuario.user.Id).ToList();
            }
            else if(Global.session_usuario.user.Id_Rol == Global.RESPONSABLE_ADC)
            {
                Global.vista_adc_cargo = Global.vista_adc
                    .Where(a => a.adc.Id_ResponsableADC == Global.session_usuario.user.Id).ToList();
            }
            else if (Global.session_usuario.user.Id_Rol == Global.SUPLENTE)
            {
                Global.vista_adc_cargo = Global.vista_adc
                    .Where(a => a.adc.Id_Suplente == Global.session_usuario.user.Id).ToList();
            }

            Global.resumenADC = Consultas.VistaResumenADC(_context);

            return View();
        }
        public async Task<IActionResult> Descargar(int idADC)
        {
            ReporteAnexos reporte = new ReporteAnexos(_context);
            byte[] pdf = reporte.Anexo1_PDF(Global.proyectos, idADC);
            return File(pdf, "application/pdf", $"Anexo 1 - {Global.proyectos.Nombre}.pdf");
        }
        
    }
}