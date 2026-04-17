using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Application.Interface;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios
{
    public class CrearUsuario
    {
        private readonly IUsuarioRepository _repo;
        private readonly IEstadoUsuarioRepository _estadoUsuRepo;

        public CrearUsuario(IUsuarioRepository repo, IEstadoUsuarioRepository estadoUsuRepo)
        {
            _repo = repo;
            _estadoUsuRepo = estadoUsuRepo;
        }

        public void Ejecutar(
            string nombre,
            string apellido,
            string email,
            string pais,
            DateTime fechaNacimiento,
            List<(string numero, string tipo)> telefonos // Pluralizado para mayor claridad
        )
        {
            // 1. Obtener el estado inicial (Validando que exista)
            var estado = _estadoUsuRepo.ObtenerPorNombre(EstadoUsuario.Disponible)
                         ?? throw new Exception("Error interno: El estado inicial 'DISPONIBLE' no existe en el sistema.");

            // 2. Crear la instancia de Usuario (Aquí se disparan las validaciones del dominio)
            var usuario = new Usuario(nombre, apellido, email, pais, fechaNacimiento, estado);

            // 3. Agregar teléfonos si existen
            if (telefonos != null)
            {
                foreach (var tel in telefonos)
                {
                    usuario.AgregarTelefono(tel.numero, tel.tipo);
                }
            }

            // 4. Persistir a través del repositorio
            _repo.Guardar(usuario);
        }
    }
}