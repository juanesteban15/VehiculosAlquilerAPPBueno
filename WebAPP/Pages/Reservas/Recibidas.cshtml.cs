using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.AceptarReserva;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.RechazarReserva;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetReservasRecibidas;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Reservas
{
    public class RecibidasModel : PageModel
    {
        private readonly IMediator _mediator;

        public RecibidasModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<ReservaResumenDto> Reservas { get; private set; } = [];
        public string? Mensaje { get; private set; }
        public string? Error { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return await CargarAsync();
        }

        public async Task<IActionResult> OnPostAceptarAsync(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            try
            {
                await _mediator.Send(new AceptarReservaCommand
                {
                    ReservaId = id,
                    DuenioId = usuarioId.Value
                });

                Mensaje = "Reserva aceptada. Se activo el chat con el cliente.";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return await CargarAsync();
        }

        public async Task<IActionResult> OnPostRechazarAsync(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            try
            {
                await _mediator.Send(new RechazarReservaCommand
                {
                    ReservaId = id,
                    DuenioId = usuarioId.Value
                });

                Mensaje = "Reserva rechazada.";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return await CargarAsync();
        }

        private async Task<IActionResult> CargarAsync()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            Reservas = await _mediator.Send(new GetReservasRecibidasQuery { DuenioId = usuarioId.Value });
            return Page();
        }
    }
}
