using AppCenagas_v2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using AppCenagas_v2.Data;

namespace AppCenagas_v2.Services
{
    public interface IUserService
    {

        Task<UserManagerResponse> RegisterUserAsync(Usuario model);

        Task<UserManagerResponse> LoginUserAsync(Usuario model);

        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        Task<UserManagerResponse> ResetPasswordAsync(Usuario model);
    }
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        private IMailService _mailService;

        private readonly ApplicationDbContext _context;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, 
            IMailService mailService, ApplicationDbContext context)
        {
            _userManger = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _context = context;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(Usuario model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserManagerResponse> LoginUserAsync(Usuario model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            Usuario user = (from u in _context.Usuario
                             where u.Email == email
                             select u).FirstOrDefault<Usuario>();

            //var user = await _userManger.FindByEmailAsync(email);
            /*return new UserManagerResponse
            {
                IsSuccess = false,
                Message = user.Email,
                //Message = "No user associated with email",
            };*/

            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            //var token = await _userManger.GeneratePasswordResetTokenAsync(user);
            //var encodedToken = Encoding.UTF8.GetBytes(token);
            //var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}";// &token={validToken}";

            await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }       

        public async Task<UserManagerResponse> ResetPasswordAsync(Usuario model)
        {
            //var user = await _userManger.FindByEmailAsync(model.Email);
            Usuario user = (from u in _context.Usuario
                            where u.Email == model.Email
                            select u).FirstOrDefault<Usuario>();
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Password doesn't match its confirmation",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            //var result = await _userManger.ResetPasswordAsync(user, normalToken, model.Password);

            /*if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };*/

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                //Errors = result.Errors.Select(e => e.Description),
            };
        }
    }
}
