using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearComentarioPerfil
{
    public class CrearComentarioPerfilCommand : IRequest
    {
        public required int UsuarioEvaluadoId { get; set; }
        public required int AutorId { get; set; }
        public required int Calificacion { get; set; }
        public required string Comentario { get; set; }
    }
}
