using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerAoo.Domain.Exceptions.Vehiculo
{
    public class VehiculoInvalidoException : BusinessException
    {
        public VehiculoInvalidoException(string message) : base(message)
        {


        }
    }
}