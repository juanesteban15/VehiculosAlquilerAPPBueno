using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Queries.Chat;
using VehiculosAlquilerApp.Infrastructure.Persistence;

namespace VehiculosAlquilerApp.Infrastructure.Queries
{
    public class ChatReadService : IChatReadService
    {
        private readonly AlquilerDbContext _context;

        public ChatReadService(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task<ChatDetalleDto?> GetDetalleAsync(int conversacionId, int usuarioId)
        {
            var conversacion = await _context.Conversaciones
                .Include(c => c.Reserva).ThenInclude(r => r.Vehiculo).ThenInclude(v => v.Marca)
                .Include(c => c.Cliente)
                .Include(c => c.Propietario)
                .FirstOrDefaultAsync(c => c.Id == conversacionId);

            if (conversacion is null)
                return null;

            if (conversacion.Cliente.Id != usuarioId && conversacion.Propietario.Id != usuarioId)
                return null;

            var dto = new ChatDetalleDto
            {
                Id = conversacion.Id,
                Vehiculo = $"{conversacion.Reserva.Vehiculo.Marca.Nombre} {conversacion.Reserva.Vehiculo.Modelo}",
                Cliente = $"{conversacion.Cliente.Nombre} {conversacion.Cliente.Apellido}",
                Propietario = conversacion.Propietario.TipoCuenta == "Empresa"
                    ? conversacion.Propietario.NombreEmpresa ?? $"{conversacion.Propietario.Nombre} {conversacion.Propietario.Apellido}"
                    : $"{conversacion.Propietario.Nombre} {conversacion.Propietario.Apellido}",
                UsuarioActualId = usuarioId
            };

            dto.Mensajes = await _context.MensajesChat
                .Include(m => m.Autor)
                .Where(m => m.Conversacion.Id == conversacionId)
                .OrderBy(m => m.FechaEnvio)
                .Select(m => new MensajeChatDto
                {
                    AutorId = m.Autor.Id,
                    AutorNombre = $"{m.Autor.Nombre} {m.Autor.Apellido}",
                    Texto = m.Texto,
                    FechaEnvio = m.FechaEnvio
                })
                .ToListAsync();

            return dto;
        }
    }
}
