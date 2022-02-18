using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SistemaCenagas.Data;
using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario user)
        {            
            Usuario us = _context.Usuario.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();

            if(us != null)
            {
                Global.sesionUsuario = (Usuario)us;
                var emp = _context.Empleado.Where(e => e.Id_Usuario == Global.sesionUsuario.Id_Usuario).FirstOrDefault();
                Global.sesionEmpleado = (Empleado)emp;

                return RedirectToAction(nameof(Dashboard));
            }            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard()
        {

            if (Global.sesionUsuario == null)
                return RedirectToAction(nameof(Index));

            //userlogin = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserSession"));
            Global.session = "usuario";
            ViewBag.session = Global.session; //HttpContext.Session.GetString("Session");
            ViewBag.username = Global.sesionUsuario.Nombre;


            
            return View();
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(Usuario user)
        {

            if (ModelState.IsValid)
            {
                if (user.Password.Equals(user.Confirmar_Password))
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    Empleado emp = new Empleado();

                    emp.Id_Usuario = user.Id_Usuario;
                    emp.Nombre = "";
                    emp.Paterno = "";
                    emp.Materno = "";
                    emp.Titulo = "Ing.";
                    emp.Observaciones = "";
                    _context.Add(emp);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }                    
            }
            return View(user);

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            Global.sesionUsuario = null;
            Global.sesionEmpleado = null;
            Global.session = null;
            Global.nombreProyectoEmpleado = null;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccountSettings()
        {
            ViewBag.session = Global.session;
            return View((Usuario)Global.sesionUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountSettings(Usuario user)
        {
            //return Content(JsonConvert.SerializeObject(user));
            /*actualiza tabla de usuarios*/
            user.Id_Usuario = Global.sesionUsuario.Id_Usuario;
            Usuario consultaUsuario = _context.Usuario.Find(Global.sesionUsuario.Id_Usuario);
            consultaUsuario.Nombre = user.Nombre;
            consultaUsuario.Paterno = user.Paterno;
            consultaUsuario.Materno = user.Materno;
            consultaUsuario.Email = user.Email;
            consultaUsuario.Ubicacion = user.Ubicacion;
            consultaUsuario.Observaciones = user.Observaciones;
            _context.Update(consultaUsuario);
            await _context.SaveChangesAsync();
            Global.sesionUsuario = consultaUsuario;

            /*actualiza tabla de empleado*/
            Empleado consultaEmpleado = _context.Empleado.Find(Global.sesionEmpleado.Id_Empleado);
            consultaEmpleado.Nombre = user.Nombre;
            consultaEmpleado.Paterno = user.Paterno;
            consultaEmpleado.Materno = user.Materno;
            _context.Update(consultaEmpleado);
            await _context.SaveChangesAsync();
            Global.sesionEmpleado = consultaEmpleado;

            return RedirectToAction(nameof(AccountSettings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(Usuario user)
        {
            if (user.Password.Equals(Global.sesionUsuario.Password) &&
                user.Nueva_Password.Equals(user.Confirmar_Password))
            {
                
                /*actualiza tabla de usuarios*/
                user.Id_Usuario = Global.sesionUsuario.Id_Usuario;
                Usuario consultaUsuario = _context.Usuario.Find(Global.sesionUsuario.Id_Usuario);
                consultaUsuario.Password = user.Nueva_Password;
                _context.Update(consultaUsuario);
                await _context.SaveChangesAsync();
                Global.sesionUsuario = consultaUsuario;
            }

            return RedirectToAction(nameof(AccountSettings));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id_Usuario == id);
        }
    }
}
