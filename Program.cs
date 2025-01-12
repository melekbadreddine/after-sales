using after_sales.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Ajouter le service de session
builder.Services.AddDistributedMemoryCache();  // Utilise la m�moire pour stocker la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // D�finir la dur�e de vie de la session
    options.Cookie.HttpOnly = true;  // Emp�che l'acc�s � la session via JavaScript
    options.Cookie.IsEssential = true;  // Rendre le cookie essentiel
});

// Ajout des services n�cessaires � la base de donn�es
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDBConnection")));
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

// Ajout de la gestion de l'identit� pour le Client
builder.Services.AddIdentity<Client, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

// Ajouter l'authentification pour les responsables

builder.Services.AddAuthentication("ResponsableSAV");


// Ajouter les contr�leurs avec les vues
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

// Utiliser la session
app.UseSession();  // Active l'utilisation des sessions

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
