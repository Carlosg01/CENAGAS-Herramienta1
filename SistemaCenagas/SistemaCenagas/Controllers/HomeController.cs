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
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private Usuario userlogin;

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
            IEnumerable<Usuario> users = await _context.Usuario.ToListAsync();

            var us = _context.Usuario.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();

            if(us != null)
            {
                Global.sesionUsuario = (Usuario)us;
                var emp = _context.Empleado.Where(e => e.Id_Usuario == Global.sesionUsuario.Id_Usuario).FirstOrDefault();
                Global.sesionEmpleado = (Empleado)emp;

                return RedirectToAction(nameof(Dashboard));

            }


            /*foreach (Usuario u in users)
            {
                if (u.Email.Equals(user.Email) && u.Password.Equals(user.Password))
                {
                    HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(u));
                    HttpContext.Session.SetString("Session", "usuario");
                    HttpContext.Session.SetString("IdUser", u.Id_Usuario.ToString());

                    Global.userSession = u;

                    return RedirectToAction(nameof(Dashboard));
                }
            }*/
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard()
        {

            if (Global.sesionUsuario == null)
                return RedirectToAction(nameof(Index));

            //userlogin = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserSession"));
            Global.session = "usuario";
            ViewBag.session = Global.session; //HttpContext.Session.GetString("Session");


            
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

                _context.Add(user);
                await _context.SaveChangesAsync();

                Empleado emp = new Empleado();
                emp.Id_Usuario = user.Id_Usuario;
                emp.Nombre = user.Nombre;
                string[] ap = user.Apellido.Split(" ");
                emp.Paterno = ap[0];
                emp.Materno = "";
                if (ap.Length == 2)
                    emp.Materno = ap[1];
                emp.Titulo = "Ing.";
                emp.Observaciones = "";
                _context.Add(emp);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
