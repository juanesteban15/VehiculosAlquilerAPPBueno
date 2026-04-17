using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface ISistemaTransmisionRepository
    {
        SistemaTransmision ObtenerPorId(int Id);
        SistemaTransmision ObtenerPorNombre(string Nombre);
    }
}