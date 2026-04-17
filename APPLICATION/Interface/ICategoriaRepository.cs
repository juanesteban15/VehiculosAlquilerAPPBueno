using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface ICategoriaRepository
    {
        Categoria ObtenerPorId(int Id);
    }
}