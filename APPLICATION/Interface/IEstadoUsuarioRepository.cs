using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IEstadoUsuarioRepository
    {
        EstadoUsuario ObtenerPorId(int Id);
        EstadoUsuario ObtenerPorNombre(string Nombre);

    }
}