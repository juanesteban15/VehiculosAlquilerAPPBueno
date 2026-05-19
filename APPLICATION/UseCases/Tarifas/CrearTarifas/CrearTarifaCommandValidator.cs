using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario;

namespace VehiculosAlquilerApp.Application.UseCases.Tarifas.CrearTarifas
{
    public class CrearTarifaCommandValidator : AbstractValidator<CrearTarifaCommand>
    {
        public CrearTarifaCommandValidator()
        {
            RuleFor(c => c.PlacaId)
                .NotEmpty().WithMessage("La placa es obligatoria.").
                MaximumLength(10).WithMessage("La placa no puede superar 10 caracteres.");


            RuleFor(c => c.PrecioPorDia)
                .NotEmpty().WithMessage("El precio por día es obligatorio.")
                .GreaterThan(0).WithMessage("El precio por día debe ser mayor que cero.");

            RuleFor(c => c.FechaInicio)
                .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
                .GreaterThan(DateTime.Now).WithMessage("La fecha de inicio no puede ser pasada.");  

            RuleFor(c => c.FechaFin)
                .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
                .GreaterThan(c => c.FechaInicio).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");

            // etc
        }
    }
}
