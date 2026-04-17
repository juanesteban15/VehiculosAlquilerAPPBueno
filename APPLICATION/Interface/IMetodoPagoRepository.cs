using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Application.Interface
{

    public interface IMetodoPagoRepository
    {
        MetodoPago ObtenerPorId(int Id); 
        MetodoPago ObtenerPorNombre(string Nombre);
    }

}