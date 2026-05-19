using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilPublico
{
    public class GetPerfilPublicoQuery : IRequest<PerfilPublicoResult>
    {
        public required int UsuarioId { get; set; }
    }
}
