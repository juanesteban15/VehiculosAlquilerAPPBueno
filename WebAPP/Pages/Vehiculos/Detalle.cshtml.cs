using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CrearReserva;
using VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculoDetalle;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Vehiculos
{
    public class DetalleModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetalleModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public VehiculoDetalleDto? Vehiculo { get; private set; }

        [BindProperty]
        public ReservaInput Input { get; set; } = new();

        public string? SuccessMessage { get; private set; }
        public string? ErrorMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync(string placa)
        {
            return await CargarPaginaAsync(placa);
        }

        public async Task<IActionResult> OnPostReservarAsync(string placa)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            if (!ModelState.IsValid)
                return await CargarPaginaAsync(placa);

            try
            {
                await _mediator.Send(new CrearReservaCommand
                {
                    Placa = placa,
                    UsuarioId = usuarioId.Value,
                    FechaInicio = Input.FechaInicio,
                    FechaFin = Input.FechaFin,
                    MetodoPagoId = Input.MetodoPagoId
                });

                SuccessMessage = "Tu reserva quedo en proceso. El dueno del vehiculo recibio la solicitud y podra aceptarla o rechazarla.";
                Input = new ReservaInput();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return await CargarPaginaAsync(placa);
        }

        private async Task<IActionResult> CargarPaginaAsync(string placa)
        {
            Vehiculo = await _mediator.Send(new GetVehiculoDetalleQuery { Placa = placa });

            if (Vehiculo is null)
                return NotFound();

            return Page();
        }
    }

    public class ReservaInput
    {
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; } = DateTime.Today.AddDays(1);

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona el metodo de pago.")]
        [Display(Name = "Metodo de pago")]
        public int MetodoPagoId { get; set; }
    }
}
