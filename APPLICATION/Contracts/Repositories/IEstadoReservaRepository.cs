using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IEstadoReservaRepository : IRepository<EstadoReserva, int>
    {
        Task<EstadoReserva?> GetByNombreAsync(string nombre);
    }
}
