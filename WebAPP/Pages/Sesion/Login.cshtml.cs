using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Security;

namespace WebAPP.Pages.Sesion
{
    public class LoginModel : PageModel
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginModel(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; private set; }
        public string? SuccessMessage { get; private set; }

        public void OnGet(string? registered)
        {
            if (registered == "true")
                SuccessMessage = "Usuario registrado correctamente. Ya puedes iniciar sesión.";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var usuario = await _usuarioRepository.GetByEmailAsync(Email);

            if (usuario is null || !PasswordHasher.Verify(Password, usuario.PasswordHash))
            {
                ErrorMessage = "Email o contraseña incorrectos.";
                return Page();
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioEmail", usuario.Email);

            return RedirectToPage("/Index");
        }
    }
}
