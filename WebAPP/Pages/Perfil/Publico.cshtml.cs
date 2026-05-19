using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearComentarioPerfil;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilPublico;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Perfil
{
    public class PublicoModel : PageModel
    {
        private readonly IMediator _mediator;

        public PublicoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public PerfilPublicoDto? Perfil { get; private set; }
        public List<VehiculoPerfilDto> Vehiculos { get; private set; } = [];
        public List<ComentarioPerfilDto> Comentarios { get; private set; } = [];
        public int? UsuarioActualId { get; private set; }

        [BindProperty]
        public ComentarioInput ComentarioInput { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            UsuarioActualId = HttpContext.Session.GetInt32("UsuarioId");
            if (UsuarioActualId is null)
                return RedirectToPage("/Sesion/Login");

            if (UsuarioActualId == id)
                ModelState.AddModelError(string.Empty, "No puedes comentar tu propio perfil.");

            if (!ModelState.IsValid)
            {
                await LoadAsync(id);
                return Page();
            }

            try
            {
                await _mediator.Send(new CrearComentarioPerfilCommand
                {
                    UsuarioEvaluadoId = id,
                    AutorId = UsuarioActualId.Value,
                    Calificacion = ComentarioInput.Calificacion,
                    Comentario = ComentarioInput.Comentario
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadAsync(id);
                return Page();
            }

            return RedirectToPage("/Perfil/Publico", new { id });
        }

        private async Task LoadAsync(int id)
        {
            UsuarioActualId = HttpContext.Session.GetInt32("UsuarioId");
            PerfilPublicoResult result = await _mediator.Send(new GetPerfilPublicoQuery { UsuarioId = id });
            Perfil = result.Perfil;
            Vehiculos = result.Vehiculos;
            Comentarios = result.Comentarios;
        }
    }

    public class ComentarioInput
    {
        [Range(1, 5)]
        [Display(Name = "Calificacion")]
        public int Calificacion { get; set; } = 5;

        [Required(ErrorMessage = "El comentario es obligatorio.")]
        [MaxLength(700, ErrorMessage = "El comentario no puede superar 700 caracteres.")]
        public string Comentario { get; set; } = string.Empty;
    }
}
