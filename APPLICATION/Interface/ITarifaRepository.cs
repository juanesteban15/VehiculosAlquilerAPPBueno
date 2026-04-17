using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface ITarifaRepository
    {
        void Guardar(Tarifa tarifa);
        Tarifa ObtenerPorId(int Id);
        Tarifa ObtenerPorPrecioPorDia(decimal PrecioPorDia);
        Tarifa ObtenerActivaPorPlaca(string placa);
    }
}