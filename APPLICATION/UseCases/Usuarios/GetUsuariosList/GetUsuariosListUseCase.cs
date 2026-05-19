using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.GetUsuariosList
{
    public class GetUsuariosListUseCase
        : IRequestHandler<GetUsuariosListQuery, PaginationResponse<UsuarioListItemDTO>>
    {
        private readonly IUsuarioRepository _repo; 

        public GetUsuariosListUseCase(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PaginationResponse<UsuarioListItemDTO>> Handle(GetUsuariosListQuery query)
        {
            (List<Usuario> usuarios, int totalCount) = await _repo.GetPagedListAsync(
                query.Pagination,
                query.NombreFilter);

            List<UsuarioListItemDTO> dtos = usuarios.Select(u => new UsuarioListItemDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Pais = u.Pais,
                Estado = u.Estado.Nombre
            }).ToList();

            return PaginationResponse<UsuarioListItemDTO>.Create(dtos, totalCount, query.Pagination);
        }
    }
}