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
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult EmailAuto(string username)
        {
            string email = username + "@cenagas.gob.mx";
            return Json(email);            
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if (!Global.session.Equals("LogIn"))
            {
                return RedirectToAction("Index", "Home");
            }

            Global.vista_usuarios = Consultas.VistaUsuarios(_context);
            return View();
        }

        public async Task<IActionResult> Eliminados()
        {
            Global.vista_usuarios = Consultas.VistaUsuarios(_context).Where(u => u.user.Registro_Eliminado == 1);
            return View();
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {   
            if (id == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Global.usuario = new Global.V_Usuarios
            {
                Rol = _context.Roles.Where(
                    r => r.Id == usuario.Id_Rol).FirstOrDefault().Nombre
            };

            return PartialView(usuario);
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
        public async Task<IActionResult> Create(Usuarios usuario)
        {
            //return Content(JsonConvert.SerializeObject(usuario));
            if (ModelState.IsValid)
            {
                usuario.Email = usuario.Username + "@cenagas.gob.mx";
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            Global.usuario = Consultas.VistaUsuarios(_context).Where(u => u.user.Id == id).FirstOrDefault();

            return PartialView(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuarios usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && usuario.Password.Equals(usuario.Confirmar_Password))
            {
                try
                {
                    usuario.Email = usuario.Username + (usuario.Username.Equals("ahdzt.97") ? "@gmail.com" : "@cenagas.gob.mx");

                    if (usuario.Id_Rol == 2)
                    {
                        Global.lideres = _context.Usuarios
                            .Where(u => u.Id_Rol == 2 && u.Id != Global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == 3)
                    {
                        Global.responsablesADC = _context.Usuarios
                            .Where(u => u.Id_Rol == 3 && u.Id != Global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == 4)
                    {
                        Global.suplentes = _context.Usuarios
                            .Where(u => u.Id_Rol == 4 && u.Id != Global.session_usuario.user.Id).ToList();
                    }
                    //return Content(JsonConvert.SerializeObject(usuario));

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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

            /*Global.usuario = new Global.V_Usuarios
            {
                user = _context.Usuarios.Find(id),
                Rol = _context.Roles.Where(
                    r => r.Id_Rol == usuario.Id_Rol).FirstOrDefault().Nombre

            };*/

            return PartialView(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Global.usuario = new Global.V_Usuarios
            {
                Rol = _context.Roles.Where(
                    r => r.Id == usuario.Id_Rol).FirstOrDefault().Nombre
            };

            return PartialView(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            usuario.Registro_Eliminado = 1; 
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Global.usuario = new Global.V_Usuarios
            {
                Rol = _context.Roles.Where(
                    r => r.Id == usuario.Id_Rol).FirstOrDefault().Nombre
            };

            return PartialView(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            usuario.Registro_Eliminado = 0;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Eliminados));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
