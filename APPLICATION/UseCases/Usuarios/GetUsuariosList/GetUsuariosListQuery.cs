using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.GetUsuariosList
{
    public class GetUsuariosListQuery : IRequest<PaginationResponse<UsuarioListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = null!;
        public string? NombreFilter { get; set; }
    }
}
