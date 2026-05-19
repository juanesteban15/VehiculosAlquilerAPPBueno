
using FluentValidation;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CrearReserva
{
    public class CrearReservaCommandValidator : AbstractValidator<CrearReservaCommand>
    {
        public CrearReservaCommandValidator()
        {
            RuleFor(x => x.Placa)
                .NotEmpty().WithMessage("La placa del vehículo es obligatoria.")
                .MaximumLength(10).WithMessage("La placa no puede superar 10 caracteres.");

            RuleFor(x => x.UsuarioId)
                .GreaterThan(0).WithMessage("El usuario es obligatorio.");

            RuleFor(x => x.MetodoPagoId)
                .GreaterThan(0).WithMessage("El método de pago es obligatorio.");

            RuleFor(x => x.FechaInicio)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("La fecha de inicio debe ser hoy o una fecha futura.");

            RuleFor(x => x.FechaFin)
                .GreaterThan(x => x.FechaInicio)
                .WithMessage("La fecha de fin debe ser después de la fecha de inicio.");
        }
    }
}
