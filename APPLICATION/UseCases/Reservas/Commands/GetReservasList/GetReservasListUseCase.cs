// ARCHIVO 3 — GetReservasListUseCase.cs
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.GetReservasList
{
    public class GetReservasListUseCase
        : IRequestHandler<GetReservasListQuery, PaginationResponse<ReservaListItemDTO>>
    {
        private readonly IReservaRepository _repo;

        public GetReservasListUseCase(IReservaRepository repo)
        {
            _repo = repo;
        }

        public async Task<PaginationResponse<ReservaListItemDTO>> Handle(GetReservasListQuery query)
        {
            (List<Reserva> reservas, int totalCount) = await _repo.GetPagedListAsync(
                query.Pagination,
                query.PlacaFilter,
                query.UsuarioIdFilter);

            List<ReservaListItemDTO> dtos = reservas.Select(r => new ReservaListItemDTO
            {
                Id = r.Id,
                PlacaVehiculo = r.Vehiculo.Placa,
                NombreUsuario = $"{r.Cliente.Nombre} {r.Cliente.Apellido}",
                FechaInicio = r.FechaInicio,
                FechaFin = r.FechaFin,
                Estado = r.Estado.Nombre,
                PrecioTotal = r.PrecioTotal
            }).ToList();

            return PaginationResponse<ReservaListItemDTO>.Create(dtos, totalCount, query.Pagination);
        }
    }
}