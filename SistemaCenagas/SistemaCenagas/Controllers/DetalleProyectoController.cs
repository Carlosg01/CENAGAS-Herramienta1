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

        public DetalleProyectoController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void inicializarInfoProyecto()
        {
            //Global.sesionDetalleProyecto = (DetalleProyecto)results.FirstOrDefault();
            ViewBag.session = Global.session;
            ViewBag.folioProyecto = Global._proyecto.Folio_adc;
            ViewBag.nombreProyecto = Global._proyecto.Nombre;
            ViewBag.instalacionArea = Global._proyecto.Instalacion_Area;
            ViewBag.tipoCambio = Global._proyecto.Tipo_Cambio;

            /*Consulta a los asignados del proyecto*/
            var idProyecto = new MySqlParameter("@idProyecto", Global._proyecto.Id_Proyecto);
            //var funcion = new MySqlParameter("@funcion", "lider de equipo verificador");
            IEnumerable<Empleado> empleadosProyecto = _context.Empleado.FromSqlRaw(
                "Call Proc_empleadosProyecto(@idProyecto)", idProyecto).ToList();

            ViewBag.empleadosProyecto = new string[3];
            int i;
            for (i = 0; i < 3; i++) ViewBag.empleadosProyecto[i] = "";
            i = 0;
            foreach (Empleado e in empleadosProyecto)
            {
                ViewBag.empleadosProyecto[i] = $"{e.Titulo} {e.Nombre} {e.Paterno} {e.Materno}";
                i++;
            }
        }

        // GET: DetalleProyecto
        public async Task<IActionResult> Index()
        {
            var idproyectoParam = new MySqlParameter("@idProyecto", Global._proyecto.Id_Proyecto);
            Global._detallesProyecto = await _context.DetalleProyecto.FromSqlRaw("Call Proc_DetallesProyectos(@idProyecto)",
            idproyectoParam).ToListAsync();

            inicializarInfoProyecto();            

            return View(Global._detallesProyecto);
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
            ViewBag.session = Global.session;
            
            inicializarInfoProyecto();

            return View(detalleProyecto);
        }

        // GET: DetalleProyecto/Create
        public IActionResult Create()
        {
            ViewBag.session = Global.session;
            ViewBag.asignacionProyecto = Global._proyecto;
            inicializarInfoProyecto();
            
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
                return RedirectToAction("Index", "DetalleProyecto");
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
            inicializarInfoProyecto();
            ViewBag.session = Global.session;

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
