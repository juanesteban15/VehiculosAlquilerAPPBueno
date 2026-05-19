using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculosHome;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public string? Filtro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TipoVehiculo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MarcaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoriaId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CombustibleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? TransmisionId { get; set; }

        public List<VehiculoHomeDto> Vehiculos { get; private set; } = [];
        public List<CatalogoFiltroDto> TiposVehiculo { get; private set; } = [];
        public List<CatalogoFiltroDto> Marcas { get; private set; } = [];
        public List<CatalogoFiltroDto> Categorias { get; private set; } = [];
        public List<CatalogoFiltroDto> Combustibles { get; private set; } = [];
        public List<CatalogoFiltroDto> Transmisiones { get; private set; } = [];

        public async Task OnGetAsync()
        {
            VehiculosHomeResult result = await _mediator.Send(new GetVehiculosHomeQuery
            {
                Filter = new VehiculosHomeFilter
                {
                    Filtro = Filtro,
                    TipoVehiculo = TipoVehiculo,
                    MarcaId = MarcaId,
                    CategoriaId = CategoriaId,
                    CombustibleId = CombustibleId,
                    TransmisionId = TransmisionId
                }
            });

            Vehiculos = result.Vehiculos;
            TiposVehiculo = result.TiposVehiculo;
            Marcas = result.Marcas;
            Categorias = result.Categorias;
            Combustibles = result.Combustibles;
            Transmisiones = result.Transmisiones;
        }
    }
}
