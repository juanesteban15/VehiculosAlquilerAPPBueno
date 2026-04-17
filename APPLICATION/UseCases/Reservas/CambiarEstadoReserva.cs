using System;
using VehiculosAlquilerApp.Application.Interface;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas
{
    public class CambiarEstadoReserva
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IEstadoReservaRepository _estadoRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public CambiarEstadoReserva(
            IReservaRepository reservaRepo,
            IEstadoReservaRepository estadoRepo,
            IUsuarioRepository usuarioRepo
        )
        {
            _reservaRepo = reservaRepo;
            _estadoRepo = estadoRepo;
            _usuarioRepo = usuarioRepo;
        }

        public void Ejecutar(int reservaId, string nombreNuevoEstado)
        {
            // 1. OBTENER RESERVA PRIMERO
            var reserva = _reservaRepo.ObtenerPorId(reservaId);

            // 2. VALIDAR INMEDIATAMENTE
            if (reserva == null)
                throw new Exception($"La reserva con ID {reservaId} no existe.");

            // 3. OBTENER EL USUARIO (Seguro, porque ya validamos la reserva)
            // Asegúrate de que tu repositorio de Reserva traiga al Cliente cargado
            var usuario = _usuarioRepo.ObtenerPorId(reserva.Cliente.Id);

            if (usuario == null)
                throw new Exception("El cliente asociado a la reserva no pudo ser encontrado.");

            // 4. OBTENER EL ESTADO
            var estado = _estadoRepo.ObtenerPorNombre(nombreNuevoEstado);

            if (estado == null)
                throw new Exception($"El estado '{nombreNuevoEstado}' no es un estado válido.");

            // 5. CAMBIAR ESTADO (DOMAIN)
            reserva.CambiarEstado(usuario, estado);

            // 6. GUARDAR CAMBIOS
            _reservaRepo.Guardar(reserva);
        }
    }
}
