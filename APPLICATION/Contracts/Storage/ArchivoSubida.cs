namespace VehiculosAlquilerApp.Application.Contracts.Storage
{
    public class ArchivoSubida
    {
        public required string NombreArchivo { get; set; }
        public required Stream Contenido { get; set; }
        public required long Length { get; set; }
    }
}
