using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones;
using VehiculosAlquilerApp.Application.UseCases.Notificaciones.Queries.GetNotificacionesUsuario;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Notificaciones
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<NotificacionDto> Notificaciones { get; private set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            Notificaciones = await _mediator.Send(new GetNotificacionesUsuarioQuery { UsuarioId = usuarioId.Value });
            return Page();
        }
    }
}
