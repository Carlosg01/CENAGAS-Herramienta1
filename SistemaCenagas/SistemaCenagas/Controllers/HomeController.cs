﻿using Microsoft.AspNetCore.Http;
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
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Jint;

/*using System.Net;
using System.Net.Mail;
using SistemaCenagas.Email;*/

namespace SistemaCenagas.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;

        private Global global;

        public HomeController(ApplicationDbContext context, IConfiguration config, ITokenService tokenService)
        {
            _context = context;
            ServicioEmail.EMAIL_ADDRESS = "aihm.mytests@gmail.com";
            ServicioEmail.EMAIL_PASSWORD = "test#12345";
            ServicioEmail.EMAIL_SERVER = "https://localhost:43366";
            ServicioEmail.EMAIL_SERVER = "http://cenagas-001-site1.htempurl.com/";

            _config = config;
            _tokenService = tokenService;

        }

        public IActionResult Index()
        {
            
            /*
            if (HttpContext.Session.GetString("Global") != null)
            {
                global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
                if(global == null)
                {
                    HttpContext.Session.Clear();                    
                }
                else if (global.session_usuario.user != null)
                {
                    return RedirectToAction(nameof(Dashboard));
                }
                

            }
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(new Global()));
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            //Se carga la informacion
            global.ADMINISTRADOR = _context.Roles.Where(x => x.Nombre.Equals("Administrador")).FirstOrDefault().Id;
            global.SUPERADMIN = _context.Roles.Where(x => x.Nombre.Equals("Superadmin")).FirstOrDefault().Id;
            global.RESPONSABLE_ADC = _context.Roles.Where(x => x.Nombre.Equals("Responsable de la administración de cambio")).FirstOrDefault().Id;
            global.RESPONSABLE_PREARRANQUE = _context.Roles.Where(x => x.Nombre.Equals("Responsable de la revisión de seguridad del pre-arranque")).FirstOrDefault().Id;
            global.SUPLENTE = _context.Roles.Where(x => x.Nombre.Equals("Suplente")).FirstOrDefault().Id;
            global.LIDER_EQUIPO_VERIFICADOR = _context.Roles.Where(x => x.Nombre.Equals("Lider de equipo verificador")).FirstOrDefault().Id;
            global.EQUIPO_VERIFICADOR = _context.Roles.Where(x => x.Nombre.Equals("Equipo verificador")).FirstOrDefault().Id;
            global.EMPLEADO = _context.Roles.Where(x => x.Nombre.Equals("Empleado")).FirstOrDefault().Id;

            global.vista_usuarios = Consultas.VistaUsuarios(_context);
            global.session = "LogOut";
            
            ////return Content(JsonConvert.SerializeObject(_context.Roles.ToList()));
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            */
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuarios user)
        {
            Usuarios us = _context.Usuarios.Where(u => u.Email == user.Email && 
                    u.Password == user.Password && u.Estatus != null).FirstOrDefault();

            IActionResult response = Unauthorized();

            //global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            var engine = new Engine().Execute("function alertError(msj){alert(msj);}");

            if (us != null && us.Estatus.Equals("Habilitado"))
            {
                /*
                if(us.Token != null)
                {
                    HttpContext.Session.SetString("Token", us.Token);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(us)); //.SetString("Token", us.Token);
                    HttpContext.Session.SetString("prueba", JsonConvert.SerializeObject(p));
                    return RedirectToAction(nameof(Dashboard));
                }
                */


                generatedToken = _tokenService.BuildToken(_config["Jwt:SecretKey1"].ToString(), _config["Jwt:Issuer"].ToString(), us, _context);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(us));
                    HttpContext.Session.SetString("Token", generatedToken);

                    HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(new Global()));
                    global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

                    //Se carga la informacion
                    global.ADMINISTRADOR = _context.Roles.Where(x => x.Nombre.Equals("Administrador")).FirstOrDefault().Id;
                    global.SUPERADMIN = _context.Roles.Where(x => x.Nombre.Equals("Superadmin")).FirstOrDefault().Id;
                    global.RESPONSABLE_ADC = _context.Roles.Where(x => x.Nombre.Equals("Responsable de la administración de cambio")).FirstOrDefault().Id;
                    global.RESPONSABLE_PREARRANQUE = _context.Roles.Where(x => x.Nombre.Equals("Responsable de la revisión de seguridad del pre-arranque")).FirstOrDefault().Id;
                    global.SUPLENTE = _context.Roles.Where(x => x.Nombre.Equals("Suplente")).FirstOrDefault().Id;
                    global.LIDER_EQUIPO_VERIFICADOR = _context.Roles.Where(x => x.Nombre.Equals("Lider de equipo verificador")).FirstOrDefault().Id;
                    global.EQUIPO_VERIFICADOR = _context.Roles.Where(x => x.Nombre.Equals("Equipo verificador")).FirstOrDefault().Id;
                    global.EMPLEADO = _context.Roles.Where(x => x.Nombre.Equals("Empleado")).FirstOrDefault().Id;

                    global.vista_usuarios = Consultas.VistaUsuarios(_context);
                    global.session = "LogOut";

                    HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
                    return RedirectToAction(nameof(Dashboard));
                }
                else
                {
                    //engine.Invoke("alertError", "Error al generar el token!");
                    //global.ERROR_MSJ = "Error al generar el token!";
                    return RedirectToAction(nameof(Index));
                }
            }
            //engine.Invoke("alertError", "El correo o contraseña son incorrectos!");
            //global.ERROR_MSJ = "El correo o contraseña son incorrectos!";
            return RedirectToAction(nameof(Index));
            ////return Content(ViewBag.error ? "True" : "False");


        }


        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var json = HttpContext.Session.GetString("Global");
            if (json == null || json.Length == 0)
            {
                return RedirectToAction("Index");
            }
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            string token = HttpContext.Session.GetString("Token");
            //var emailUserLogged = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            //var userLogged = _context.Usuarios.Where(u => u.Email == emailUserLogged.Value).FirstOrDefault();

            //HttpContext.Session.SetString("Token", token);
            var _user = JsonConvert.DeserializeObject<Usuarios>(HttpContext.Session.GetString("User"));

            



            _user.Token = token;

            if(global.session == null || global.session == "LogOut")
            {
                global.session_usuario = new V_Usuarios
                {
                    user = new Usuarios
                    {
                        Id = int.Parse(User.FindFirstValue("Id")),
                        Id_Rol = int.Parse(User.FindFirstValue("Id_Rol")),
                        Nombre = User.FindFirstValue("Nombre"),
                        Titulo = User.FindFirstValue("Titulo"),
                        Email = User.FindFirstValue("Email")
                    },
                    Rol = User.FindFirstValue("Rol") //_context.Roles.Where(r => r.Id == int.Parse(User.FindFirstValue("Rol"))).FirstOrDefault().Nombre
                };

                if (token == null && !_tokenService.IsTokenValid(_config["Jwt:SecretKey1"].ToString(), _config["Jwt:Issuer"].ToString(), token))
                {
                    return RedirectToAction(nameof(Index));
                }

                _context.Update(_user);
                await _context.SaveChangesAsync();

                if (JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global")) == null)
                    return RedirectToAction(nameof(Index));
            }
            /*
            global.ADMINISTRADOR = _context.Roles.Where(x => x.Nombre.Equals("Administrador")).FirstOrDefault().Id;
            global.SUPERADMIN = _context.Roles.Where(x => x.Nombre.Equals("Superadmin")).FirstOrDefault().Id;
            global.RESPONSABLE_ADC = _context.Roles.Where(x => x.Nombre.Equals("Responsable de la administración de cambio")).FirstOrDefault().Id;
            global.RESPONSABLE_PREARRANQUE = _context.Roles.Where(x => x.Nombre.Equals("Responsable de la revisión de seguridad del pre-arranque")).FirstOrDefault().Id;
            global.SUPLENTE = _context.Roles.Where(x => x.Nombre.Equals("Suplente")).FirstOrDefault().Id;
            global.LIDER_EQUIPO_VERIFICADOR = _context.Roles.Where(x => x.Nombre.Equals("Lider de equipo verificador")).FirstOrDefault().Id;
            global.EQUIPO_VERIFICADOR = _context.Roles.Where(x => x.Nombre.Equals("Equipo verificador")).FirstOrDefault().Id;
            global.EMPLEADO = _context.Roles.Where(x => x.Nombre.Equals("Empleado")).FirstOrDefault().Id;
            */

            //catalogos
            global.roles = _context.Roles.Where(r => r.Id != 1).ToList();            
            global.puestos = _context.Puestos.ToList();            
            global.residencias = _context.Residencias.ToList();

            global.lideres = _context.Usuarios.Where(u => u.Id_Rol == global.LIDER_EQUIPO_VERIFICADOR && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
            global.responsablesADC = _context.Usuarios.Where(u => u.Id_Rol == global.RESPONSABLE_ADC && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
            global.responsablesPreArranque = _context.Usuarios.Where(u => u.Id_Rol == global.RESPONSABLE_PREARRANQUE && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
            global.suplentes = _context.Usuarios.Where(u => u.Id_Rol == global.SUPLENTE && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();
            global.equipo_verificador = _context.Usuarios.Where(u => u.Id_Rol == global.EQUIPO_VERIFICADOR && u.Id != int.Parse(User.FindFirstValue("Id"))).ToList();

            global.anexos = _context.ADC_Anexos.ToList();
            global.vista_actividadesADC = _context.ADC_Actividades.ToList();
            global.vista_actividadesPreArranque = _context.PreArranque_Actividades.ToList();

            //global.lista_proyectos_adc = _context.Proyectos.Where(p => p.Estado_ADC)
            
            global.session = "LogIn";
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            ViewBag.global = global;
            return View();
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

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            var _user = JsonConvert.DeserializeObject<Usuarios>(HttpContext.Session.GetString("User"));
            _user.Token = null;
            global.session_usuario.user.Token = null;
            _context.Update(_user);
            await _context.SaveChangesAsync();

            //var x = HttpContext.Session.GetString("Token");
            global.session = null;
            global.usuario.user = null;

            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult AccountSettings()
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            ViewBag.global = global;
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            return View((Usuarios)global.session_usuario.user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountSettings(Usuarios user)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            /*actualiza tabla de usuarios*/
            user.Id = int.Parse(User.FindFirstValue("Id"));
            Usuarios consultaUsuario = _context.Usuarios.Find(int.Parse(User.FindFirstValue("Id")));
            consultaUsuario.Nombre = user.Nombre;
            consultaUsuario.Paterno = user.Paterno;
            consultaUsuario.Materno = user.Materno;
            consultaUsuario.Email = user.Email;
            consultaUsuario.Id_Rol = user.Id_Rol;
            consultaUsuario.Observaciones = user.Observaciones;
            _context.Update(consultaUsuario);
            await _context.SaveChangesAsync();
            global.session_usuario.user = consultaUsuario;
            ViewBag.global = global;
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            return RedirectToAction(nameof(AccountSettings));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(Usuarios user)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));

            if (user.Password.Equals(global.session_usuario.user.Password) &&
                user.Nueva_Password.Equals(user.Confirmar_Password))
            {
                
                /*actualiza tabla de usuarios*/
                user.Id = int.Parse(User.FindFirstValue("Id"));
                Usuarios consultaUsuario = _context.Usuarios.Find(int.Parse(User.FindFirstValue("Id")));
                consultaUsuario.Password = user.Nueva_Password;
                _context.Update(consultaUsuario);
                await _context.SaveChangesAsync();
                global.session_usuario.user = consultaUsuario;
            }
            ViewBag.global = global;
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            return RedirectToAction(nameof(AccountSettings));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Notificaciones(Usuarios user)
        {
            global = JsonConvert.DeserializeObject<Global>(HttpContext.Session.GetString("Global"));
            /*actualiza notificaciones de usuarios*/
            user.Id = int.Parse(User.FindFirstValue("Id"));
            Usuarios consultaUsuario = _context.Usuarios.Find(int.Parse(User.FindFirstValue("Id")));
            consultaUsuario.Notificacion_Tarea = user.Notificacion_Tarea;
            consultaUsuario.Notificacion_Proyecto = user.Notificacion_Proyecto;
            consultaUsuario.Notificacion_ADC = user.Notificacion_ADC;
            _context.Update(consultaUsuario);
            await _context.SaveChangesAsync();
            global.session_usuario.user = consultaUsuario;
            ViewBag.global = global;
            HttpContext.Session.SetString("Global", JsonConvert.SerializeObject(global));
            return RedirectToAction(nameof(AccountSettings));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
