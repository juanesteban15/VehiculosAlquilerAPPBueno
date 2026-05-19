using FluentValidation;


namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario
{
    public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El email no es válido.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

        RuleFor(c=> c.Pais)
            .NotEmpty().WithMessage("El país es obligatorio.");

        RuleFor(c=> c.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
            .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser futura.");


        RuleFor(c=> c.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio.");

            // etc
        }
}
}
