using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilEditar
{
    public class GetPerfilEditarQuery : IRequest<PerfilEdicionDto?>
    {
        public required int UsuarioId { get; set; }
    }
}
