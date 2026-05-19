using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IEstadoVehiculoRepository: IRepository<EstadoVehiculo, int>
    {
        Task<EstadoVehiculo?> GetByNombreAsync(string nombre);
    }

}
