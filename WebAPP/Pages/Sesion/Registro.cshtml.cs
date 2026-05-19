using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Exceptions;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.RegistrarDocumentoUsuario;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Sesion
{
    public class RegistroModel : PageModel
    {
        private readonly IMediator _mediator;

        public RegistroModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public RegistroUsuarioInput Input { get; set; } = new();

        public string? ErrorMessage { get; private set; }

        public IReadOnlyList<PaisOption> Paises => PaisOptions;

        public void OnGet(string? tipoCuenta)
        {
            if (string.Equals(tipoCuenta, "Empresa", StringComparison.OrdinalIgnoreCase))
                Input.TipoCuenta = "Empresa";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            PaisOption? paisNacimiento = FindPais(Input.Pais);
            if (paisNacimiento is null)
                ModelState.AddModelError("Input.Pais", "Selecciona un país de nacimiento de la lista.");

            if (Input.TipoCuenta == "Empresa")
            {
                if (string.IsNullOrWhiteSpace(Input.NombreEmpresa))
                    ModelState.AddModelError("Input.NombreEmpresa", "El nombre de la empresa es obligatorio.");

                if (string.IsNullOrWhiteSpace(Input.NitEmpresa))
                    ModelState.AddModelError("Input.NitEmpresa", "El NIT de la empresa es obligatorio.");

                if (Input.RuntEmpresa is null || Input.RuntEmpresa.Length == 0)
                {
                    ModelState.AddModelError("Input.RuntEmpresa", "Debes subir el RUNT de la empresa.");
                }
                else
                {
                    string extension = Path.GetExtension(Input.RuntEmpresa.FileName).ToLower();
                    string[] permitidas = [".jpg", ".jpeg", ".png", ".pdf"];
                    if (!permitidas.Contains(extension))
                        ModelState.AddModelError("Input.RuntEmpresa", "El RUNT debe ser JPG, PNG o PDF.");
                }
            }
            else
            {
                Input.TipoCuenta = "Persona";
            }

            PaisOption? paisTelefono = null;
            if (!string.IsNullOrWhiteSpace(Input.TelefonoNumero))
            {
                paisTelefono = FindPais(Input.TelefonoPais);
                if (paisTelefono is null)
                    ModelState.AddModelError("Input.TelefonoPais", "Selecciona el país del número telefónico.");
            }

            if (!ModelState.IsValid)
                return Page();

            var command = new CrearUsuarioCommand
            {
                Nombre = Input.Nombre,
                Apellido = Input.Apellido,
                Email = Input.Email,
                Password = Input.Password,
                Pais = paisNacimiento!.Nombre,
                FechaNacimiento = Input.FechaNacimiento,
                TipoCuenta = Input.TipoCuenta,
                NombreEmpresa = Input.NombreEmpresa,
                NitEmpresa = Input.NitEmpresa
            };

            if (!string.IsNullOrWhiteSpace(Input.TelefonoNumero))
            {
                command.Telefonos.Add(new TelefonoDTO
                {
                    Numero = $"{paisTelefono!.Indicativo} {Input.TelefonoNumero.Trim()}",
                    Tipo = string.IsNullOrWhiteSpace(Input.TelefonoTipo) ? "personal" : Input.TelefonoTipo
                });
            }

            try
            {
                int usuarioId = await _mediator.Send(command);

                if (Input.TipoCuenta == "Empresa")
                    await GuardarRuntEmpresaAsync(usuarioId);

                return RedirectToPage("/Sesion/Login", new { registered = "true" });
            }
            catch (CustomValidationException ex)
            {
                foreach (string error in ex.Errors)
                    ModelState.AddModelError(string.Empty, error);

                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }

        private async Task GuardarRuntEmpresaAsync(int usuarioId)
        {
            if (Input.RuntEmpresa is null || Input.RuntEmpresa.Length == 0)
                return;

            string extension = Path.GetExtension(Input.RuntEmpresa.FileName).ToLower();
            string[] permitidas = [".jpg", ".jpeg", ".png", ".pdf"];
            if (!permitidas.Contains(extension))
            {
                ModelState.AddModelError("Input.RuntEmpresa", "El RUNT debe ser JPG, PNG o PDF.");
                return;
            }

            await _mediator.Send(new RegistrarDocumentoUsuarioCommand
            {
                UsuarioId = usuarioId,
                TipoDocumento = "RUNT empresa",
                Archivo = ToArchivoSubida(Input.RuntEmpresa)
            });
        }

        private static ArchivoSubida ToArchivoSubida(IFormFile archivo)
        {
            return new ArchivoSubida
            {
                NombreArchivo = archivo.FileName,
                Length = archivo.Length,
                Contenido = archivo.OpenReadStream()
            };
        }

        private static PaisOption? FindPais(string? nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            return PaisOptions.FirstOrDefault(p =>
                string.Equals(p.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static readonly PaisOption[] PaisOptions =
        [
            new("Argentina", "+54"),
            new("Bolivia", "+591"),
            new("Brasil", "+55"),
            new("Canadá", "+1"),
            new("Chile", "+56"),
            new("Colombia", "+57"),
            new("Costa Rica", "+506"),
            new("Cuba", "+53"),
            new("Ecuador", "+593"),
            new("El Salvador", "+503"),
            new("España", "+34"),
            new("Estados Unidos", "+1"),
            new("Guatemala", "+502"),
            new("Honduras", "+504"),
            new("México", "+52"),
            new("Nicaragua", "+505"),
            new("Panamá", "+507"),
            new("Paraguay", "+595"),
            new("Perú", "+51"),
            new("Puerto Rico", "+1"),
            new("República Dominicana", "+1"),
            new("Uruguay", "+598"),
            new("Venezuela", "+58")
        ];
    }

    public record PaisOption(string Nombre, string Indicativo);

    public class RegistroUsuarioInput
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes confirmar la contraseña.")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país es obligatorio.")]
        [Display(Name = "País de nacimiento")]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;

        [Display(Name = "Tipo de cuenta")]
        public string TipoCuenta { get; set; } = "Persona";

        [Display(Name = "Nombre de la empresa")]
        public string? NombreEmpresa { get; set; }

        [Display(Name = "NIT de la empresa")]
        public string? NitEmpresa { get; set; }

        [Display(Name = "RUNT de la empresa")]
        public IFormFile? RuntEmpresa { get; set; }

        [Display(Name = "Teléfono")]
        public string? TelefonoNumero { get; set; }

        [Display(Name = "País del número")]
        public string? TelefonoPais { get; set; } = "Colombia";

        [Display(Name = "Tipo de teléfono")]
        public string? TelefonoTipo { get; set; } = "personal";
    }
}
