
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Domain.ValueObjects;
    public class DocumentoUsuario
    {
        public Usuario Usuario { get; private set; }

        public string UbicacionDocumento { get; private set; }
        public string UbicacionLicencia { get; private set; }
        public DateTime? FechaLicencia { get; private set; }

        public DateTime FechaRegistro { get; private set; }

        public DocumentoUsuario(
            Usuario usuario,
            string ubicacionDocumento,
            string ubicacionLicencia,
            DateTime? fechaLicencia
        )
        {
            if (usuario == null)
                throw new Exception("Usuario requerido");

            if (fechaLicencia.HasValue && fechaLicencia > DateTime.Today)
                throw new Exception("Fecha de licencia inválida");

            Usuario = usuario;
            UbicacionDocumento = ubicacionDocumento;
            UbicacionLicencia = ubicacionLicencia;
            FechaLicencia = fechaLicencia;
            FechaRegistro = DateTime.Now;
        }
    }
}