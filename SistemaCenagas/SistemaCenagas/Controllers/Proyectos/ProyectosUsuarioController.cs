using System;
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
    public class ProyectosUsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProyectosUsuario
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }
            Global.vista_proyectos = Consultas.VistaProyectos(_context);
            return View();
        }

        public async Task<IActionResult> ADCs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.proyectos = Global.vista_proyectos.Where(p => p.Id == id).FirstOrDefault();

            return RedirectToAction("Index", "ADC_Proyecto");
            //return RedirectToAction("Create", "Anexo1");
        }

        public async Task<IActionResult> PreArranques(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.proyectos = Global.vista_proyectos.Where(p => p.Id == id).FirstOrDefault();

            return RedirectToAction("Index", "PreArranque_Proyecto");
            //return RedirectToAction("Create", "Anexo1");
        }

        // GET: ProyectosUsuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.proyectos = await _context.Proyectos.FindAsync(id);
            if (Global.proyectos == null)
            {
                return NotFound();
            }
            Global.miembrosProyecto = Consultas.VistaMiembrosProyecto(_context, Global.proyectos.Id);

            Proyecto_Miembro_Model model_ProyectoMiembro = new Proyecto_Miembro_Model();
            model_ProyectoMiembro.proyecto = Global.proyectos;
            model_ProyectoMiembro.miembros = new List<string>();
            model_ProyectoMiembro.idMiembro = new List<int>();

            return PartialView(model_ProyectoMiembro);
        }

        // GET: ProyectosUsuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProyectosUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proyecto,Clave,Nombre,Descripcion,Estado_ADC,Registro_Eliminado")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyectos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyectos);
        }

        // GET: ProyectosUsuario/Edit/5
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
            return PartialView(proyectos);
        }

        // POST: ProyectosUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Proyecto,Clave,Nombre,Descripcion,Estado_ADC,Registro_Eliminado")] Proyectos proyectos)
        {
            if (id != proyectos.Id)
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
                    if (!ProyectosExists(proyectos.Id))
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
            return PartialView(proyectos);
        }

        // GET: ProyectosUsuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyectos = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyectos == null)
            {
                return NotFound();
            }

            return View(proyectos);
        }

        // POST: ProyectosUsuario/Delete/5
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
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
