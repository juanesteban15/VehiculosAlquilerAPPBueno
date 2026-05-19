

using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Domain.ValueObjects { 
    public class DocumentoVehiculo
    {
        public Vehiculo Vehiculo { get; private set; }

        public string Soat { get; private set; }
        public string Tecnomecanico { get; private set; }
        public string Multas { get; private set; }

        public DateTime FechaVencimientoSoat { get; private set; }
        public DateTime FechaVencimientoTecnomecanica { get; private set; }

        public DateTime FechaRegistro { get; private set; }

        public DocumentoVehiculo(
            Vehiculo vehiculo,
            string soat,
            string tecnomecanico,
            DateTime fechaSoat,
            DateTime fechaTecno,
            string multas = null
        )
        {
            if (Vehiculo == null)
                throw new Exception("Vehículo requerido");

            if (string.IsNullOrWhiteSpace(soat))
                throw new Exception("SOAT requerido");

            if (string.IsNullOrWhiteSpace(tecnomecanico))
                throw new Exception("Tecnomecánica requerida");

            if (fechaSoat <= DateTime.Today)
                throw new Exception("SOAT vencido");

            if (fechaTecno <= DateTime.Today)
                throw new Exception("Tecnomecánica vencida");

            Vehiculo = vehiculo;
            Soat = soat;
            Tecnomecanico = tecnomecanico;
            FechaVencimientoSoat = fechaSoat;
            FechaVencimientoTecnomecanica = fechaTecno;
            Multas = multas;
            FechaRegistro = DateTime.Now;
        }
    }
}