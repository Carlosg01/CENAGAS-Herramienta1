using AppCenagas_v2.Data;
using AppCenagas_v2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext _context)
        {
            context = _context;            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario user)
        {
            IEnumerable<Usuario> users = await context.Usuario.ToListAsync();
            foreach(Usuario u in users)
            {
                if(u.Email.Equals(user.Email) && u.Password.Equals(user.Password))
                {
                    return RedirectToAction(nameof(Dashboard));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard()
        {
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
                context.Add(user);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
