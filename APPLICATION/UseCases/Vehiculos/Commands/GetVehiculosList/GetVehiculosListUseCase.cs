using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.GetVehiculosList
{
    public class GetVehiculosListUseCase
        : IRequestHandler<GetVehiculosListQuery, PaginationResponse<VehiculoListItemDTO>>
    {
        private readonly IVehiculoRepository _repo;

        public GetVehiculosListUseCase(IVehiculoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PaginationResponse<VehiculoListItemDTO>> Handle(
            GetVehiculosListQuery query) // ✅ recibe la query correcta
        {
            (List<Vehiculo> vehiculos, int totalCount) = await _repo.GetPagedListAsync(
                query.Pagination,
                query.PlacaFilter);

            List<VehiculoListItemDTO> dtos = vehiculos.Select(v => new VehiculoListItemDTO
            {
                Placa = v.Placa,
                Marca = v.Marca.Nombre,
                TipoVehiculo = v.TipoVehiculo.Nombre,
                Modelo = v.Modelo,
                Estado = v.Estado.Nombre
            }).ToList();

            return PaginationResponse<VehiculoListItemDTO>.Create(
                dtos, totalCount, query.Pagination);
        }
    }
}