using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Infrastructure.Persistence;

namespace VehiculosAlquilerApp.Infrastructure.Queries
{
    public class VehiculoReadService : IVehiculoReadService
    {
        private readonly AlquilerDbContext _context;

        public VehiculoReadService(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task<VehiculosHomeResult> GetHomeAsync(VehiculosHomeFilter filter)
        {
            var result = new VehiculosHomeResult();

            result.TiposVehiculo = await _context.TiposVehiculo
                .Where(t => t.Codigo == "AUTO" || t.Codigo == "MOTO")
                .OrderBy(t => t.Nombre)
                .Select(t => new CatalogoFiltroDto(t.Id, t.Codigo, t.Nombre))
                .ToListAsync();

            CatalogoFiltroDto? tipoSeleccionado = result.TiposVehiculo.FirstOrDefault(t =>
                string.Equals(t.Codigo, filter.TipoVehiculo, StringComparison.OrdinalIgnoreCase));

            if (tipoSeleccionado is not null)
            {
                result.Marcas = await _context.Marcas
                    .Where(m => m.TipoVehiculo.Id == tipoSeleccionado.Id)
                    .OrderBy(m => m.Nombre)
                    .Select(m => new CatalogoFiltroDto(m.Id, string.Empty, m.Nombre))
                    .ToListAsync();

                result.Categorias = await _context.Categorias
                    .Where(c => c.TipoVehiculo.Id == tipoSeleccionado.Id)
                    .OrderBy(c => c.Nombre)
                    .Select(c => new CatalogoFiltroDto(c.Id, string.Empty, c.Nombre))
                    .ToListAsync();

                result.Combustibles = await _context.TiposCombustible
                    .Where(c => c.TipoVehiculo.Id == tipoSeleccionado.Id)
                    .OrderBy(c => c.Nombre)
                    .Select(c => new CatalogoFiltroDto(c.Id, string.Empty, c.Nombre))
                    .ToListAsync();

                result.Transmisiones = await _context.SistemasTransmision
                    .Where(t => t.TipoVehiculo.Id == tipoSeleccionado.Id)
                    .OrderBy(t => t.Nombre)
                    .Select(t => new CatalogoFiltroDto(t.Id, string.Empty, t.Nombre))
                    .ToListAsync();
            }

            IQueryable<Vehiculo> query = _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Categoria)
                .Include(v => v.TipoCombustible)
                .Include(v => v.SistemaTransmision)
                .Include(v => v.Propietario)
                .OrderByDescending(v => v.FechaRegistro);

            if (tipoSeleccionado is not null)
                query = query.Where(v => v.TipoVehiculo.Id == tipoSeleccionado.Id);

            if (filter.MarcaId.HasValue)
                query = query.Where(v => v.Marca.Id == filter.MarcaId.Value);

            if (filter.CategoriaId.HasValue)
                query = query.Where(v => v.Categoria.Id == filter.CategoriaId.Value);

            if (filter.CombustibleId.HasValue)
                query = query.Where(v => v.TipoCombustible.Id == filter.CombustibleId.Value);

            if (filter.TransmisionId.HasValue)
                query = query.Where(v => v.SistemaTransmision.Id == filter.TransmisionId.Value);

            if (!string.IsNullOrWhiteSpace(filter.Filtro))
            {
                string texto = filter.Filtro.Trim();
                query = query.Where(v =>
                    v.TipoVehiculo.Nombre.Contains(texto) ||
                    v.Categoria.Nombre.Contains(texto) ||
                    v.Marca.Nombre.Contains(texto));
            }

            result.Vehiculos = await query
                .Select(v => new VehiculoHomeDto
                {
                    Placa = v.Placa,
                    Marca = v.Marca.Nombre,
                    Modelo = v.Modelo,
                    TipoVehiculo = v.TipoVehiculo.Nombre,
                    Categoria = v.Categoria.Nombre,
                    PublicadorId = v.Propietario.Id,
                    TipoPublicador = v.Propietario.TipoCuenta,
                    PublicadorNombre = v.Propietario.TipoCuenta == "Empresa"
                        ? v.Propietario.NombreEmpresa ?? $"{v.Propietario.Nombre} {v.Propietario.Apellido}"
                        : $"{v.Propietario.Nombre} {v.Propietario.Apellido}",
                    FechaRegistro = v.FechaRegistro
                })
                .Take(24)
                .ToListAsync();

            foreach (VehiculoHomeDto vehiculo in result.Vehiculos)
            {
                vehiculo.FotoPrincipal = await _context.VehiculoFotos
                    .Where(f => f.Vehiculo.Placa == vehiculo.Placa)
                    .OrderBy(f => f.Orden)
                    .Select(f => f.RutaArchivo)
                    .FirstOrDefaultAsync();
            }

            return result;
        }

        public async Task<VehiculoDetalleDto?> GetDetalleAsync(string placa)
        {
            Vehiculo? vehiculo = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Categoria)
                .Include(v => v.TipoCombustible)
                .Include(v => v.SistemaTransmision)
                .Include(v => v.Estado)
                .Include(v => v.Propietario)
                .Include(v => v.Colores)
                .FirstOrDefaultAsync(v => v.Placa == placa.ToUpper().Trim());

            if (vehiculo is null)
                return null;

