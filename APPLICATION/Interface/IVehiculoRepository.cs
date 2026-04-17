
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IVehiculoRepository
    {
        void Guardar(Vehiculo vehiculo);
        Vehiculo ObtenerPorPlaca(string Placa);
        void Actualizar(Vehiculo vehiculo);
    }
}