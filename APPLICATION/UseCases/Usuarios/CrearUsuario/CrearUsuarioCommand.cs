using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario
{
    public class CrearUsuarioCommand : IRequest<int>
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Pais { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public string TipoCuenta { get; set; } = "Persona";
        public string? NombreEmpresa { get; set; }
        public string? NitEmpresa { get; set; }

        // Clase pequeña dentro del mismo archivo
        // representa claramente qué es un teléfono
        public List<TelefonoDTO> Telefonos { get; set; } = [];
    }

    // Va en el mismo archivo, no necesita su propio archivo
    // porque solo se usa aquí
    public class TelefonoDTO
    {
        public required string Numero { get; set; }
        public required string Tipo { get; set; }
    }
}
