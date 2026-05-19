namespace VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles
{
    public class PerfilEdicionDto
    {
        public string Pais { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string? FotoPerfilRuta { get; set; }
        public List<DocumentoPerfilDto> Documentos { get; set; } = [];
    }

    public class DocumentoPerfilDto
    {
        public string TipoDocumento { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public string RutaArchivo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; }
    }

    public class PerfilPublicoDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Iniciales { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string? FotoPerfilRuta { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = "Persona";
        public string? NombreEmpresa { get; set; }
        public string? NitEmpresa { get; set; }
        public double CalificacionPromedio { get; set; }
        public int TotalComentarios { get; set; }
    }

    public class VehiculoPerfilDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string TipoVehiculo { get; set; } = string.Empty;
        public int Modelo { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class ComentarioPerfilDto
    {
        public string AutorNombre { get; set; } = string.Empty;
        public int Calificacion { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }

    public class PerfilPublicoResult
    {
        public PerfilPublicoDto? Perfil { get; set; }
        public List<VehiculoPerfilDto> Vehiculos { get; set; } = [];
        public List<ComentarioPerfilDto> Comentarios { get; set; } = [];
    }
}
