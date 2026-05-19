using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.GetReservasList
{
    public class GetReservasListQuery : IRequest<PaginationResponse<ReservaListItemDTO>>
    {
        public PaginationRequest Pagination { get; set; } = null!;
        public string? PlacaFilter { get; set; }  
        public int? UsuarioIdFilter { get; set; }    

    }
}