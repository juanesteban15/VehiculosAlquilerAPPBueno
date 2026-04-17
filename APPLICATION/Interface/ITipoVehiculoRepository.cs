using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface ITipoVehiculoRepository
    {
        TipoVehiculo ObtenerPorId(int Id);
        TipoVehiculo ObtenerPorNombre(string Nombre);
    }
}