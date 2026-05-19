// ============================================================
// ARCHIVO: Repositories/VehiculoRepository.cs
// ============================================================
// Implementa IVehiculoRepository usando Entity Framework
// Hereda Repository<Vehiculo, string> para tener el CRUD gratis
// El string es porque el ID del vehículo es la Placa

using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class VehiculoRepository : Repository<Vehiculo, string>, IVehiculoRepository
    {
        private readonly AlquilerDbContext _context;

        // El constructor recibe el contexto y lo pasa al Repository base
        public VehiculoRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vehiculo?> GetByPlacaAsync(string placa)
        {
            // Include trae las relaciones — sin esto vendrían null
            return await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Categoria)
                .Include(v => v.TipoCombustible)
                .Include(v => v.SistemaTransmision)
                .Include(v => v.Estado)
                .Include(v => v.Propietario)
                .Include(v => v.Colores)
                .FirstOrDefaultAsync(v => v.Placa == placa.ToUpper().Trim());
            // ToUpper().Trim() porque la placa se guarda normalizada
        }

        public async Task<(List<Vehiculo> items, int totalCount)> GetPagedListAsync(
            PaginationRequest request,
            string? placaFilter,
            CancellationToken cancellationToken = default)
        {
            // AsQueryable() construye la consulta sin ejecutarla todavía
            IQueryable<Vehiculo> query = _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Estado)
                .AsQueryable();

            // Solo agrega el filtro si viene — si es null no filtra
            if (!string.IsNullOrWhiteSpace(placaFilter))
                query = query.Where(v => v.Placa.Contains(placaFilter.Trim().ToUpper()));

            // Cuenta el total ANTES de paginar — necesario para saber cuántas páginas hay
            int totalCount = await query.CountAsync(cancellationToken);

            // Skip salta los registros anteriores, Take trae solo los de esta página
            List<Vehiculo> items = await query
                .OrderBy(v => v.Placa)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}