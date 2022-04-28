using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UpgradeProjectSample.Books;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users;
using UpgradeProjectSample.Users.Models;
using UpgradeProjectSample.Users.Repositories;

namespace UpgradeProjectSample
{
    public class Startup
    {
        private readonly IConfiguration config;
        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<SampleContext>(options =>
                options.UseNpgsql(this.config["DbConnection"]));
            
            // JWTを使った認証.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.config["Jwt:Issuer"],
                        ValidAudience = this.config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(this.config["Jwt:Key"])),
                    };
                });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // System.Text.Jsonだとver.3.XでReferenceLoopを切る方法がない？ためスキップ.
                    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddUserStore<ApplicationUserStore>()
                .AddEntityFrameworkStores<SampleContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IUserTokens, UserTokens>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationUsers, ApplicationUsers>();
            services.AddScoped<ILanguages, Languages>();
            services.AddScoped<IAuthors, Authors>();
            services.AddScoped<IBooks, UpgradeProjectSample.Books.Books>();
            services.AddScoped<ISearchBooks, SearchBooks>();
            services.AddScoped<IBookService, BookService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("user-token");
                if(string.IsNullOrEmpty(token) == false)
                {            
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
                await next();
            });
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    if(context.HttpContext.Request.Path.StartsWithSegments("/") ||
                        context.HttpContext.Request.Path.StartsWithSegments("/pages"))
                    {
                        context.HttpContext.Response.Redirect("/pages/signin");
                    }
                    else
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }            
                    return;
                }
                await context.Next(context.HttpContext);
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
