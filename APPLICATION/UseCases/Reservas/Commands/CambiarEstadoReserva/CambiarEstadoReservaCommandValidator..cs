using FluentValidation;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CambiarEstadoReserva
{
    public class CambiarEstadoReservaCommandValidator : AbstractValidator<CambiarEstadoReservaCommand>
    {
        public CambiarEstadoReservaCommandValidator()
        {
            RuleFor(c => c.ReservaId)
                .GreaterThan(0).WithMessage("El ID de la reserva debe ser mayor que cero.");

            RuleFor(c => c.NuevoEstado)
                .NotEmpty().WithMessage("El nuevo estado es obligatorio.");

            RuleFor(c => c.UsuarioId)
                .GreaterThan(0).WithMessage("El usuario es obligatorio.");
        }
    }
}
