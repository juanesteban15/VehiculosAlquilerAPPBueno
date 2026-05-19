using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IMarcaRepository: IRepository<Marca, int>
    {
        Task<Marca?> GetByNombreAsync(string nombre);
    }
}
