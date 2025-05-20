using Microsoft.EntityFrameworkCore;
using ElQueHacer.Data;

var builder = WebApplication.CreateBuilder(args);

// Leer string de conexión desde appsettings o variables de entorno
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMySQL")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnectionMySQL' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Leer puerto desde variable de entorno (Heroku)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Configurar pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Puedes quitarlo si Heroku no reenvía HTTPS
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
