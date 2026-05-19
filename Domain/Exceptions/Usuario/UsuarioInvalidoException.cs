using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Domain.Exceptions.Usuario
{

    public class UsuarioInvalidoException : BusinessException
    {
        public UsuarioInvalidoException(string message, string v) : base(message)
        {


        }
    }
}