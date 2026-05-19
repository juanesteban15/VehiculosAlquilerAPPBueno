using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Exceptions.Usuario;
using VehiculosAlquilerApp.Domain.ValueObjects;

namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = default!;
        public string Apellido { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public string Pais { get; private set; } = default!;
        public string TipoCuenta { get; private set; } = "Persona";
        public string? NombreEmpresa { get; private set; }
        public string? NitEmpresa { get; private set; }
        public string? FotoPerfilRuta { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public DateTime FechaRegistro { get; private set; }
        public EstadoUsuario Estado { get; private set; } = default!;

        private readonly List<Telefono> _telefonos = new();
        public IReadOnlyCollection<Telefono> Telefonos => _telefonos.AsReadOnly();

        protected Usuario() { }

        public Usuario(
            string nombre,
            string apellido,
            string email,
            string passwordHash,
            string pais,
            DateTime fechaNacimiento,
            EstadoUsuario estado,
            string tipoCuenta = "Persona",
            string? nombreEmpresa = null,
            string? nitEmpresa = null)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new UsuarioInvalidoException("Nombre requerido", nameof(nombre));
            if (string.IsNullOrWhiteSpace(apellido)) throw new UsuarioInvalidoException("Apellido requerido", nameof(apellido));
            if (string.IsNullOrWhiteSpace(email)) throw new UsuarioInvalidoException("Email requerido", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new UsuarioInvalidoException("Contraseña requerida", nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(pais)) throw new UsuarioInvalidoException("Pais requerido", nameof(pais));

            if (fechaNacimiento > DateTime.Now)
                throw new UsuarioInvalidoException("La fecha de nacimiento no puede ser futura", nameof(fechaNacimiento));

            Nombre = nombre.Trim();
            Apellido = apellido.Trim();
            Email = email.Trim().ToLower();
            PasswordHash = passwordHash;
            Pais = pais.Trim();
            ConfigurarTipoCuenta(tipoCuenta, nombreEmpresa, nitEmpresa);
            FechaNacimiento = fechaNacimiento;
            FechaRegistro = DateTime.Now;
            Estado = estado ?? throw new UsuarioInvalidoException(nameof(estado), "El estado es obligatorio");
        }

        public void AgregarTelefono(string numero, string tipo)
        {
            _telefonos.Add(new Telefono(numero, tipo));
        }

        public void ActualizarPerfil(string pais, DateTime fechaNacimiento)
        {
            if (string.IsNullOrWhiteSpace(pais))
                throw new UsuarioInvalidoException("Pais requerido", nameof(pais));

            if (fechaNacimiento > DateTime.Now)
                throw new UsuarioInvalidoException("La fecha de nacimiento no puede ser futura", nameof(fechaNacimiento));

            Pais = pais.Trim();
            FechaNacimiento = fechaNacimiento;
        }

        public void ActualizarFotoPerfil(string fotoPerfilRuta)
        {
            if (string.IsNullOrWhiteSpace(fotoPerfilRuta))
                throw new UsuarioInvalidoException("La foto de perfil es obligatoria", nameof(fotoPerfilRuta));

            FotoPerfilRuta = fotoPerfilRuta.Trim();
        }

        public void ConfigurarTipoCuenta(string tipoCuenta, string? nombreEmpresa, string? nitEmpresa)
        {
            string tipoNormalizado = string.IsNullOrWhiteSpace(tipoCuenta) ? "Persona" : tipoCuenta.Trim();
            if (tipoNormalizado != "Persona" && tipoNormalizado != "Empresa")
                throw new UsuarioInvalidoException("El tipo de cuenta no es válido", nameof(tipoCuenta));

            if (tipoNormalizado == "Empresa")
            {
                if (string.IsNullOrWhiteSpace(nombreEmpresa))
                    throw new UsuarioInvalidoException("El nombre de la empresa es obligatorio", nameof(nombreEmpresa));

                if (string.IsNullOrWhiteSpace(nitEmpresa))
                    throw new UsuarioInvalidoException("El NIT de la empresa es obligatorio", nameof(nitEmpresa));

                NombreEmpresa = nombreEmpresa.Trim();
                NitEmpresa = nitEmpresa.Trim();
            }
            else
            {
                NombreEmpresa = null;
                NitEmpresa = null;
            }

            TipoCuenta = tipoNormalizado;
        }
    }
}
