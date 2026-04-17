using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Domain.Exceptions.Tarifa
{
    public class TarifaInvalidoException : BusinessException
    {
        public TarifaInvalidoException(string message) : base(message)
        {


        }
    }
}