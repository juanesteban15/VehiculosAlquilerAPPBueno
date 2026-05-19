using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IEstadoUsuarioRepository: IRepository<EstadoUsuario, int>
    {
        Task<EstadoUsuario?> GetByNombreAsync(string nombre);

    }
}