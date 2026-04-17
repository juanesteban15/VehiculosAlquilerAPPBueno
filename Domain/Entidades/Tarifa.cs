
using VehiculosAlquilerApp.Domain.Exceptions.Tarifa;

namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class Tarifa
    {
        public int Id { get; private set; }
        public Vehiculo Vehiculo { get; private set; }
        public decimal PrecioPorDia { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public bool Activa { get; private set; }

        public Tarifa(Vehiculo vehiculo, decimal precioPorDia, DateTime fechaInicio)
        {
            // Validaciones de negocio
            if (vehiculo == null)
                throw new TarifaInvalidoException("El vehículo es requerido para asignar una tarifa.");

            if (precioPorDia <= 0)
                throw new TarifaInvalidoException("El precio por día debe ser un valor positivo.");

            // Opcional: Validar que la tarifa no empiece en el pasado
            if (fechaInicio.Date < DateTime.Now.Date)
                throw new TarifaInvalidoException("La fecha de inicio de la tarifa no puede ser anterior a hoy.");

            Vehiculo = vehiculo;
            PrecioPorDia = precioPorDia;
            FechaInicio = fechaInicio;
            Activa = true; // Por defecto, una tarifa nueva nace activa
        }

        // Método para desactivar tarifas antiguas cuando se cree una nueva
        public void Desactivar()
        {
            Activa = false;
        }
    }
}