using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IVehiculoRepository : IRepository<Vehiculo, string>
    {
        Task<Vehiculo?> GetByPlacaAsync(string placa);

        Task<(List<Vehiculo> items, int totalCount)>
            GetPagedListAsync(PaginationRequest request,
            string? placaFilter, CancellationToken cancellationToken = default);
    }
}
