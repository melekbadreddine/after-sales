using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDBConnection")));
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddIdentity<Client, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options =>
                   {
                       options.LoginPath = "/Account/Login";  // Définir le chemin pour le login Client
                       options.Cookie.Name = "ClientAuthCookie";
                   });

// Configuration des utilisateurs pour ResponsableSAV (utilisation d'un manager personnalisé)
builder.Services.AddScoped<IResponsableSAV, ResponsableSAV>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "ResponsableSAV"; // Le nom de votre cookie d'authentification
})
.AddCookie("ResponsableSAV", options =>
{
    options.LoginPath = "/Responsable/Login"; // Redirection en cas de non-authentification
    options.LogoutPath = "/Responsable/Logout"; // Redirection après déconnexion
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
