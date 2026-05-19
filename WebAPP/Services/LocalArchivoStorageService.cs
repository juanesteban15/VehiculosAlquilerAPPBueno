using VehiculosAlquilerApp.Application.Contracts.Storage;

namespace WebAPP.Services
{
    public class LocalArchivoStorageService : IArchivoStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalArchivoStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> GuardarAsync(ArchivoSubida archivo, string carpeta, string identificador)
        {
            string extension = Path.GetExtension(archivo.NombreArchivo).ToLower();
            string relativeFolder = Path.Combine(carpeta, identificador);
            string absoluteFolder = Path.Combine(_environment.WebRootPath, relativeFolder);
            Directory.CreateDirectory(absoluteFolder);

            string fileName = $"{Guid.NewGuid():N}{extension}";
            string absolutePath = Path.Combine(absoluteFolder, fileName);

            using (FileStream stream = File.Create(absolutePath))
            {
                await archivo.Contenido.CopyToAsync(stream);
            }

            return "/" + Path.Combine(relativeFolder, fileName).Replace("\\", "/");
        }
    }
}
