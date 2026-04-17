using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IEstadoVehiculoRepository
    {
        EstadoVehiculo ObtenerPorId(int Id);
        EstadoVehiculo ObtenerPorNombre(string Nombre);
    }

}
