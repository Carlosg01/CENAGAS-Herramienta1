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
        private string EMAIL_ADDRESS, EMAIL_PASSWORD;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            EMAIL_ADDRESS = "test@gmail.com";
            EMAIL_PASSWORD = "test";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario user)
        {            
            Usuario us = _context.Usuario.Where(u => u.Email == user.Email && 
                    u.Password == user.Password && u.Confirmacion_email != null).FirstOrDefault();

            if(us != null)
            {
                Global._usuario = (Usuario)us;

                return RedirectToAction(nameof(Dashboard));
            }            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard()
        {

            if (Global._usuario == null)
                return RedirectToAction(nameof(Index));

            //userlogin = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("UserSession"));
            Global.session = "usuario";
            ViewBag.session = Global.session; //HttpContext.Session.GetString("Session");
            ViewBag.username = Global._usuario.Nombre;


            
            return View();
        }

        public IActionResult CreateAccount()
        {
            ViewBag.CreateAccountSendEmail = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(Usuario user)
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

                    SendEmail(user, "CreateAccountConfirm", "Confirma tu email para acceder", emailText);

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
        public async Task<IActionResult> ForgotPassword(Usuario user)
        {
            ViewBag.ForgotPasswordSendEmail = true;
            ViewBag.email = user.Email;

            string emailText = "<h1>Olvidaste tu contraseña</h1>" +
            "<p>Por favor has clic en el siguiente enlace para resetear tu contraseña. </p>";

            SendEmail(user, "ResetPassword", "Verificacion para cambio de contraseña", emailText);

            return View();
        }

        

        [HttpGet]
        public async Task<IActionResult> CreateAccountConfirm(string idUser)
        {
            if (string.IsNullOrWhiteSpace(idUser))
                return NotFound();

            Usuario confirmUser = _context.Usuario.Find(int.Parse(idUser));
            confirmUser.Confirmacion_email = "Confirmado";
            _context.Update(confirmUser);
            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email)
        {
            Usuario user = new Usuario
            {
                Email = email
            };
            
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(Usuario user)
        {
            if (user.Password.Equals(user.Confirmar_Password))
            {
                Usuario confirmUser = _context.Usuario.Where(u => u.Email == user.Email).FirstOrDefault();
                confirmUser.Password = user.Password;
                _context.Update(confirmUser);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public string SendEmail(Usuario user, string action, string subject, string bodyText)
        {
            string url = $"https://localhost:44330/Home/{action}?" + 
                ((action.Equals("CreateAccountConfirm")) ? $"idUser={user.Id_Usuario}" : $"email={user.Email}");
            string emailText = bodyText + $"<a href='{url}'>Clic aquí</a>";
            string fromAddress = EMAIL_ADDRESS;
            string password = EMAIL_PASSWORD;
            string toAddress = user.Email;


            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
                    client.Authenticate(fromAddress, password);

                    var body = new BodyBuilder
                    {
                        HtmlBody = emailText//$"<p>Body test</p>",
                        //TextBody = emailText
                    };
                    var message = new MimeMessage
                    {
                        Body = body.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Sistema Cenagas", fromAddress));
                    message.To.Add(new MailboxAddress("", toAddress));
                    message.Subject = "Confirma tu email para acceder";
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Envio exitoso! :)";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public IActionResult LogOut()
        {
            Global._usuario = null;
            Global._proyecto = null;
            Global._detallesProyecto = null;
            Global.session = null;
            Global.nombreProyectoEmpleado = null;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccountSettings()
        {
            ViewBag.session = Global.session;
            return View((Usuario)Global._usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountSettings(Usuario user)
        {
            //return Content(JsonConvert.SerializeObject(user));
            /*actualiza tabla de usuarios*/
            user.Id_Usuario = Global._usuario.Id_Usuario;
            Usuario consultaUsuario = _context.Usuario.Find(Global._usuario.Id_Usuario);
            consultaUsuario.Nombre = user.Nombre;
            consultaUsuario.Paterno = user.Paterno;
            consultaUsuario.Materno = user.Materno;
            consultaUsuario.Email = user.Email;
            consultaUsuario.Rol = user.Rol;
            consultaUsuario.Observaciones = user.Observaciones;
            _context.Update(consultaUsuario);
            await _context.SaveChangesAsync();
            Global._usuario = consultaUsuario;
            
            return RedirectToAction(nameof(AccountSettings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(Usuario user)
        {
            if (user.Password.Equals(Global._usuario.Password) &&
                user.Nueva_Password.Equals(user.Confirmar_Password))
            {
                
                /*actualiza tabla de usuarios*/
                user.Id_Usuario = Global._usuario.Id_Usuario;
                Usuario consultaUsuario = _context.Usuario.Find(Global._usuario.Id_Usuario);
                consultaUsuario.Password = user.Nueva_Password;
                _context.Update(consultaUsuario);
                await _context.SaveChangesAsync();
                Global._usuario = consultaUsuario;
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
