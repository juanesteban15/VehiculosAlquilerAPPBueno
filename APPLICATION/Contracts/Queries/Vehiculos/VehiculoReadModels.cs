namespace VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos
{
    public class VehiculosHomeFilter
    {
        public string? Filtro { get; set; }
        public string? TipoVehiculo { get; set; }
        public int? MarcaId { get; set; }
        public int? CategoriaId { get; set; }
        public int? CombustibleId { get; set; }
        public int? TransmisionId { get; set; }
    }

    public class VehiculosHomeResult
    {
        public List<VehiculoHomeDto> Vehiculos { get; set; } = [];
        public List<CatalogoFiltroDto> TiposVehiculo { get; set; } = [];
        public List<CatalogoFiltroDto> Marcas { get; set; } = [];
        public List<CatalogoFiltroDto> Categorias { get; set; } = [];
        public List<CatalogoFiltroDto> Combustibles { get; set; } = [];
        public List<CatalogoFiltroDto> Transmisiones { get; set; } = [];
    }

    public record CatalogoFiltroDto(int Id, string Codigo, string Nombre);

    public class VehiculoHomeDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Modelo { get; set; }
        public string TipoVehiculo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int PublicadorId { get; set; }
        public string TipoPublicador { get; set; } = string.Empty;
        public string PublicadorNombre { get; set; } = string.Empty;
        public string? FotoPrincipal { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class VehiculoDetalleDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string TipoVehiculo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Combustible { get; set; } = string.Empty;
        public string Transmision { get; set; } = string.Empty;
        public int Modelo { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public int PublicadorId { get; set; }
        public string PublicadorNombre { get; set; } = string.Empty;
        public string TipoPublicador { get; set; } = string.Empty;
        public decimal? PrecioPorDia { get; set; }
        public List<string> Fotos { get; set; } = [];
        public List<DocumentoVehiculoDto> Documentos { get; set; } = [];
        public List<MetodoPagoReservaDto> MetodosPago { get; set; } = [];
    }

    public class DocumentoVehiculoDto
    {
        public string TipoDocumento { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaArchivo { get; set; } = string.Empty;
    }

    public record MetodoPagoReservaDto(int Id, string Nombre);
}
