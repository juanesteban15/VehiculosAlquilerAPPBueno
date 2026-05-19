using FluentValidation;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CambiarEstadoVehiculo
{
    public class CambiarEstadoVehiculoCommandValidator
        : AbstractValidator<CambiarEstadoVehiculoCommand>
    {
        public CambiarEstadoVehiculoCommandValidator()
        {
            RuleFor(c => c.Placa)
                .NotEmpty().WithMessage("La placa es obligatoria.")
                .MaximumLength(10).WithMessage("La placa no puede superar 10 caracteres.");

            RuleFor(c => c.NuevoEstado)
                .NotEmpty().WithMessage("El nuevo estado es obligatorio.");
        }
    }
}