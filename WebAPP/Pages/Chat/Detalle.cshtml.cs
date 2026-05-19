using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Contracts.Queries.Chat;
using VehiculosAlquilerApp.Application.UseCases.Chat.Commands.EnviarMensajeChat;
using VehiculosAlquilerApp.Application.UseCases.Chat.Queries.GetChatDetalle;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Chat
{
    public class DetalleModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetalleModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ChatDetalleDto? Conversacion { get; private set; }

        [BindProperty]
        public MensajeInput Input { get; set; } = new();

        public string? Error { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            return await CargarAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            if (!ModelState.IsValid)
                return await CargarAsync(id);

            try
            {
                await _mediator.Send(new EnviarMensajeChatCommand
                {
                    ConversacionId = id,
                    AutorId = usuarioId.Value,
                    Texto = Input.Texto
                });
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return await CargarAsync(id);
            }

            return RedirectToPage("/Chat/Detalle", new { id });
        }

        private async Task<IActionResult> CargarAsync(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            Conversacion = await _mediator.Send(new GetChatDetalleQuery
            {
                ConversacionId = id,
                UsuarioId = usuarioId.Value
            });

            if (Conversacion is null)
                return NotFound();

            return Page();
        }
    }

    public class MensajeInput
    {
        [Required(ErrorMessage = "Escribe un mensaje.")]
        [MaxLength(1000)]
        public string Texto { get; set; } = string.Empty;
    }
}
