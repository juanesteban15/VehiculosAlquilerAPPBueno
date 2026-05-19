namespace VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones
{
    public class NotificacionDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public string? Ruta { get; set; }
        public bool Leida { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
