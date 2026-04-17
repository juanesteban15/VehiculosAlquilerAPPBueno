using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Exceptions.Usuario;
using VehiculosAlquilerApp.Domain.ValueObjects;

    namespace VehiculosAlquilerApp.Domain.Entidades
    {
        public class Usuario
        {
            public int Id { get; private set; }
            public string Nombre { get; private set; }
            public string Apellido { get; private set; }
            public string Email { get; private set; }
            public string Pais { get; private set; }
            public DateTime FechaNacimiento { get; private set; }
            public DateTime FechaRegistro { get; private set; } 
            public EstadoUsuario Estado { get; private set; } 

            private List<Telefono> _telefonos = new List<Telefono>();
            public IReadOnlyCollection<Telefono> Telefonos => _telefonos.AsReadOnly();

            public Usuario(string nombre, string apellido, string email, string pais, DateTime fechaNacimiento, EstadoUsuario estado)
            {
                if (string.IsNullOrWhiteSpace(nombre)) throw new UsuarioInvalidoException("Nombre requerido");
                if (string.IsNullOrWhiteSpace(apellido)) throw new UsuarioInvalidoException("Apellido requerido");
                if (string.IsNullOrWhiteSpace(email)) throw new UsuarioInvalidoException("Email requerido");
                if (string.IsNullOrWhiteSpace(pais)) throw new UsuarioInvalidoException("Pais requerido");

                // Validación lógica corregida
                if (fechaNacimiento > DateTime.Now)
                    throw new UsuarioInvalidoException("La fecha de nacimiento no puede ser futura");

                Nombre = nombre;
                Apellido = apellido;
                Email = email;
                Pais = pais;
                FechaNacimiento = fechaNacimiento;
                FechaRegistro = DateTime.Now; // Guardamos el objeto DateTime completo
                Estado = estado ?? throw new ArgumentNullException(nameof(estado), "El estado es obligatorio");
            }

            public void AgregarTelefono(string numero, string tipo)
            {
                _telefonos.Add(new Telefono(numero, tipo));
            }
        }
    }