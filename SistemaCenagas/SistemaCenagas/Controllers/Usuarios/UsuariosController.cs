using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Global global;

        public UsuariosController(ApplicationDbContext context)
        {
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            _context = context;
            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
        }

        [HttpPost]
        public JsonResult EmailAuto(string username)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            string email = username + "@cenagas.gob.mx";
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return Json(email);            
        }
        public async Task<bool> getGlobal()
        {
            var json = HttpContext.Session.GetString("Global");
            if(json == null || json.Length == 0)
            {
                return false;
            }
            global = JsonConvert.DeserializeObject<Global>(json);
            return true;
        }
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if(!await getGlobal()) return RedirectToAction("Index", "Home");

            if (!global.session.Equals("LogIn"))
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction("Index", "Home");
            }

            global.vista_usuarios = Consultas.VistaUsuarios(_context);
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
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
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }
            global.usuario = new V_Usuarios
            {
                Rol = _context.Roles.Where(
                    r => r.Id == usuario.Id_Rol).FirstOrDefault().Nombre
            };

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(usuario);
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
        public async Task<IActionResult> Create(Usuarios usuario)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
            ViewBag.global = global;
            //return Content(JsonConvert.SerializeObject(usuario));
            if (ModelState.IsValid)
            {
                usuario.Email = usuario.Username + "@cenagas.gob.mx";
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                if(usuario.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR)
                {
                    global.lideres = _context.Usuarios.Where(u => u.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR && u.Id != global.session_usuario.user.Id).ToList();
                } 
                if(usuario.Id_Rol == global.RESPONSABLE_ADC)
                {
                    global.responsablesADC = _context.Usuarios.Where(u => u.Id_Rol == global.RESPONSABLE_ADC && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
                }
                if (usuario.Id_Rol == global.RESPONSABLE_PREARRANQUE)
                {
                    global.responsablesPreArranque = _context.Usuarios.Where(u => u.Id_Rol == global.RESPONSABLE_PREARRANQUE && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
                }
                if (usuario.Id_Rol == global.SUPLENTE)
                {
                    global.suplentes = _context.Usuarios.Where(u => u.Id_Rol == global.SUPLENTE && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
                }

                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(usuario);
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

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            global.usuario = Consultas.VistaUsuarios(_context).Where(u => u.user.Id == id).FirstOrDefault();

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuarios usuario)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            if (id != usuario.Id)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }

            if (ModelState.IsValid && usuario.Password.Equals(usuario.Confirmar_Password))
            {
                try
                {
                    usuario.Email = usuario.Username + (usuario.Username.Equals("ahdzt.97") ? "@gmail.com" : "@cenagas.gob.mx");
                    /*
                    if (usuario.Id_Rol == 2)
                    {
                        global.lideres = _context.Usuarios
                            .Where(u => u.Id_Rol == 2 && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == 3)
                    {
                        global.responsablesADC = _context.Usuarios
                            .Where(u => u.Id_Rol == 3 && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == 4)
                    {
                        global.suplentes = _context.Usuarios
                            .Where(u => u.Id_Rol == 4 && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    */
                    
                    HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));//
                    ViewBag.global = global;
                    //return Content(JsonConvert.SerializeObject(usuario));

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    
                    if (usuario.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR)
                    {
                        global.lideres = _context.Usuarios.Where(u => u.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == global.RESPONSABLE_ADC)
                    {
                        global.responsablesADC = _context.Usuarios.Where(u => u.Id_Rol == global.RESPONSABLE_ADC && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == global.RESPONSABLE_PREARRANQUE)
                    {
                        global.responsablesPreArranque = _context.Usuarios.Where(u => u.Id_Rol == global.RESPONSABLE_PREARRANQUE && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    if (usuario.Id_Rol == global.SUPLENTE)
                    {
                        global.suplentes = _context.Usuarios.Where(u => u.Id_Rol == global.SUPLENTE && u.Id != global.session_usuario.user.Id).ToList();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
                return RedirectToAction(nameof(Index));
            }

            /*global.usuario = new global.V_Usuarios
            {
                user = _context.Usuarios.Find(id),
                Rol = _context.Roles.Where(
                    r => r.Id_Rol == usuario.Id_Rol).FirstOrDefault().Nombre

            };*/

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(usuario);
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

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }
            global.usuario = new V_Usuarios
            {
                Rol = _context.Roles.Where(
                    r => r.Id == usuario.Id_Rol).FirstOrDefault().Nombre
            };

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var usuario = await _context.Usuarios.FindAsync(id);
            usuario.Eliminado = 1; 
            _context.Usuarios.Update(usuario);
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

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                ViewBag.global = global;
                return NotFound();
            }
            global.usuario = new V_Usuarios
            {
                Rol = _context.Roles.Where(
                    r => r.Id == usuario.Id_Rol).FirstOrDefault().Nombre
            };

            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return PartialView(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(int id)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var usuario = await _context.Usuarios.FindAsync(id);
            usuario.Eliminado = 0;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return RedirectToAction(nameof(Eliminados));
        }

        private bool UsuarioExists(int id)
        {
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
