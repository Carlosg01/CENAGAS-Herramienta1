using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;

namespace SistemaCenagas.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Usuario userSession;

        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            //userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserSession"));
            //HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(userSession));

            //int idUser = int.Parse(HttpContext.Session.GetString("IdUser"));

            int idUser = Global.sesionEmpleado.Id_Empleado;

            var outputParameter = new MySqlParameter("@idEmpleado", idUser);
            IEnumerable<Proyectos> results = await _context.Proyectos.FromSqlRaw("Call ProyectosEmpleado(@idEmpleado)",
            outputParameter).ToListAsync();

            //HttpContext.Session.SetString("Session", "usuario");
            ViewBag.session = Global.session;//HttpContext.Session.GetString("Session");

            return View(results);
            //return View(await _context.Proyectos.ToListAsync());
        }

        /*public IActionResult Details()
        {
            
            return View();
        }*/

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos.FirstOrDefaultAsync(m => m.Id_Proyecto == id);
            if (proyectos == null)
            {
                return NotFound();
            }

            

            Global.sesionProyecto = (Proyectos)proyectos;
            return RedirectToAction("Index", "DetalleProyecto");
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyectos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proyecto,Folio_adc,Nombre,Instalacion_Area,Tipo_Cambio,Descripcion")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyectos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyectos);
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos.FindAsync(id);
            if (proyectos == null)
            {
                return NotFound();
            }
            return View(proyectos);
        }

        // POST: Proyectos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Proyecto,Folio_adc,Nombre,Instalacion_Area,Tipo_Cambio,Descripcion")] Proyectos proyectos)
        {
            if (id != proyectos.Id_Proyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyectos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectosExists(proyectos.Id_Proyecto))
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
            return View(proyectos);
        }

        // GET: Proyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.Id_Proyecto == id);
            if (proyectos == null)
            {
                return NotFound();
            }

            return View(proyectos);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyectos = await _context.Proyectos.FindAsync(id);
            _context.Proyectos.Remove(proyectos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectosExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id_Proyecto == id);
        }
    }
}
