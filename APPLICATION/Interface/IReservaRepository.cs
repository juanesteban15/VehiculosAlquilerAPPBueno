using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IReservaRepository
    {
        void Guardar(Reserva reserva);
        Reserva ObtenerPorId(int Id);
    }
}