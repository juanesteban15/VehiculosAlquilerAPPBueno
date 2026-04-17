using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Domain.Exceptions.Reserva
{
    public class ReservaInvalidoException : BusinessException
    {
        public ReservaInvalidoException(string message) : base(message)
        {


        }
    }
}