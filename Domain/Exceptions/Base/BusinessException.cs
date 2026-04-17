namespace VehiculosAlquilerApp.Domain.Exceptions.Base
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}