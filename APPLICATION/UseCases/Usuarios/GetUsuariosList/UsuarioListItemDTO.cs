using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.ValueObjects;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.GetUsuariosList
{
    public class UsuarioListItemDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}