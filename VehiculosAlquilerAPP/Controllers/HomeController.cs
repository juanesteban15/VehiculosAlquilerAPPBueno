using Microsoft.AspNetCore.Mvc;
using VehiculosAlquilerAPP.Web.Middlewares;

namespace VehiculosAlquilerApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            // Lee el mensaje de error que guardó el Middleware en la sesión
            string? message = HttpContext.Session.GetString(
                ExceptionHandlerMiddleware.ERROR_MESSAGE_SESSION_KEY);

            // Limpia el mensaje para que no se muestre dos veces
            HttpContext.Session.Remove(
                ExceptionHandlerMiddleware.ERROR_MESSAGE_SESSION_KEY);

            ViewBag.ErrorMessage = message ?? "Ha ocurrido un error inesperado.";
            return View();
        }
    }
}