using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Infrastructure.Persistence;

namespace VehiculosAlquilerApp.Infrastructure.Queries
{
    public class ReservaReadService : IReservaReadService
    {
        private readonly AlquilerDbContext _context;

        public ReservaReadService(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservaResumenDto>> GetMisReservasAsync(int usuarioId)
        {
            List<ReservaResumenDto> reservas = await _context.Reservas
                .Include(r => r.Vehiculo).ThenInclude(v => v.Marca)
                .Include(r => r.Vehiculo).ThenInclude(v => v.Propietario)
                .Include(r => r.Estado)
                .Where(r => r.Cliente.Id == usuarioId)
                .OrderByDescending(r => r.FechaCreacion)
                .Select(r => new ReservaResumenDto
                {
                    Id = r.Id,
                    VehiculoPlaca = r.Vehiculo.Placa,
                    VehiculoNombre = $"{r.Vehiculo.Marca.Nombre} {r.Vehiculo.Modelo}",
                    PersonaNombre = r.Vehiculo.Propietario.TipoCuenta == "Empresa"
                        ? r.Vehiculo.Propietario.NombreEmpresa ?? $"{r.Vehiculo.Propietario.Nombre} {r.Vehiculo.Propietario.Apellido}"
                        : $"{r.Vehiculo.Propietario.Nombre} {r.Vehiculo.Propietario.Apellido}",
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin,
                    Estado = r.Estado.Nombre,
                    PrecioTotal = r.PrecioTotal
                })
                .ToListAsync();

            await CargarConversacionesAsync(reservas);
            return reservas;
        }

        public async Task<List<ReservaResumenDto>> GetRecibidasAsync(int duenioId)
        {
            List<ReservaResumenDto> reservas = await _context.Reservas
                .Include(r => r.Vehiculo).ThenInclude(v => v.Marca)
                .Include(r => r.Vehiculo).ThenInclude(v => v.Propietario)
                .Include(r => r.Cliente)
                .Include(r => r.Estado)
                .Where(r => r.Vehiculo.Propietario.Id == duenioId)
                .OrderByDescending(r => r.FechaCreacion)
                .Select(r => new ReservaResumenDto
                {
                    Id = r.Id,
                    VehiculoPlaca = r.Vehiculo.Placa,
                    VehiculoNombre = $"{r.Vehiculo.Marca.Nombre} {r.Vehiculo.Modelo}",
                    PersonaNombre = $"{r.Cliente.Nombre} {r.Cliente.Apellido}",
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin,
                    Estado = r.Estado.Nombre,
                    PrecioTotal = r.PrecioTotal
                })
                .ToListAsync();

            await CargarConversacionesAsync(reservas);
            return reservas;
        }

        private async Task CargarConversacionesAsync(List<ReservaResumenDto> reservas)
        {
            foreach (ReservaResumenDto reserva in reservas)
            {
                reserva.ConversacionId = await _context.Conversaciones
                    .Where(c => c.Reserva.Id == reserva.Id)
                    .Select(c => (int?)c.Id)
                    .FirstOrDefaultAsync();
            }
        }
    }
}
