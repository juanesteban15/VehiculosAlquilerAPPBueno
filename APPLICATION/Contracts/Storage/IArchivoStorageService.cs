namespace VehiculosAlquilerApp.Application.Contracts.Storage
{
    public interface IArchivoStorageService
    {
        Task<string> GuardarAsync(ArchivoSubida archivo, string carpeta, string identificador);
    }
}
