using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Interface
{
    public interface IUsuarioRepository
    {
        void Guardar(Usuario usuario);
        Usuario ObtenerPorId(int Id);
        Usuario ObtenerPorNombre(string Nombre);
    }
}