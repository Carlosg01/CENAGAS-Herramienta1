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
    public class DetalleProyectoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Usuario userSession;
        private int idUser, idProyecto;
        IEnumerable<DetalleProyecto> results;

        public DetalleProyectoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleProyecto
        public async Task<IActionResult> Index()
        {
            //idUser = int.Parse(HttpContext.Session.GetString("IdUser"));

            idUser = Global.sesionUsuario.Id_Usuario;
            idProyecto = Global.sesionProyecto.Id_Proyecto;

            var idempleadoParam = new MySqlParameter("@idEmpleado", idUser);
            var idproyectoParam = new MySqlParameter("@idProyecto", idProyecto);
            results = await _context.DetalleProyecto.FromSqlRaw("Call DetalleProyectosEmpleado(@idEmpleado, @idProyecto)",
            idempleadoParam, idproyectoParam).ToListAsync();

            Global.sesionDetalleProyecto = (DetalleProyecto)results.FirstOrDefault();
            ViewBag.session = Global.session;
            ViewBag.nombreProyectoEmpleado = Global.sesionProyecto.Nombre;
            ////return Content(ViewBag.nombreProyectoEmpleado);

            return View(results);
        }

        // GET: DetalleProyecto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyecto
                .FirstOrDefaultAsync(m => m.Id_DetalleProyecto == id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }

            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Create
        public IActionResult Create()
        {
            ViewBag.idProyecto = Global.sesionDetalleProyecto.Id_Proyecto;
            ViewBag.idAsignacion = Global.sesionDetalleProyecto.Id_Asignacion;
            return View();
        }

        // POST: DetalleProyecto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_DetalleProyecto,Id_Proyecto,Id_Residencia,Id_Asignacion,No_Desarrollo,Desarrollo,Descripcion_Actividad,Avance,Faltante_Comentarios,Comentarios,Plan_Accion,Anexos,Tipo_Proyecto")] DetalleProyecto detalleProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction("/DetalleProyecto/Index");
            }
            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyecto.FindAsync(id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }
            return View(detalleProyecto);
        }

        // POST: DetalleProyecto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_DetalleProyecto,Id_Proyecto,Id_Residencia,Id_Asignacion,No_Desarrollo,Desarrollo,Descripcion_Actividad,Avance,Faltante_Comentarios,Comentarios,Plan_Accion,Anexos,Tipo_Proyecto")] DetalleProyecto detalleProyecto)
        {
            if (id != detalleProyecto.Id_DetalleProyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleProyectoExists(detalleProyecto.Id_DetalleProyecto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(userSession));
                return RedirectToAction(nameof(Index));
            }
            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyecto
                .FirstOrDefaultAsync(m => m.Id_DetalleProyecto == id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }

            return View(detalleProyecto);
        }

        // POST: DetalleProyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleProyecto = await _context.DetalleProyecto.FindAsync(id);
            _context.DetalleProyecto.Remove(detalleProyecto);
            await _context.SaveChangesAsync();
            return View("Index");
        }

        private bool DetalleProyectoExists(int id)
        {
            return _context.DetalleProyecto.Any(e => e.Id_DetalleProyecto == id);
        }
    }
}
