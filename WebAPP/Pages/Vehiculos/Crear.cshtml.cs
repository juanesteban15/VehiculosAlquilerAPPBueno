using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CrearVehiculo;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetCrearVehiculoCatalogos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Vehiculos
{
    public class CrearModel : PageModel
    {
        private readonly IMediator _mediator;

        public CrearModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public VehiculoInput Input { get; set; } = new();

        public List<CatalogoVehiculoDto> TiposVehiculo { get; private set; } = [];
        public List<CatalogoVehiculoDto> Marcas { get; private set; } = [];
        public List<CatalogoVehiculoDto> Categorias { get; private set; } = [];
        public List<CatalogoVehiculoDto> Combustibles { get; private set; } = [];
        public List<CatalogoVehiculoDto> Transmisiones { get; private set; } = [];
        public List<ColorVehiculoDto> Colores { get; private set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") is null)
                return RedirectToPage("/Sesion/Login");

            await LoadCatalogosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            ValidarInput();

            if (!ModelState.IsValid)
            {
                await LoadCatalogosAsync();
                return Page();
            }

            try
            {
                string placa = await _mediator.Send(new CrearVehiculoCommand
                {
                    Placa = Input.Placa,
                    PropietarioId = usuarioId.Value,
                    TipoVehiculoId = Input.TipoVehiculoId,
                    MarcaId = Input.MarcaId,
                    CategoriaId = Input.CategoriaId,
                    CombustibleId = Input.CombustibleId,
                    TransmisionId = Input.TransmisionId,
                    Modelo = Input.Modelo,
                    ColorIds = GetColoresSeleccionados(),
                    Fotos = Input.Fotos.Select(ToArchivoSubida).ToList(),
                    TarjetaPropiedad = ToArchivoSubidaOpcional(Input.TarjetaPropiedad),
                    Soat = ToArchivoSubidaOpcional(Input.Soat),
                    Tecnomecanica = ToArchivoSubidaOpcional(Input.Tecnomecanica)
                });

                return RedirectToPage("/Vehiculos/Detalle", new { placa });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadCatalogosAsync();
                return Page();
            }
        }

        private async Task LoadCatalogosAsync()
        {
            CrearVehiculoCatalogosDto catalogos = await _mediator.Send(new GetCrearVehiculoCatalogosQuery());
            TiposVehiculo = catalogos.TiposVehiculo;
            Marcas = catalogos.Marcas;
            Categorias = catalogos.Categorias;
            Combustibles = catalogos.Combustibles;
            Transmisiones = catalogos.Transmisiones;
            Colores = catalogos.Colores;
        }

        private void ValidarInput()
        {
            if (Input.TipoVehiculoId <= 0)
                ModelState.AddModelError("Input.TipoVehiculoId", "Selecciona si vas a publicar una moto o un carro.");

            if (Input.EsMulticolor)
                ModelState.AddModelError("Input.EsMulticolor", "Multicolor no es una categoria valida en la licencia de transito. Registra hasta tres colores predominantes.");

            if (!Input.EsMulticolor && Input.ColorPrincipalId is null)
                ModelState.AddModelError("Input.ColorPrincipalId", "Selecciona el color principal del vehiculo.");

            if (!Input.EsMulticolor && Input.ColorTerciarioId is not null && Input.ColorSecundarioId is null)
                ModelState.AddModelError("Input.ColorSecundarioId", "Debes seleccionar el color secundario antes del terciario.");

            if (Input.Fotos.Count != 5)
                ModelState.AddModelError("Input.Fotos", "Debes subir exactamente 5 fotos del vehiculo.");

            ValidarArchivo(Input.TarjetaPropiedad, "Input.TarjetaPropiedad");
            ValidarArchivo(Input.Soat, "Input.Soat");
            ValidarArchivo(Input.Tecnomecanica, "Input.Tecnomecanica");

            foreach (IFormFile foto in Input.Fotos)
                ValidarArchivo(foto, "Input.Fotos", soloImagen: true);
        }

        private List<int> GetColoresSeleccionados()
        {
            return new int?[]
                {
                    Input.ColorPrincipalId,
                    Input.ColorSecundarioId,
                    Input.ColorTerciarioId
                }
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToList();
        }

        private void ValidarArchivo(IFormFile? archivo, string key, bool soloImagen = false)
        {
            if (archivo is null || archivo.Length == 0)
            {
                ModelState.AddModelError(key, "Este archivo es obligatorio.");
                return;
            }

            string extension = Path.GetExtension(archivo.FileName).ToLower();
            string[] permitidas = soloImagen ? [".jpg", ".jpeg", ".png"] : [".jpg", ".jpeg", ".png", ".pdf"];
            if (!permitidas.Contains(extension))
                ModelState.AddModelError(key, soloImagen ? "Las fotos deben ser JPG o PNG." : "El archivo debe ser JPG, PNG o PDF.");
        }

        private static ArchivoSubida ToArchivoSubida(IFormFile archivo)
        {
            return new ArchivoSubida
            {
                NombreArchivo = archivo.FileName,
                Length = archivo.Length,
                Contenido = archivo.OpenReadStream()
            };
        }

        private static ArchivoSubida? ToArchivoSubidaOpcional(IFormFile? archivo)
        {
            if (archivo is null || archivo.Length == 0)
                return null;

            return ToArchivoSubida(archivo);
        }
    }

    public class VehiculoInput
    {
        [Required]
        [MaxLength(10)]
        public string Placa { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona si vas a publicar una moto o un carro.")]
        public int TipoVehiculoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona una marca.")]
        public int MarcaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona una categoria.")]
        public int CategoriaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un combustible.")]
        public int CombustibleId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona una transmision.")]
        public int TransmisionId { get; set; }

        [Range(1900, 2100)]
        public int Modelo { get; set; } = DateTime.Today.Year;

        [Display(Name = "Multicolor")]
        public bool EsMulticolor { get; set; }

        [Display(Name = "Color principal")]
        public int? ColorPrincipalId { get; set; }

        [Display(Name = "Color secundario")]
        public int? ColorSecundarioId { get; set; }

        [Display(Name = "Color terciario")]
        public int? ColorTerciarioId { get; set; }

        [Display(Name = "Fotos del vehiculo")]
        public List<IFormFile> Fotos { get; set; } = [];

        [Display(Name = "Tarjeta de propiedad")]
        public IFormFile? TarjetaPropiedad { get; set; }

        public IFormFile? Soat { get; set; }

        [Display(Name = "Tecnomecanica")]
        public IFormFile? Tecnomecanica { get; set; }
    }
}
