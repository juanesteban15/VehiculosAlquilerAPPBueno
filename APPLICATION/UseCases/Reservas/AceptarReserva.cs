using VehiculosAlquilerApp.Application.Interface;
using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas
{
    public class AceptarReserva
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEstadoReservaRepository _estadoRepo;

        public AceptarReserva(IReservaRepository reservaRepo, IUsuarioRepository usuarioRepo, IEstadoReservaRepository estadoRepo)
        {
            _reservaRepo = reservaRepo;
            _usuarioRepo = usuarioRepo;
            _estadoRepo = estadoRepo;
        }

        public void Ejecutar(int reservaId, int duenioId)
        {
            // 1. Validar que la reserva exista
            var reserva = _reservaRepo.ObtenerPorId(reservaId)
                ?? throw new Exception($"No se encontró la reserva con ID {reservaId}.");

            // 2. Validar que el dueño/usuario exista
            var duenio = _usuarioRepo.ObtenerPorId(duenioId)
                ?? throw new Exception($"El usuario con ID {duenioId} no existe.");

            // 3. Obtener el estado del catálogo (Ojo con el nombre de la constante)
            var estadoConfirmado = _estadoRepo.ObtenerPorNombre(EstadoReserva.Confirmada)
                ?? throw new Exception("Error interno: El estado 'CONFIRMADA' no está configurado en el sistema.");

            // 4. Lógica de Negocio (Aquí el Dominio valida si el dueño es realmente el dueño del vehículo)
            reserva.Aceptar(duenio, estadoConfirmado);

            // 5. Persistir cambios
            // Si usas EF, recuerda que si el objeto ya viene rastreado, a veces basta con SaveChanges()
            _reservaRepo.Guardar(reserva);
        }
    }
}