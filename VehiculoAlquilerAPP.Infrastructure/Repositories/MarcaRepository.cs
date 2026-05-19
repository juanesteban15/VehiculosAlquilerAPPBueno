// ============================================================
// ARCHIVO: Repositories/CatalogoRepository.cs
// ============================================================
// Repositorios para todos los catálogos simples
// Son tan simples que van todos en un solo archivo
// Solo necesitan GetByNombreAsync además del CRUD heredado

using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    // Cada catálogo hereda Repository<SuEntidad, int> e implementa su interfaz

    public class MarcaRepository : Repository<Marca, int>, IMarcaRepository
    {
        private readonly AlquilerDbContext _context;
        public MarcaRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Marca?> GetByNombreAsync(string nombre)
        {
            return await _context.Marcas
                .Include(m => m.TipoVehiculo) // Marca tiene TipoVehiculo
                .FirstOrDefaultAsync(m => m.Nombre == nombre.Trim());
        }

        public override async Task<Marca?> GetByIdAsync(int id)
        {
            return await _context.Marcas
                .Include(m => m.TipoVehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }

    public class CategoriaRepository : Repository<Categoria, int>, ICategoriaRepository
    {
        private readonly AlquilerDbContext _context;
        public CategoriaRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Categoria?> GetByNombreAsync(string nombre)
        {
            return await _context.Categorias
                .Include(c => c.TipoVehiculo)
                .FirstOrDefaultAsync(c => c.Nombre == nombre.Trim());
        }

        public override async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.TipoVehiculo)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }

    public class EstadoVehiculoRepository : Repository<EstadoVehiculo, int>, IEstadoVehiculoRepository
    {
        private readonly AlquilerDbContext _context;
        public EstadoVehiculoRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EstadoVehiculo?> GetByNombreAsync(string nombre)
        {
            return await _context.EstadosVehiculo
                .FirstOrDefaultAsync(e => e.Nombre == nombre.Trim().ToUpper());
        }
    }

    public class EstadoReservaRepository : Repository<EstadoReserva, int>, IEstadoReservaRepository
    {
        private readonly AlquilerDbContext _context;
        public EstadoReservaRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EstadoReserva?> GetByNombreAsync(string nombre)
        {
            return await _context.EstadosReserva
                .FirstOrDefaultAsync(e => e.Nombre == nombre.Trim().ToUpper());
        }
    }

    public class EstadoUsuarioRepository : Repository<EstadoUsuario, int>, IEstadoUsuarioRepository
    {
        private readonly AlquilerDbContext _context;
        public EstadoUsuarioRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EstadoUsuario?> GetByNombreAsync(string nombre)
        {
            return await _context.EstadosUsuario
                .FirstOrDefaultAsync(e => e.Nombre == nombre.Trim().ToUpper());
        }
    }

    public class TipoVehiculoRepository : Repository<TipoVehiculo, int>, ITipoVehiculoRepository
    {
        private readonly AlquilerDbContext _context;
        public TipoVehiculoRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TipoVehiculo?> GetByNombreAsync(string nombre)
        {
            return await _context.TiposVehiculo
                .FirstOrDefaultAsync(t => t.Nombre == nombre.Trim());
        }
    }

    public class TipoCombustibleRepository : Repository<TipoCombustible, int>, ITipoCombustibleRepository
    {
        private readonly AlquilerDbContext _context;
        public TipoCombustibleRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TipoCombustible?> GetByNombreAsync(string nombre)
        {
            return await _context.TiposCombustible
                .Include(t => t.TipoVehiculo)
                .FirstOrDefaultAsync(t => t.Nombre == nombre.Trim().ToUpper());
        }

        public override async Task<TipoCombustible?> GetByIdAsync(int id)
        {
            return await _context.TiposCombustible
                .Include(t => t.TipoVehiculo)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }

    public class SistemaTransmisionRepository : Repository<SistemaTransmision, int>, ISistemaTransmisionRepository
    {
        private readonly AlquilerDbContext _context;
        public SistemaTransmisionRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SistemaTransmision?> GetByNombreAsync(string nombre)
        {
            return await _context.SistemasTransmision
                .Include(s => s.TipoVehiculo)
                .FirstOrDefaultAsync(s => s.Nombre == nombre.Trim().ToUpper());
        }

        public override async Task<SistemaTransmision?> GetByIdAsync(int id)
        {
            return await _context.SistemasTransmision
                .Include(s => s.TipoVehiculo)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }

    public class MetodoPagoRepository : Repository<MetodoPago, int>, IMetodoPagoRepository
    {
        private readonly AlquilerDbContext _context;
        public MetodoPagoRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MetodoPago?> GetByNombreAsync(string nombre)
        {
            return await _context.MetodosPago
                .FirstOrDefaultAsync(m => m.Nombre == nombre.Trim().ToUpper());
        }
    }
}
