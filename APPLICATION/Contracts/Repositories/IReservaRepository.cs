using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IReservaRepository : IRepository<Reserva, int>
    {
        Task<Reserva?> GetByIdConDetalleAsync(int id);

        Task<(List<Reserva> items, int totalCount)> GetPagedListAsync(
            PaginationRequest request,
            string? placaFilter,        // filtrar por placa del vehículo
            int? usuarioId,             // filtrar por usuario
            CancellationToken cancellationToken = default);
    }
}
