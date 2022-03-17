﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class Anexo1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Anexo1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anexo1
        public async Task<IActionResult> Index()
        {
            Global.vista_anexo1 = Consultas.VistaAnexo1(_context);
            return View();
        }

        // GET: Anexo1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1 = await _context.Anexo1
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1 == null)
            {
                return NotFound();
            }

            return View(anexo1);
        }

        // GET: Anexo1/Create
        public IActionResult Create()
        {
            ViewBag.fecha = DateTime.Now.ToShortDateString();
            return View();
        }

        // POST: Anexo1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anexo1 anexo1)
        {
            if (ModelState.IsValid)
            {
                anexo1.Estatus = "Pendiente";
                _context.Add(anexo1);                

                ADC adc = new ADC
                {                    
                    Id_Proyecto = Global.proyectos.Id_Proyecto,
                    Id_ProponenteCambio = Global.session_usuario.user.Id_Usuario,
                    Id_Lider = 2,
                    Id_ResponsableADC = 1,
                    Id_Suplente = 1,
                    Fecha_Actualizacion = anexo1.Fecha,
                    Registro_Eliminado = 0
                };
                _context.Add(adc);
                await _context.SaveChangesAsync();

                int id = _context.ADC.OrderByDescending(a => a.Id_ADC).FirstOrDefault().Id_ADC;
                Global.adc = Consultas.VistaADC(_context).Where(a => a.adc.Id_ADC == id).FirstOrDefault();

                return RedirectToAction("Index", "ADC_Procesos");
            }
            return View(anexo1);
        }

        // GET: Anexo1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1 = await _context.Anexo1.FindAsync(id);
            if (anexo1 == null)
            {
                return NotFound();
            }
            return View(anexo1);
        }

        // POST: Anexo1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_PropuestaCambio,Id_Proyecto,Tipo_Cambio,Fecha,Id_Residencia,Sector_Area,Planta_Instalacion,Proceso,Prestacion_Servicio,Descripcion,Resultados_Analisis,Resultados_Propuesta,Estatus,Registro_Eliminado")] Anexo1 anexo1)
        {
            if (id != anexo1.Id_PropuestaCambio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anexo1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1Exists(anexo1.Id_PropuestaCambio))
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
            return View(anexo1);
        }

        // GET: Anexo1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1 = await _context.Anexo1
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1 == null)
            {
                return NotFound();
            }

            return View(anexo1);
        }

        // POST: Anexo1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anexo1 = await _context.Anexo1.FindAsync(id);
            _context.Anexo1.Remove(anexo1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Anexo1Exists(int id)
        {
            return _context.Anexo1.Any(e => e.Id_PropuestaCambio == id);
        }
    }
}