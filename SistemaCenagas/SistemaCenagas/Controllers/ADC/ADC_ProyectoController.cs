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

namespace SistemaCenagas.Controllers
{
    public class ADC_ProyectoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ADC_ProyectoController(ApplicationDbContext context)
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
        public async Task<IActionResult> ADC(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.proyectos = Consultas.VistaProyectos(_context).Where(p => p.Id == id).FirstOrDefault();

            //return RedirectToAction("Index", "ADCProyecto");
            return RedirectToAction("Create", "ADC_Anexo1");
        }
        public async Task<IActionResult> Tareas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.adc = Global.vista_adc.Where(a => a.adc.Id == id).FirstOrDefault();

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
                .FirstOrDefaultAsync(m => m.Id == id);
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

            Global.adc = Global.vista_adc.Where(a => a.adc.Id == id).FirstOrDefault();

            return View(aDC);
        }

        // POST: ADC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ADC aDC)
        {
            if (id != aDC.Id)
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ADCExists(aDC.Id))
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
            return View(aDC);
        }

        // GET: ADC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aDC = await _context.ADC
                .FirstOrDefaultAsync(m => m.Id == id);
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
            aDC.Eliminado = 1;
            _context.ADC.Update(aDC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ADCExists(int id)
        {
            return _context.ADC.Any(e => e.Id == id);
        }
    }
}
