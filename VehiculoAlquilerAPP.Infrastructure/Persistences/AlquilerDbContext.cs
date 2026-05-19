using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Infrastructure.Persistence
{
    public class AlquilerDbContext : DbContext
    {
        public AlquilerDbContext(DbContextOptions<AlquilerDbContext> options) : base(options) { }

        // Entidades principales
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<DocumentoVerificacionUsuario> DocumentosVerificacionUsuario { get; set; }
        public DbSet<VehiculoFoto> VehiculoFotos { get; set; }
        public DbSet<DocumentoVehiculoArchivo> DocumentosVehiculo { get; set; }
        public DbSet<ComentarioUsuario> ComentariosUsuario { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Conversacion> Conversaciones { get; set; }
        public DbSet<MensajeChat> MensajesChat { get; set; }

        // Catálogo
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstadoVehiculo> EstadosVehiculo { get; set; }
        public DbSet<EstadoReserva> EstadosReserva { get; set; }
        public DbSet<EstadoUsuario> EstadosUsuario { get; set; }
        public DbSet<TipoVehiculo> TiposVehiculo { get; set; }
        public DbSet<TipoCombustible> TiposCombustible { get; set; }
        public DbSet<SistemaTransmision> SistemasTransmision { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<ColorVehiculo> Colores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Carga automáticamente todos los archivos *Config.cs
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AlquilerDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
