using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IMarcaRepository
    {
        Marca ObtenerPorId(int Id);
        Marca ObtenerPorNombre(string Nombre);
    }
}