namespace VehiculosAlquilerApp.Application.Contracts.Queries.Reservas
{
    public class ReservaResumenDto
    {
        public int Id { get; set; }
        public string VehiculoPlaca { get; set; } = string.Empty;
        public string VehiculoNombre { get; set; } = string.Empty;
        public string PersonaNombre { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal PrecioTotal { get; set; }
        public int? ConversacionId { get; set; }
    }
}
