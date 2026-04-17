
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Domain.Exceptions.Color
{
    public class ColorInvalidoException : BusinessException
    {
        public ColorInvalidoException(string message) : base(message)
        {


        }
    }
}

