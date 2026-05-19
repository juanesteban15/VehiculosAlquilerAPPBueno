using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.RegistrarDocumentoUsuario
{
    public class RegistrarDocumentoUsuarioCommand : IRequest
    {
        public required int UsuarioId { get; set; }
        public required string TipoDocumento { get; set; }
        public required ArchivoSubida Archivo { get; set; }
    }
}
