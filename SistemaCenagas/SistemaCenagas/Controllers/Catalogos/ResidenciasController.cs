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
    public class ResidenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            Global.residencias = _context.Residencias.ToList();
            Global.estados = _context.Estados.ToList();
            

            var model = (from res in _context.Residencias
                         join est in _context.Estados on res.Id_Estado equals est.Id
                         select new ResidenciasIndex
                         {
                             residencia = res,
                             estado = est.Estado,
                             capital = est.Capital
                         }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Eliminados()
        {
            Global.vista_usuarios = Consultas.VistaUsuarios(_context).Where(u => u.user.Eliminado == 1);
            return View();
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {   
            if (id == null)
            {
                return NotFound();
            }
            var model = await _context.Residencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView(model);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {   
            return PartialView();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Residencias model)
        {
            //return Content(JsonConvert.SerializeObject(usuario));
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(model);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Residencias.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView(model);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Residencias model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {

                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenciasExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return PartialView(model);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Residencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            

            return PartialView(model);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Residencias.FindAsync(id);
            model.Eliminado = 1; 
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Residencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView(model);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            var model = await _context.Residencias.FindAsync(id);
            model.Eliminado = 0;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Eliminados));
        }

        private bool ResidenciasExists(int id)
        {
            return _context.Residencias.Any(e => e.Id == id);
        }
    }
}
