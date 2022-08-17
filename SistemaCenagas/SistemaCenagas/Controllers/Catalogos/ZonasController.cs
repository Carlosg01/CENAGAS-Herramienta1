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
    public class ZonasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public ZonasController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
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
        private JsonConverter[] tString(string v)
        {
            throw new NotImplementedException();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (!global.session.Equals("LogIn"))
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            var model = _context.Zonas.ToList();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
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
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }
            var model = await _context.Zonas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
        global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));   
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Zonas model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            //HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var model = await _context.Zonas.FindAsync(id);
            if (model == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }
            
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Zonas model)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != model.Id)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            try
            {

                _context.Update(model);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(model.Id))
                {
                    HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                    ViewBag.global = global;
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var model = await _context.Zonas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }
            

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var model = await _context.Zonas.FindAsync(id);
            model.Eliminado = 1; 
            _context.Update(model);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            var model = await _context.Zonas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(model);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var usuario = await _context.Zonas.FindAsync(id);
            usuario.Eliminado = 0;
            _context.Zonas.Update(usuario);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Eliminados));
        }

        private bool UsuarioExists(int id)
        {
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return _context.Zonas.Any(e => e.Id == id);
        }
    }
}
