using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IColorVehiculoRepository : IRepository<ColorVehiculo, int>
    {
        Task<List<ColorVehiculo>> GetActivosPorIdsAsync(IEnumerable<int> ids);
    }
}
