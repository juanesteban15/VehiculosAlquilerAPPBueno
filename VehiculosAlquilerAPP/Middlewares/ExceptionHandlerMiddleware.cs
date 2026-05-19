// ============================================================
// ARCHIVO: Web/Middlewares/ExceptionHandlerMiddleware.cs
// ============================================================
// Captura TODAS las excepciones que no fueron atrapadas en los Controllers
// Las convierte en mensajes entendibles para el usuario
// Sin esto el usuario vería una pantalla de error técnica fea

using VehiculosAlquilerApp.Application.Exceptions;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerAPP.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        // Clave para guardar el mensaje de error en la sesión
        // El Controller de error la lee para mostrar el mensaje
        public const string ERROR_MESSAGE_SESSION_KEY = "ErrorMessage";

        // next es el siguiente middleware en la cadena
        // si no hay error, la petición sigue su camino normal
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Intenta ejecutar el siguiente middleware normalmente
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si algo lanza una excepción llega aquí
                string message = "Ha ocurrido un error inesperado.";

                // switch por tipo — cada excepción tiene su propio mensaje
                switch (ex)
                {
                    // Error de regla de negocio del dominio
                    // Ejemplo: "El vehículo no está disponible"
                    case BusinessException business:
                        message = business.Message;
                        break;

                    // Error del Mediator — UseCase no registrado
                    case MediatorException mediator:
                        message = mediator.Message;
                        break;

                    // Error de validación con lista de errores
                    // Ejemplo: "La placa es obligatoria. El modelo es inválido."
                    case CustomValidationException validation when validation.Errors.Count > 0:
                        message = string.Join(" ", validation.Errors);
                        break;

                    // Error de validación sin lista
                    case CustomValidationException validation:
                        message = validation.Message;
                        break;
                }

                // Guarda el mensaje en la sesión para que lo lea la vista de error
                await context.Session.LoadAsync(context.RequestAborted);
                context.Session.SetString(ERROR_MESSAGE_SESSION_KEY, message);

                // Redirige a la página de error
                context.Response.Redirect("/Home/Error");
            }
        }
    }

    // Método de extensión para registrar el middleware de forma limpia
    // Permite usar app.UseExceptionHandlerMiddleware() en Program.cs
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}