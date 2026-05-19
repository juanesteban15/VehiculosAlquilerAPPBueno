using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface ICategoriaRepository :IRepository<Categoria, int>
    {

        Task<Categoria?> GetByNombreAsync(string nombre);
        
    }
}