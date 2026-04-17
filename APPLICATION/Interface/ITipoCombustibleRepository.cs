using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface ITipoCombustibleRepository
    {
        TipoCombustible ObtenerPorId(int Id);
        TipoCombustible ObtenerPorNombre(string Nombre);
    }
}