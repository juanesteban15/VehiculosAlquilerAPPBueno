using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface ISistemaTransmisionRepository: IRepository<SistemaTransmision, int>
    {
      Task<SistemaTransmision?> GetByNombreAsync(string nombre);
    }
}