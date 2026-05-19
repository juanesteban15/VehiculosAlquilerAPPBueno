using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface ITipoVehiculoRepository : IRepository<TipoVehiculo, int>
    {
        // GetByIdAsync ya lo heredas, no lo repitas
        // GetListAsync ya lo heredas, úsalo para llenar selects

        // Solo agregas lo verdaderamente específico de TipoVehiculo:
        Task<TipoVehiculo?> GetByNombreAsync(string nombre);
    }
}