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

using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;

using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;

/*using System.Net;
using System.Net.Mail;
using SistemaCenagas.Email;*/

namespace SistemaCenagas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private string EMAIL_ADDRESS, EMAIL_PASSWORD;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            ServicioEmail.EMAIL_ADDRESS = "aihm.mytests@gmail.com";
            ServicioEmail.EMAIL_PASSWORD = "test#12345";
            ServicioEmail.EMAIL_SERVER = "https://localhost:43366";
            ServicioEmail.EMAIL_SERVER = "http://armandohdz-001-site1.btempurl.com/";


        }

        public IActionResult Index()
        {
            Global.vista_usuarios = Consultas.VistaUsuarios(_context);
            Global.session = "LogOut";

            //return Content(JsonConvert.SerializeObject(_context.Roles.ToList()));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuarios user)
        {
            Usuarios us = _context.Usuarios.Where(u => u.Email == user.Email && 
                    u.Password == user.Password && u.Estatus != null).FirstOrDefault();

            if (us != null && us.Estatus.Equals("Habilitado"))
            {
                //return Content(JsonConvert.SerializeObject(us));
                Global.session_usuario = new Global.V_Usuarios { 
                    user = us,
                    Rol = _context.Roles.Where(
                    r => r.Id_Rol == us.Id_Rol).FirstOrDefault().Nombre 
                };
                return RedirectToAction(nameof(Dashboard));
            }
            Global.ERROR_MSJ = "El correo o contraseña son incorrectos!";
            //return Content(ViewBag.error ? "True" : "False");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard()
        {

            if (Global.session_usuario.user == null)
                return RedirectToAction(nameof(Index));

            //catalogos
            Global.roles = _context.Roles.Where(r => r.Id_Rol != 5).ToList();            
            Global.residencias = _context.Residencias.ToList();
            Global.lideres = _context.Usuarios
                .Where(u => u.Id_Rol == 2 && u.Id_Usuario != Global.session_usuario.user.Id_Usuario).ToList();
            Global.responsablesADC = _context.Usuarios
                .Where(u => u.Id_Rol == 3 && u.Id_Usuario != Global.session_usuario.user.Id_Usuario).ToList();
            Global.suplentes = _context.Usuarios
                .Where(u => u.Id_Rol == 4 && u.Id_Usuario != Global.session_usuario.user.Id_Usuario).ToList();

            Global.anexos = _context.Anexos.ToList();
            Global.vista_actividadesADC = _context.ADC_Actividades.ToList();
            
            Global.session = "LogIn";
            return View();
        }

        public IActionResult CreateAccount()
        {
            ViewBag.CreateAccountSendEmail = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(Usuarios user)
        {
            //return Content(JsonConvert.SerializeObject(user));

            if (ModelState.IsValid)
            {
                if (user.Password.Equals(user.Confirmar_Password) &&
                    user.Email.Split("@")[1].Equals("cenagas.gob.mx")) //validacion de dominio
                {
                    user.Username = user.Email.Split("@")[0];
                    user.Nombre = "";
                    user.Paterno = "";
                    user.Materno = "";
                    user.Titulo = "Ing.";
                    user.Observaciones = "";
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    ViewBag.CreateAccountSendEmail = true;
                    ViewBag.email = user.Email;

                    string emailText = "<h1>Bienvenido al sistema cenagas</h1>" +
                    "<p>Por favor confirma tu email haciendo clic en el siguiente enlace. </p>";

                    ServicioEmail.SendEmailResetPassword(user, "CreateAccountConfirm", "Confirma tu email para acceder", emailText);

                    return View();
                }                    
            }
            return View(user);

        }

        public IActionResult ForgotPassword()
        {
            ViewBag.ForgotPasswordSendEmail = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(Usuarios user)
        {
            ViewBag.ForgotPasswordSendEmail = true;
            ViewBag.email = user.Email;

            string emailText = "<h1>Olvidaste tu contraseña</h1>" +
            "<p>Por favor has clic en el siguiente enlace para resetear tu contraseña. </p>";

            ServicioEmail.SendEmailResetPassword(user, "ResetPassword", "Verificacion para cambio de contraseña", emailText);

            return View();
        }

        

        [HttpGet]
        public async Task<IActionResult> CreateAccountConfirm(string idUser)
        {
            if (string.IsNullOrWhiteSpace(idUser))
                return NotFound();

            Usuarios confirmUser = _context.Usuarios.Find(int.Parse(idUser));
            confirmUser.Estatus = "Habilitado";
            _context.Update(confirmUser);
            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email)
        {
            Usuarios user = new Usuarios
            {
                Email = email
            };
            
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(Usuarios user)
        {
            if (user.Password.Equals(user.Confirmar_Password))
            {
                Usuarios confirmUser = _context.Usuarios.Where(u => u.Email == user.Email).FirstOrDefault();
                confirmUser.Password = user.Password;
                _context.Update(confirmUser);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult LogOut()
        {
            Global.session = null;
            Global.usuario.user = null;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccountSettings()
        {
            return View((Usuarios)Global.session_usuario.user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountSettings(Usuarios user)
        {
            /*actualiza tabla de usuarios*/
            user.Id_Usuario = Global.session_usuario.user.Id_Usuario;
            Usuarios consultaUsuario = _context.Usuarios.Find(Global.session_usuario.user.Id_Usuario);
            consultaUsuario.Nombre = user.Nombre;
            consultaUsuario.Paterno = user.Paterno;
            consultaUsuario.Materno = user.Materno;
            consultaUsuario.Email = user.Email;
            consultaUsuario.Id_Rol = user.Id_Rol;
            consultaUsuario.Observaciones = user.Observaciones;
            _context.Update(consultaUsuario);
            await _context.SaveChangesAsync();
            Global.session_usuario.user = consultaUsuario;
            
            return RedirectToAction(nameof(AccountSettings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(Usuarios user)
        {
            if (user.Password.Equals(Global.session_usuario.user.Password) &&
                user.Nueva_Password.Equals(user.Confirmar_Password))
            {
                
                /*actualiza tabla de usuarios*/
                user.Id_Usuario = Global.session_usuario.user.Id_Usuario;
                Usuarios consultaUsuario = _context.Usuarios.Find(Global.session_usuario.user.Id_Usuario);
                consultaUsuario.Password = user.Nueva_Password;
                _context.Update(consultaUsuario);
                await _context.SaveChangesAsync();
                Global.session_usuario.user = consultaUsuario;
            }

            return RedirectToAction(nameof(AccountSettings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Notificaciones(Usuarios user)
        {
            /*actualiza notificaciones de usuarios*/
            user.Id_Usuario = Global.session_usuario.user.Id_Usuario;
            Usuarios consultaUsuario = _context.Usuarios.Find(Global.session_usuario.user.Id_Usuario);
            consultaUsuario.Notificacion_Tarea = user.Notificacion_Tarea;
            consultaUsuario.Notificacion_Proyecto = user.Notificacion_Proyecto;
            consultaUsuario.Notificacion_ADC = user.Notificacion_ADC;
            _context.Update(consultaUsuario);
            await _context.SaveChangesAsync();
            Global.session_usuario.user = consultaUsuario;

            return RedirectToAction(nameof(AccountSettings));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id_Usuario == id);
        }
    }
}
