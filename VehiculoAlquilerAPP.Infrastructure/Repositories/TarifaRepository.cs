// ============================================================
// ARCHIVO: Repositories/TarifaRepository.cs
// ============================================================
// Implementa ITarifaRepository
// int porque el ID de Tarifa es autoincremental

using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class TarifaRepository : Repository<Tarifa, int>, ITarifaRepository
    {
        private readonly AlquilerDbContext _context;

        public TarifaRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Tarifa?> GetActivaPorPlacaAsync(string placa)
        {
            // Busca la tarifa activa del vehículo
            // Activa == true porque una tarifa nueva nace activa
            return await _context.Tarifas
                .Include(t => t.Vehiculo)
                .FirstOrDefaultAsync(t =>
                    t.Vehiculo.Placa == placa.ToUpper().Trim() &&
                    t.Activa == true);
        }

        public async Task<(List<Tarifa> items, int totalCount)> GetPagedListAsync(
            PaginationRequest request,
            decimal? precioMaximo,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Tarifa> query = _context.Tarifas
                .Include(t => t.Vehiculo)
                .AsQueryable();

            // Solo filtra por precio si viene el filtro
            if (precioMaximo.HasValue)
                query = query.Where(t => t.PrecioPorDia <= precioMaximo.Value);

            int totalCount = await query.CountAsync(cancellationToken);

            List<Tarifa> items = await query
                .OrderBy(t => t.PrecioPorDia) // Ordena de menor a mayor precio
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}