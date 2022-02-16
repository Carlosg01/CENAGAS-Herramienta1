using AppCenagas_v2.Models;
using AppCenagas_v2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenagas_v2.Controllers
{
   // [Route("api/[controller]")]
    //[ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IMailService _mailService;
        private IConfiguration _configuration;

        public AuthController(IUserService userService, IMailService mailService, IConfiguration configuration)
        {
            _userService = userService;
            _mailService = mailService;
            _configuration = configuration;
        }
        
        /*public IActionResult ForgotPassword()
        {           
            return View();
        }*/

        // api/auth/forgetpassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(Usuario user)
        {
            if (string.IsNullOrEmpty(user.Email))
                return NotFound();

            //return Content(user.Email.ToString());

            var result = await _userService.ForgetPasswordAsync(user.Email.ToString());

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] Usuario model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
