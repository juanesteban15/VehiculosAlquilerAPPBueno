namespace VehiculosAlquilerApp.Application.Contracts.Queries.Chat
{
    public class ChatDetalleDto
    {
        public int Id { get; set; }
        public string Vehiculo { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Propietario { get; set; } = string.Empty;
        public int UsuarioActualId { get; set; }
        public List<MensajeChatDto> Mensajes { get; set; } = [];
    }

    public class MensajeChatDto
    {
        public int AutorId { get; set; }
        public string AutorNombre { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; }
    }
}
