using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetMisReservas;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Reservas
{
    public class MisReservasModel : PageModel
    {
        private readonly IMediator _mediator;

        public MisReservasModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<ReservaResumenDto> Reservas { get; private set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            Reservas = await _mediator.Send(new GetMisReservasQuery { UsuarioId = usuarioId.Value });
            return Page();
        }
    }
}
