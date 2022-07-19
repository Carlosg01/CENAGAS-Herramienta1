﻿using System;
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
    public class DDVController : Controller
    {
        //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
        private readonly ApplicationDbContext _context;
        private Global global;

        public DDVController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            var model = _context.DDV.ToList();

            ViewBag.global = global;
            return View(model);
        }

        public async Task<IActionResult> Eliminados()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            global.vista_usuarios = Consultas.VistaUsuarios(_context).Where(u => u.user.Eliminado == 1);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
        global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));   
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            var model = await _context.DDV
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return PartialView(model);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
        global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));   
            ViewBag.global = global;
            return PartialView();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DDV model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.global = global;
            return PartialView(model);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var model = await _context.DDV.FindAsync(id);
            if (model == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DDV model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != model.Id)
            {
                ViewBag.global = global;
                return NotFound();
            }

            try
            {

                _context.Update(model);
                await _context.SaveChangesAsync();
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(model.Id))
                {
                    ViewBag.global = global;
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewBag.global = global;
            return PartialView(model);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var model = await _context.DDV
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                ViewBag.global = global;
                return NotFound();
            }
            

            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var model = await _context.DDV.FindAsync(id);
            model.Eliminado = 1; 
            _context.Update(model);
            await _context.SaveChangesAsync();
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            var model = await _context.DDV
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                ViewBag.global = global;
                return NotFound();
            }

            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var usuario = await _context.DDV.FindAsync(id);
            usuario.Eliminado = 0;
            _context.DDV.Update(usuario);
            await _context.SaveChangesAsync();
            ViewBag.global = global;
            return RedirectToAction(nameof(Eliminados));
        }

        private bool UsuarioExists(int id)
        {
            ViewBag.global = global;
            return _context.DDV.Any(e => e.Id == id);
        }
    }
}
