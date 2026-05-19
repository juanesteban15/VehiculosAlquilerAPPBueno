using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.ActualizarPerfil
{
    public class ActualizarPerfilCommand : IRequest
    {
        public required int UsuarioId { get; set; }
        public required string Pais { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public ArchivoSubida? FotoPerfil { get; set; }
        public ArchivoSubida? DocumentoIdentidad { get; set; }
        public ArchivoSubida? LicenciaConduccion { get; set; }
    }
}
