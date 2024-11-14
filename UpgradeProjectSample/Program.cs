using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using UpgradeProjectSample.Books;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users;
using UpgradeProjectSample.Users.Models;
using UpgradeProjectSample.Users.Repositories;

var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    })
    .UseNLog();
    builder.Services.AddRazorPages();
    builder.Services.AddDbContext<SampleContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("BookShelf")));
            
    // JWTを使った認証.
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
            };
        });
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // ReferenceLoopを切る.
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
        .AddUserStore<ApplicationUserStore>()
        .AddEntityFrameworkStores<SampleContext>()
        .AddDefaultTokenProviders();
    builder.Services.AddScoped<IUserTokens, UserTokens>();
    builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
    builder.Services.AddScoped<IApplicationUsers, ApplicationUsers>();
    builder.Services.AddScoped<ILanguages, Languages>();
    builder.Services.AddScoped<IAuthors, Authors>();
    builder.Services.AddScoped<IBooks, UpgradeProjectSample.Books.Books>();
    builder.Services.AddScoped<ISearchBooks, SearchBooks>();
    builder.Services.AddScoped<IBookService, BookService>();
    
    var app = builder.Build();
    app.UseStaticFiles();
    
    if (builder.Environment.EnvironmentName == "Development")
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
    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    logger.Error(ex, "Stopped program because of exception");
}
finally
{
    NLog.LogManager.Shutdown();
}
