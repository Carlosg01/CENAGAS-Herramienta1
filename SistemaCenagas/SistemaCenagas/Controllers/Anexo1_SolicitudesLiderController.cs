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
    public class Anexo1_SolicitudesLiderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Anexo1_SolicitudesLiderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anexo1_SolicitudesCambio
        public async Task<IActionResult> Index()
        {
            Global.vistaAnexo1 = (from a in _context.Anexo1_PropuestaCambio
                                  join pro in _context.Proyectos on a.Id_Proyecto equals pro.Id_Proyecto
                                  join r in _context.Residencias on a.Id_Residencia equals r.Id_Residencia
                                  join p in _context.Usuarios on a.Id_ProponenteCambio equals p.Id_Usuario
                                  join res in _context.Usuarios on a.Id_ResponsableADC equals res.Id_Usuario
                                  join lid in _context.Usuarios on pro.Id_Lider equals lid.Id_Usuario
                                  where a.Id_Proyecto == Global.proyecto.Id_Proyecto
                                  select new Global.VistaAnexo1
                                  {
                                      id_PropuestaCambio = a.Id_PropuestaCambio,
                                      tipoCambio = a.Tipo_Cambio,
                                      fecha = a.Fecha,
                                      id_lider = pro.Id_Lider,
                                      lider = lid.Titulo + " " + lid.Nombre + " " + lid.Paterno + " " + lid.Materno,
                                      residencia = r.Nombre,
                                      proponente = p.Titulo + " " + p.Nombre + " " + p.Paterno + " " + p.Materno,
                                      id_responsableADC = res.Id_Usuario,
                                      responsableADC = res.Titulo + " " + res.Nombre + " " + res.Paterno + " " + res.Materno,
                                      estatus = a.Estatus
                                  });
            return View();
        }

        // GET: Anexo1_SolicitudesCambio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Global.vistaPropuesta = (from a in _context.Anexo1_PropuestaCambio
                                     join r in _context.Residencias on a.Id_Residencia equals r.Id_Residencia
                                     join g in _context.Gasoductos on a.Sector_Area equals g.Ut_Gasoducto
                                     join t in _context.Tramos on a.Planta_Instalacion equals t.Ut_Tramo
                                     join p in _context.Usuarios on a.Id_ProponenteCambio equals p.Id_Usuario
                                     join res in _context.Usuarios on a.Id_ResponsableADC equals res.Id_Usuario
                                     where a.Id_Proyecto == Global.proyecto.Id_Proyecto && a.Id_PropuestaCambio == id
                                     select new Global.VistaAnexo1
                                     {
                                         proponente = p.Titulo + " " + p.Nombre + " " + p.Paterno + " " + p.Materno,
                                         responsableADC = res.Titulo + " " + res.Nombre + " " + res.Paterno + " " + res.Materno,
                                         sector_area = g.Gasoducto,
                                         planta_instalacion = t.Tramo,
                                         residencia = r.Nombre

                                     }).FirstOrDefault();

            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1_PropuestaCambio == null)
            {
                return NotFound();
            }

            return View(anexo1_PropuestaCambio);
        }

        // GET: Anexo1_SolicitudesCambio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anexo1_SolicitudesCambio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anexo1_PropuestaCambio anexo1_PropuestaCambio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anexo1_PropuestaCambio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anexo1_PropuestaCambio);
        }

        // GET: Anexo1_SolicitudesCambio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio.FindAsync(id);
            Global.vistaPropuesta = (from a in _context.Anexo1_PropuestaCambio
                                     join r in _context.Residencias on a.Id_Residencia equals r.Id_Residencia
                                     join g in _context.Gasoductos on a.Sector_Area equals g.Ut_Gasoducto
                                     join t in _context.Tramos on a.Planta_Instalacion equals t.Ut_Tramo                                     
                                     join p in _context.Usuarios on a.Id_ProponenteCambio equals p.Id_Usuario
                                     join res in _context.Usuarios on a.Id_ResponsableADC equals res.Id_Usuario
                                     where a.Id_Proyecto == Global.proyecto.Id_Proyecto && a.Id_PropuestaCambio == id
                                     select new Global.VistaAnexo1
                                     {                                         
                                         proponente = p.Titulo + " " + p.Nombre + " " + p.Paterno + " " + p.Materno,
                                         responsableADC = res.Titulo + " " + res.Nombre + " " + res.Paterno + " " + res.Materno,
                                         sector_area = g.Gasoducto,
                                         planta_instalacion = t.Tramo,
                                         residencia = r.Nombre
                                         
                                     }).FirstOrDefault();

            Global.responsables_adc = _context.Usuarios.Where(u => u.Rol.Equals("Responsable de la administración de cambio")).ToList();
            return View(anexo1_PropuestaCambio);
        }

        // POST: Anexo1_SolicitudesCambio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Anexo1_PropuestaCambio anexo1_PropuestaCambio)
        {
            if (id != anexo1_PropuestaCambio.Id_PropuestaCambio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(anexo1_PropuestaCambio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anexo1_PropuestaCambioExists(anexo1_PropuestaCambio.Id_PropuestaCambio))
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
            return View(anexo1_PropuestaCambio);
        }

        // GET: Anexo1_SolicitudesCambio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio
                .FirstOrDefaultAsync(m => m.Id_PropuestaCambio == id);
            if (anexo1_PropuestaCambio == null)
            {
                return NotFound();
            }

            return View(anexo1_PropuestaCambio);
        }

        // POST: Anexo1_SolicitudesCambio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anexo1_PropuestaCambio = await _context.Anexo1_PropuestaCambio.FindAsync(id);
            _context.Anexo1_PropuestaCambio.Remove(anexo1_PropuestaCambio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Anexo1_PropuestaCambioExists(int id)
        {
            return _context.Anexo1_PropuestaCambio.Any(e => e.Id_PropuestaCambio == id);
        }
    }
}
