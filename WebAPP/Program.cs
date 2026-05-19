using VehiculosAlquilerApp.Application;
using VehiculosAlquilerApp.Infrastructure;
using VehiculosAlquilerApp.Infrastructure.Persistence.Seeding;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using WebAPP.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IArchivoStorageService, LocalArchivoStorageService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AgregarInfraestructura(
    builder.Configuration.GetConnectionString("AlquilerConnectionString")!);

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    SeedDb seedDb = scope.ServiceProvider.GetRequiredService<SeedDb>();
    await seedDb.SeedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
