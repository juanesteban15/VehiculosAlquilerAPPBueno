using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.ActualizarPerfil;
using VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilEditar;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace WebAPP.Pages.Perfil
{
    public class EditarModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditarModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public PerfilInput Input { get; set; } = new();

        public List<DocumentoPerfilDto> Documentos { get; private set; } = [];
        public string? FotoPerfilRuta { get; private set; }
        public string? SuccessMessage { get; private set; }
        public string? ErrorMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            PerfilEdicionDto? perfil = await _mediator.Send(new GetPerfilEditarQuery { UsuarioId = usuarioId.Value });
            if (perfil is null)
                return RedirectToPage("/Sesion/Login");

            Input.Pais = perfil.Pais;
            Input.FechaNacimiento = perfil.FechaNacimiento;
            FotoPerfilRuta = perfil.FotoPerfilRuta;
            Documentos = perfil.Documentos;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId is null)
                return RedirectToPage("/Sesion/Login");

            ValidarArchivo(Input.FotoPerfil, "La foto de perfil debe ser JPG o PNG.", soloImagen: true);
            ValidarArchivo(Input.DocumentoIdentidad, "El documento de identidad debe ser JPG, PNG o PDF.");
            ValidarArchivo(Input.LicenciaConduccion, "La licencia de conduccion debe ser JPG, PNG o PDF.");

            if (!ModelState.IsValid)
            {
                await CargarResumenAsync(usuarioId.Value);
                return Page();
            }

            try
            {
                await _mediator.Send(new ActualizarPerfilCommand
                {
                    UsuarioId = usuarioId.Value,
                    Pais = Input.Pais,
                    FechaNacimiento = Input.FechaNacimiento,
                    FotoPerfil = ToArchivoSubida(Input.FotoPerfil),
                    DocumentoIdentidad = ToArchivoSubida(Input.DocumentoIdentidad),
                    LicenciaConduccion = ToArchivoSubida(Input.LicenciaConduccion)
                });

                SuccessMessage = "Perfil actualizado correctamente.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            await CargarResumenAsync(usuarioId.Value);
            return Page();
        }

        private async Task CargarResumenAsync(int usuarioId)
        {
            PerfilEdicionDto? perfil = await _mediator.Send(new GetPerfilEditarQuery { UsuarioId = usuarioId });
            if (perfil is null)
                return;

            FotoPerfilRuta = perfil.FotoPerfilRuta;
            Documentos = perfil.Documentos;
        }

        private void ValidarArchivo(IFormFile? archivo, string mensaje, bool soloImagen = false)
        {
            if (archivo is null || archivo.Length == 0)
                return;

            string extension = Path.GetExtension(archivo.FileName).ToLower();
            string[] permitidas = soloImagen ? [".jpg", ".jpeg", ".png"] : [".jpg", ".jpeg", ".png", ".pdf"];
            if (!permitidas.Contains(extension))
                ModelState.AddModelError(string.Empty, mensaje);
        }

        private static ArchivoSubida? ToArchivoSubida(IFormFile? archivo)
        {
            if (archivo is null || archivo.Length == 0)
                return null;

            return new ArchivoSubida
            {
                NombreArchivo = archivo.FileName,
                Length = archivo.Length,
                Contenido = archivo.OpenReadStream()
            };
        }
    }

    public class PerfilInput
    {
        [Required(ErrorMessage = "El pais es obligatorio.")]
        [Display(Name = "Pais de nacimiento")]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;

        [Display(Name = "Foto de perfil")]
        public IFormFile? FotoPerfil { get; set; }

        [Display(Name = "Documento de identidad")]
        public IFormFile? DocumentoIdentidad { get; set; }

        [Display(Name = "Licencia de conduccion")]
        public IFormFile? LicenciaConduccion { get; set; }
    }
}
