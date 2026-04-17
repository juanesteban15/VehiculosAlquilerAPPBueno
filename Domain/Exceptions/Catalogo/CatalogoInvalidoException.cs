
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Domain.Exceptions.Catalogo

{
    public class CatalogoInvalidoException : BusinessException
    {
        public CatalogoInvalidoException(string message) : base(message)
        {


        }
    }
}

