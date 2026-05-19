using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Infrastructure.Persistence;

namespace VehiculosAlquilerApp.Infrastructure.Queries
{
    public class PerfilReadService : IPerfilReadService
    {
        private readonly AlquilerDbContext _context;

        public PerfilReadService(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task<PerfilEdicionDto?> GetParaEditarAsync(int usuarioId)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario is null)
                return null;

            return new PerfilEdicionDto
            {
                Pais = usuario.Pais,
                FechaNacimiento = usuario.FechaNacimiento,
                FotoPerfilRuta = usuario.FotoPerfilRuta,
                Documentos = await _context.DocumentosVerificacionUsuario
                    .Where(d => d.Usuario.Id == usuarioId)
                    .OrderByDescending(d => d.FechaSubida)
                    .Select(d => new DocumentoPerfilDto
                    {
                        TipoDocumento = d.TipoDocumento,
                        NombreArchivo = d.NombreArchivo,
                        RutaArchivo = d.RutaArchivo,
                        Estado = d.Estado,
                        FechaSubida = d.FechaSubida
                    })
                    .ToListAsync()
            };
        }

        public async Task<PerfilPublicoResult> GetPublicoAsync(int usuarioId)
        {
            var result = new PerfilPublicoResult();

            Usuario? usuario = await _context.Usuarios
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario is null)
                return result;

            List<ComentarioUsuario> comentarios = await _context.ComentariosUsuario
                .Include(c => c.Autor)
                .Where(c => c.UsuarioEvaluado.Id == usuarioId)
                .OrderByDescending(c => c.FechaCreacion)
                .ToListAsync();

            result.Perfil = new PerfilPublicoDto
            {
                Id = usuario.Id,
                NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
                Iniciales = $"{usuario.Nombre.FirstOrDefault()}{usuario.Apellido.FirstOrDefault()}".ToUpper(),
                Pais = usuario.Pais,
                FotoPerfilRuta = usuario.FotoPerfilRuta,
                Estado = usuario.Estado.Nombre,
                TipoCuenta = usuario.TipoCuenta,
                NombreEmpresa = usuario.NombreEmpresa,
                NitEmpresa = usuario.NitEmpresa,
                CalificacionPromedio = comentarios.Count == 0 ? 0 : comentarios.Average(c => c.Calificacion),
                TotalComentarios = comentarios.Count
            };

            result.Comentarios = comentarios.Select(c => new ComentarioPerfilDto
            {
                AutorNombre = $"{c.Autor.Nombre} {c.Autor.Apellido}",
                Calificacion = c.Calificacion,
                Comentario = c.Comentario,
                FechaCreacion = c.FechaCreacion
            }).ToList();

            result.Vehiculos = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Estado)
                .Where(v => v.Propietario.Id == usuarioId)
                .OrderByDescending(v => v.FechaRegistro)
                .Select(v => new VehiculoPerfilDto
                {
                    Placa = v.Placa,
                    Marca = v.Marca.Nombre,
                    TipoVehiculo = v.TipoVehiculo.Nombre,
                    Modelo = v.Modelo,
                    Estado = v.Estado.Nombre
                })
                .ToListAsync();

            return result;
        }
    }
}
