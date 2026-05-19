using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class ColorVehiculoRepository : Repository<ColorVehiculo, int>, IColorVehiculoRepository
    {
        private readonly AlquilerDbContext _context;

        public ColorVehiculoRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<ColorVehiculo>> GetActivosPorIdsAsync(IEnumerable<int> ids)
        {
            int[] idsArray = ids.ToArray();
            return _context.Colores
                .Where(c => c.Activo && idsArray.Contains(c.Id))
                .ToListAsync();
        }
    }

    public class VehiculoFotoRepository : Repository<VehiculoFoto, int>, IVehiculoFotoRepository
    {
        public VehiculoFotoRepository(AlquilerDbContext context) : base(context)
        {
        }
    }

    public class DocumentoVehiculoArchivoRepository : Repository<DocumentoVehiculoArchivo, int>, IDocumentoVehiculoArchivoRepository
    {
        public DocumentoVehiculoArchivoRepository(AlquilerDbContext context) : base(context)
        {
        }
    }

    public class DocumentoVerificacionUsuarioRepository : Repository<DocumentoVerificacionUsuario, int>, IDocumentoVerificacionUsuarioRepository
    {
        public DocumentoVerificacionUsuarioRepository(AlquilerDbContext context) : base(context)
        {
        }
    }

    public class ComentarioUsuarioRepository : Repository<ComentarioUsuario, int>, IComentarioUsuarioRepository
    {
        public ComentarioUsuarioRepository(AlquilerDbContext context) : base(context)
        {
        }
    }
}