            Tarifa? tarifa = await _context.Tarifas
                .Where(t => t.Vehiculo.Placa == vehiculo.Placa && t.Activa)
                .OrderByDescending(t => t.FechaInicio)
                .FirstOrDefaultAsync();

            var dto = new VehiculoDetalleDto
            {
                Placa = vehiculo.Placa,
                Marca = vehiculo.Marca.Nombre,
                TipoVehiculo = vehiculo.TipoVehiculo.Nombre,
                Categoria = vehiculo.Categoria.Nombre,
                Combustible = vehiculo.TipoCombustible.Nombre,
                Transmision = vehiculo.SistemaTransmision.Nombre,
                Modelo = vehiculo.Modelo,
                Color = string.Join(" / ", vehiculo.Colores.OrderBy(c => c.Orden).Select(c => c.Nombre)),
                Estado = vehiculo.Estado.Nombre,
                FechaRegistro = vehiculo.FechaRegistro,
                PublicadorId = vehiculo.Propietario.Id,
                PublicadorNombre = vehiculo.Propietario.TipoCuenta == "Empresa"
                    ? vehiculo.Propietario.NombreEmpresa ?? $"{vehiculo.Propietario.Nombre} {vehiculo.Propietario.Apellido}"
                    : $"{vehiculo.Propietario.Nombre} {vehiculo.Propietario.Apellido}",
                TipoPublicador = vehiculo.Propietario.TipoCuenta,
                PrecioPorDia = tarifa?.PrecioPorDia
            };

            if (string.IsNullOrWhiteSpace(dto.Color))
                dto.Color = "Sin color";

            dto.Fotos = await _context.VehiculoFotos
                .Where(f => f.Vehiculo.Placa == dto.Placa)
                .OrderBy(f => f.Orden)
                .Select(f => f.RutaArchivo)
                .ToListAsync();

            dto.Documentos = await _context.DocumentosVehiculo
                .Where(d => d.Vehiculo.Placa == dto.Placa)
                .OrderBy(d => d.TipoDocumento)
                .Select(d => new DocumentoVehiculoDto
                {
                    TipoDocumento = d.TipoDocumento,
                    NombreArchivo = d.NombreArchivo,
                    RutaArchivo = d.RutaArchivo
                })
                .ToListAsync();

            dto.MetodosPago = await _context.MetodosPago
                .Where(m => m.Nombre == MetodoPago.TarjetaCredito || m.Nombre == MetodoPago.Pse)
                .OrderBy(m => m.Nombre)
                .Select(m => new MetodoPagoReservaDto(m.Id, m.Nombre == MetodoPago.Pse ? "PSE" : "Tarjeta"))
                .ToListAsync();

            return dto;
        }

        public async Task<CrearVehiculoCatalogosDto> GetCatalogosCrearAsync()
        {
            var result = new CrearVehiculoCatalogosDto();

            result.TiposVehiculo = await _context.TiposVehiculo
                .Where(t => t.Activo && (t.Codigo == "AUTO" || t.Codigo == "MOTO"))
                .OrderBy(t => t.Nombre)
                .Select(t => new CatalogoVehiculoDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    TipoVehiculoId = t.Id,
                    Codigo = t.Codigo
                })
                .ToListAsync();

            result.Marcas = await _context.Marcas
                .Where(m => m.TipoVehiculo.Codigo == "AUTO" || m.TipoVehiculo.Codigo == "MOTO")
                .OrderBy(m => m.TipoVehiculo.Nombre)
                .ThenBy(m => m.Nombre)
                .Select(m => new CatalogoVehiculoDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    TipoVehiculoId = m.TipoVehiculo.Id,
                    Codigo = m.TipoVehiculo.Codigo
                })
                .ToListAsync();

            result.Categorias = await _context.Categorias
                .Where(c => c.TipoVehiculo.Codigo == "AUTO" || c.TipoVehiculo.Codigo == "MOTO")
                .OrderBy(c => c.TipoVehiculo.Nombre)
                .ThenBy(c => c.Nombre)
                .Select(c => new CatalogoVehiculoDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    TipoVehiculoId = c.TipoVehiculo.Id,
                    Codigo = c.TipoVehiculo.Codigo
                })
                .ToListAsync();

            result.Combustibles = await _context.TiposCombustible
                .Where(c => c.TipoVehiculo.Codigo == "AUTO" || c.TipoVehiculo.Codigo == "MOTO")
                .OrderBy(c => c.TipoVehiculo.Nombre)
                .ThenBy(c => c.Nombre)
                .Select(c => new CatalogoVehiculoDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    TipoVehiculoId = c.TipoVehiculo.Id,
                    Codigo = c.TipoVehiculo.Codigo
                })
                .ToListAsync();

            result.Transmisiones = await _context.SistemasTransmision
                .Where(t => t.TipoVehiculo.Codigo == "AUTO" || t.TipoVehiculo.Codigo == "MOTO")
                .OrderBy(t => t.TipoVehiculo.Nombre)
                .ThenBy(t => t.Nombre)
                .Select(t => new CatalogoVehiculoDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    TipoVehiculoId = t.TipoVehiculo.Id,
                    Codigo = t.TipoVehiculo.Codigo
                })
                .ToListAsync();

            result.Colores = await _context.Colores
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .Select(c => new ColorVehiculoDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();

            return result;
        }
    }
}
