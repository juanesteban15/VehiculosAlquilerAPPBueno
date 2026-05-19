using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface ITarifaRepository : IRepository<Tarifa, int>
    {
        // Lo verdaderamente específico de Tarifa:

        // Buscar la tarifa activa de un vehículo específico
        // (necesaria para calcular el costo de una reserva)
        Task<Tarifa?> GetActivaPorPlacaAsync(string placa);

        // Listar tarifas con paginación y filtro de precio
        Task<(List<Tarifa> items, int totalCount)> GetPagedListAsync(
            PaginationRequest request,
            decimal? precioMaximo,     // filtro opcional por precio máximo
            CancellationToken cancellationToken = default);
    }
}