using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface ITipoCombustibleRepository: IRepository<TipoCombustible, int>
    {
        Task<TipoCombustible?> GetByNombreAsync(string nombre);

    }
}