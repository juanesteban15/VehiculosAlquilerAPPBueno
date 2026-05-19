using Microsoft.AspNetCore.Mvc;
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.GetUsuariosList;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculoAlquilerAPP.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetUsuariosListQuery
            {
                Pagination = new PaginationRequest(1, 10)
            };

            var usuarios = await _mediator.Send(query);

            return View(usuarios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearUsuarioCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}