using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{

    public interface IMetodoPagoRepository: IRepository<MetodoPago, int>
    {   
        Task<MetodoPago?> GetByNombreAsync(string nombre);
    }

}