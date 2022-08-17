using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SistemaCenagas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
               Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));



            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(5);
                //options.IdleTimeout = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
                
            });

            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Home/Index";
                options.LoginPath = "/Home";
                options.LogoutPath = "/Home/LogOut";
                options.SlidingExpiration = true;
            });
            

            /*services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });*/



            //services.AddSession();
            services.AddControllersWithViews();
            
            /*
            services.AddMvc(options => {
                options.MaxModelBindingCollectionSize = 8000;
            });
            */

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddMvc().AddRazorRuntimeCompilation();


            //-----------AUTENTICACION DE USUARIO----------

            //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            /*
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });*/
            //services.AddControllers();
            
            services.AddTransient<ITokenService, TokenService>();
            
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Home";
                options.LogoutPath = "/Home/LogOut";
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey1"]))
                };
            });

            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IUserRepository, UserRepository>();




            //services.Configure<FormOptions>(options => options.ValueCountLimit = 5000);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.Use(async (context, next) =>
            {
                //var response = context.Response;
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                /*
                else //if(context.Session == null)
                {
                    context.Response.Redirect("/Home/");
                }
                */
                await next();
            });
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                        response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Home");
            });

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
