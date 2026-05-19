// ============================================================
// ARCHIVO: Repositories/ReservaRepository.cs
// ============================================================
// Implementa IReservaRepository
// int porque el ID de Reserva es autoincremental

using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class ReservaRepository : Repository<Reserva, int>, IReservaRepository
    {
        private readonly AlquilerDbContext _context;

        public ReservaRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Reserva?> GetByIdConDetalleAsync(int id)
        {
            return _context.Reservas
                .Include(r => r.Vehiculo).ThenInclude(v => v.Propietario)
                .Include(r => r.Vehiculo).ThenInclude(v => v.Marca)
                .Include(r => r.Cliente)
                .Include(r => r.Estado)
                .Include(r => r.MetodoPago)
                .Include(r => r.Tarifa)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<(List<Reserva> items, int totalCount)> GetPagedListAsync(
            PaginationRequest request,
            string? placaFilter,
            int? usuarioId,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Reserva> query = _context.Reservas
                .Include(r => r.Vehiculo)
                    .ThenInclude(v => v.Marca)  // Include anidado — trae la Marca del Vehículo
                .Include(r => r.Cliente)
                .Include(r => r.Estado)
                .Include(r => r.MetodoPago)
                .Include(r => r.Tarifa)
                .AsQueryable();

            // Filtra por placa si viene
            if (!string.IsNullOrWhiteSpace(placaFilter))
                query = query.Where(r => r.Vehiculo.Placa.Contains(placaFilter.Trim().ToUpper()));

            // Filtra por usuario si viene
            if (usuarioId.HasValue)
                query = query.Where(r => r.Cliente.Id == usuarioId.Value);

            int totalCount = await query.CountAsync(cancellationToken);

            List<Reserva> items = await query
                .OrderByDescending(r => r.FechaCreacion) // Las más recientes primero
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
