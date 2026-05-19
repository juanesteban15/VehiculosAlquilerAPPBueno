// ============================================================
// ARCHIVO: Web/Program.cs
// ============================================================
// Punto de entrada de la aplicación
// Configura todos los servicios y el pipeline HTTP

using VehiculosAlquilerApp.Application;
using VehiculosAlquilerApp.Infrastructure;
using VehiculosAlquilerApp.Infrastructure.Persistence.Seeding;
using VehiculosAlquilerAPP.Web.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// --- SERVICIOS ---

// Sesión — necesaria para que el Middleware guarde el mensaje de error
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // La cookie de sesión no es accesible desde JavaScript
    options.Cookie.HttpOnly = true;
    // La sesión es esencial para el funcionamiento de la app
    options.Cookie.IsEssential = true;
});

// Controllers con Views — cambia de Razor Pages a MVC
// Razor Pages era el default, MVC es lo que usa el profesor
builder.Services.AddControllersWithViews();

// Registra todos los UseCases, Validators y el Mediator
// Este método está en ApplicationServicesRegistry.cs
builder.Services.AddApplicationServices();

// Registra el DbContext, repositorios y UnitOfWork
// Este método está en PersistenceServicesRegistry.cs
// Lee la cadena de conexión de appsettings.json
builder.Services.AgregarInfraestructura(
    builder.Configuration.GetConnectionString("AlquilerConnectionString")!);

// --- PIPELINE ---

WebApplication app = builder.Build();

// Ejecuta las migraciones y el seeding al arrancar
// Crea la base de datos si no existe
// Inserta los datos iniciales del catálogo
using (IServiceScope scope = app.Services.CreateScope())
{
    SeedDb seedDb = scope.ServiceProvider.GetRequiredService<SeedDb>();
    await seedDb.SeedAsync();
}

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();

// Sirve los archivos estáticos (CSS, JS, imágenes)
app.UseStaticFiles();

app.UseRouting();

// La sesión debe estar antes del Middleware de excepciones
// porque el Middleware usa la sesión para guardar el mensaje
app.UseSession();

app.UseAuthorization();

// Middleware de excepciones — captura errores de toda la app
// Debe estar después de UseRouting y UseSession
app.UseExceptionHandlerMiddleware();

// Ruta por defecto — va a HomeController, acción Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
