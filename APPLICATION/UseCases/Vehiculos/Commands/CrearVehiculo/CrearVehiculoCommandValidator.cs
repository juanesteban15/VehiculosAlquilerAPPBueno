using FluentValidation;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CrearVehiculo
{
    public class CrearVehiculoCommandValidator : AbstractValidator<CrearVehiculoCommand>
    {
        public CrearVehiculoCommandValidator()
        {
            RuleFor(c => c.Placa)
                .NotEmpty().WithMessage("La placa es obligatoria.")
                .MaximumLength(10).WithMessage("La placa no puede superar 10 caracteres.");

            RuleFor(c => c.Modelo)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .WithMessage($"El modelo debe estar entre 1900 y {DateTime.Now.Year + 1}.");

            RuleFor(c => c.PropietarioId)
                .GreaterThan(0).WithMessage("El propietario es obligatorio.");

            RuleFor(c => c.MarcaId)
    .GreaterThan(0).WithMessage("La marca es obligatoria.");

            RuleFor(c => c.CategoriaId)
                .GreaterThan(0).WithMessage("La categoría es obligatoria.");

            RuleFor(c => c.TipoVehiculoId)
                .GreaterThan(0).WithMessage("El tipo de vehículo es obligatorio.");

            RuleFor(c => c.CombustibleId)
                .GreaterThan(0).WithMessage("El tipo de combustible es obligatorio.");

            RuleFor(c => c.TransmisionId)
                .GreaterThan(0).WithMessage("El sistema de transmisión es obligatorio.");

            RuleFor(c => c.EstadoVehiculoId)
                .GreaterThan(0).WithMessage("El estado del vehículo es obligatorio.");
        }
    }
}