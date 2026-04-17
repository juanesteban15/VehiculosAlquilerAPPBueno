using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IEstadoReservaRepository
    {
        EstadoReserva ObtenerPorId(int Id);
        EstadoReserva ObtenerPorNombre(string Nombre);

        void Actualizar(EstadoReserva estadoReserva);

    }
}
