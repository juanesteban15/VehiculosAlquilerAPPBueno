using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.GetVehiculosList
{
    public class GetVehiculosListQuery : IRequest<PaginationResponse<VehiculoListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = null!;
        public string? PlacaFilter { get; set; }
    }
}
