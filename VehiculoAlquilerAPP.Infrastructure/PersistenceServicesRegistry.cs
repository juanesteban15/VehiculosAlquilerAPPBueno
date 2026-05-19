// ============================================================
// ARCHIVO: PersistenceServicesRegistry.cs
// ============================================================
// Registra todos los servicios de Infrastructure en el contenedor
// Program.cs lo llama con una sola línea: AddPersistenceServices()
// Así Program.cs no necesita saber qué hay adentro

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Queries.Chat;
using VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones;
using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Infrastructure.Persistence;
using VehiculosAlquilerApp.Infrastructure.Persistence.Repositories;
using VehiculosAlquilerApp.Infrastructure.Persistence.Seeding;
using VehiculosAlquilerApp.Infrastructure.Persistence.UnitOfWorks;
using VehiculosAlquilerApp.Infrastructure.Queries;

namespace VehiculosAlquilerApp.Infrastructure
{
    public static class PersistenceServicesRegistry
    {
        public static IServiceCollection AgregarInfraestructura(
            this IServiceCollection services, string connectionString)
        {
            // Registra el DbContext con SQL Server
            // "name=AlquilerConnectionString" busca la cadena en appsettings.json
            services.AddDbContext<AlquilerDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Scoped = una instancia por request HTTP
            // Cada request tiene su propio UnitOfWork y sus propios repositorios

            // UnitOfWork — quien ejecuta el SaveChangesAsync
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

            // Repositorios principales
            services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ITarifaRepository, TarifaRepository>();
            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<IConversacionRepository, ConversacionRepository>();
            services.AddScoped<IMensajeChatRepository, MensajeChatRepository>();
            services.AddScoped<IColorVehiculoRepository, ColorVehiculoRepository>();
            services.AddScoped<IVehiculoFotoRepository, VehiculoFotoRepository>();
            services.AddScoped<IDocumentoVehiculoArchivoRepository, DocumentoVehiculoArchivoRepository>();
            services.AddScoped<IDocumentoVerificacionUsuarioRepository, DocumentoVerificacionUsuarioRepository>();
            services.AddScoped<IComentarioUsuarioRepository, ComentarioUsuarioRepository>();

            // Servicios de lectura para pantallas Razor
            services.AddScoped<IVehiculoReadService, VehiculoReadService>();
            services.AddScoped<IReservaReadService, ReservaReadService>();
            services.AddScoped<INotificacionReadService, NotificacionReadService>();
            services.AddScoped<IChatReadService, ChatReadService>();
            services.AddScoped<IPerfilReadService, PerfilReadService>();

            // Repositorios de catálogo
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IEstadoVehiculoRepository, EstadoVehiculoRepository>();
            services.AddScoped<IEstadoReservaRepository, EstadoReservaRepository>();
            services.AddScoped<IEstadoUsuarioRepository, EstadoUsuarioRepository>();
            services.AddScoped<ITipoVehiculoRepository, TipoVehiculoRepository>();
            services.AddScoped<ITipoCombustibleRepository, TipoCombustibleRepository>();
            services.AddScoped<ISistemaTransmisionRepository, SistemaTransmisionRepository>();
            services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();

            // Transient = nueva instancia cada vez que se pide
            // SeedDb solo se usa una vez al arrancar, no necesita Scoped
            services.AddTransient<SeedDb>();

            return services;
        }
    }
}
