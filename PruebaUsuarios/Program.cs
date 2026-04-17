class Program
{
    static void Main(string[] args)
    {
        var usuario = new Usuario(
                  "Juan",
                  "Perez",
                  "juan@test.com",
                  "Colombia",
                  new DateTime(1995, 5, 10)
              );

        usuario.AgregarTelefono("3001234567", "personal");

        // Imprimir datos del usuario
        Console.WriteLine("=== USUARIO ===");
        Console.WriteLine($"Nombre: {usuario.nombre} {usuario.apellido}");
        Console.WriteLine($"Email: {usuario.email}");
        Console.WriteLine($"Pais: {usuario.pais}");
        Console.WriteLine($"Fecha Nacimiento: {usuario.fechaNacimiento.ToShortDateString()}");
        Console.WriteLine($"Fecha creacion ususario: {usuario.fechaRegistro}");
        // Imprimir teléfonos
        Console.WriteLine("\n=== TELEFONOS ===");

        foreach (var tel in usuario.Telefonos)
        {
            Console.WriteLine($"Numero: {tel.Numero} - Tipo: {tel.Tipo}");
        }
    }
}