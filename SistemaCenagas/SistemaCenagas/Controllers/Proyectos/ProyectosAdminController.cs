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
    public class ProyectosAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProyectosAdmin
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }
            Global.vista_proyectos = Consultas.VistaProyectos(_context);
            return View();
        }

        public async Task<IActionResult> Eliminados()
        {
            Global.vista_proyectos = Consultas.VistaProyectos(_context).Where(p => p.Eliminado == 1);
            return View();
        }

        // GET: ProyectosAdmin/Details/5
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

        // GET: ProyectosAdmin/Create
        public IActionResult Create()
        {
            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol > 2 && u.user.Registro_Eliminado == 0).ToList();
            ViewBag.clave = "PRO-CEN-UTA-022";
            Proyecto_Miembro_Model model_ProyectoMiembro = new Proyecto_Miembro_Model();
            model_ProyectoMiembro.miembros = new List<string>();
            model_ProyectoMiembro.idMiembro = new List<int>();
            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model_ProyectoMiembro.miembros.Add("false");
                model_ProyectoMiembro.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id);
            }
                
            return PartialView(model_ProyectoMiembro);
        }

        // POST: ProyectosAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proyecto_Miembro_Model _proyecto)
        {
            //return Content(JsonConvert.SerializeObject(_proyecto));
            if (ModelState.IsValid)
            {
                Proyectos p = new Proyectos
                {
                    Clave = _proyecto.proyecto.Clave,
                    Nombre = _proyecto.proyecto.Nombre,
                    Descripcion = _proyecto.proyecto.Descripcion,
                    Estado_ADC = _proyecto.proyecto.Estado_ADC,
                    Eliminado = 0
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                int idProyecto = _context.Proyectos.OrderBy(p => p.Id).LastOrDefault().Id;

                for(int i = 0; i < _proyecto.miembros.Count(); i++)
                {
                    if (_proyecto.miembros[i].Equals("true"))
                    {
                        Proyecto_Miembros pm = new Proyecto_Miembros
                        {
                            Id_Proyecto = idProyecto,
                            Id_Usuario = _proyecto.idMiembro[i],
                            Estatus = "Agregado"
                        };

                        //Notificacion por email a miembro
                        try
                        {  
                            Usuarios user = _context.Usuarios.Find(pm.Id_Usuario);

                            //verificar si el usuario tiene activo las notificaciones de proyecto
                            if(user.Notificacion_Proyecto.Equals("true"))
                            {
                                string emailText = $"<h1>Proyecto: <b>{p.Nombre}</b></h1>" +
                                "<p>Fuiste asignado al siguiente proyecto por parte de la administración de CENAGAS.</p>" +
                                "<p>Inicia sesión en tu cuenta y revisa la lista de proyectos para ver los detalles.</p>";

                                ServicioEmail.SendEmailNotification(user, "Asignación a proyecto", emailText);
                            }
                            
                        }
                        catch(Exception ex)
                        {

                        }

                        _context.Add(pm);
                        await _context.SaveChangesAsync();
                    }
                }
                /*_context.Add(proyecto);
                await _context.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }
            return PartialView(_proyecto);
        }

        // GET: ProyectosAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            Global.vista_usuarios = Consultas.VistaUsuarios(_context)
                .Where(u => u.user.Id_Rol > 2 && u.user.Registro_Eliminado == 0).ToList();
            for (int i = 0; i < Global.vista_usuarios.Count(); i++)
            {
                model_ProyectoMiembro.miembros.Add("false");
                model_ProyectoMiembro.idMiembro.Add(Global.vista_usuarios.ElementAt(i).user.Id);

                for(int j = 0; j < Global.miembrosProyecto.Count(); j++)
                {
                    if(Global.vista_usuarios.ElementAt(i).user.Id == Global.miembrosProyecto.ElementAt(j).pm.Id_Usuario &&
                        Global.miembrosProyecto.ElementAt(j).pm.Estatus.Equals("Agregado"))
                    {
                        model_ProyectoMiembro.miembros[i] = "true";
                        break;
                    }
                }
            }


            return PartialView(model_ProyectoMiembro);
        }

        // POST: ProyectosAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proyecto_Miembro_Model proyectos)
        {
            //return Content(JsonConvert.SerializeObject(proyectos));

            if (id != proyectos.proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyectos.proyecto);
                    await _context.SaveChangesAsync();


                    for (int i = 0; i < proyectos.miembros.Count(); i++)
                    {
                        Proyecto_Miembros p = _context.Proyecto_Miembros
                            .Where(p => p.Id_Proyecto == proyectos.proyecto.Id &&
                                        p.Id_Usuario == proyectos.idMiembro[i]).FirstOrDefault();


                        if (p != null)
                        {
                            p.Id_Proyecto = proyectos.proyecto.Id;
                            p.Id_Usuario = proyectos.idMiembro[i];
                            p.Estatus = (proyectos.miembros[i].Equals("true") ? "Agregado" : "Eliminado");
                            _context.Update(p);
                            //return Content(JsonConvert.SerializeObject(p));
                            
                        }
                        else if(proyectos.miembros[i].Equals("true"))
                        {
                            Proyecto_Miembros pm = new Proyecto_Miembros
                            {
                                Id_Proyecto = proyectos.proyecto.Id,
                                Id_Usuario = proyectos.idMiembro[i],
                                Estatus = "Agregado"
                            };
                            _context.Add(pm);
                            
                            //Notificacion por email a miembro
                            try
                            {
                                Usuarios user = _context.Usuarios.Find(pm.Id_Usuario);

                                //verificar si el usuario tiene activo las notificaciones de proyecto
                                if (user.Notificacion_Proyecto.Equals("true"))
                                {
                                    string emailText = $"<h1>Proyecto: <b>{proyectos.proyecto.Nombre}</b></h1>" +
                                    "<p>Fuiste asignado al siguiente proyecto por parte de la administración de CENAGAS.</p>" +
                                    "<p>Inicia sesión en tu cuenta y revisa la lista de proyectos para ver los detalles.</p>";

                                    ServicioEmail.SendEmailNotification(user, "Asignación a proyecto", emailText);
                                }
                                    
                            }
                            catch (Exception ex)
                            {

                            }

                            //return Content(JsonConvert.SerializeObject(pm));
                        }
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectosExists(proyectos.proyecto.Id))
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

        // GET: ProyectosAdmin/Delete/5
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
            Global.proyectos = Global.vista_proyectos.Where(p => p.Id == id).FirstOrDefault();

            return PartialView(proyectos);
        }

        // POST: ProyectosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyectos = await _context.Proyectos.FindAsync(id);
            proyectos.Eliminado = 1;
            _context.Proyectos.Update(proyectos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
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
            Global.proyectos = Global.vista_proyectos.Where(p => p.Id == id).FirstOrDefault();

            return PartialView(proyectos);
        }

        // POST: ProyectosAdmin/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            var proyectos = await _context.Proyectos.FindAsync(id);
            proyectos.Eliminado = 0;
            _context.Proyectos.Update(proyectos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Eliminados));
        }


        private bool ProyectosExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
