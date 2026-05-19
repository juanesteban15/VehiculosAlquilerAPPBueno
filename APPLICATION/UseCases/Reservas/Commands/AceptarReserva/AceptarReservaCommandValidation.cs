using FluentValidation;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.AceptarReserva
{
    public class AceptarReservaCommandValidator
        : AbstractValidator<AceptarReservaCommand>
    {
        public AceptarReservaCommandValidator()
        {
            RuleFor(c => c.ReservaId)
                .GreaterThan(0).WithMessage("La reserva es obligatoria.");

            RuleFor(c => c.DuenioId)
                .GreaterThan(0).WithMessage("El dueño es obligatorio.");
        }
    }
}