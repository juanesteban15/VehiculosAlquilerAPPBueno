namespace VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos
{
    public class CrearVehiculoCatalogosDto
    {
        public List<CatalogoVehiculoDto> TiposVehiculo { get; set; } = [];
        public List<CatalogoVehiculoDto> Marcas { get; set; } = [];
        public List<CatalogoVehiculoDto> Categorias { get; set; } = [];
        public List<CatalogoVehiculoDto> Combustibles { get; set; } = [];
        public List<CatalogoVehiculoDto> Transmisiones { get; set; } = [];
        public List<ColorVehiculoDto> Colores { get; set; } = [];
    }

    public class CatalogoVehiculoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int TipoVehiculoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }

    public class ColorVehiculoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
