using System;

class Program
{
    static void Main(string[] args)
    {
        var carro = new TipoVehiculo ("Carro","Carro",null);
        var moto = new TipoVehiculo ("Moto","Moto",null);


        var marca = new Marca(carro, "Toyota");
        var categoria = new Categoria(carro, "Sedan");
        var combustible = new TipoCombustible(carro, "Gasolina");
        var transmision = new SistemaTransmision(carro, "Automatica");

        var usuario = new Usuario("Juan", "Perez", "juan@mail.com", "Colombia", new DateTime(1990, 5, 10));

        var vehiculo = new Vehiculo(
            "ABC123",
            carro,
            marca,
            categoria,
            combustible,
            transmision,
            2022,
            usuario
        );



        Console.WriteLine("Vehículo creado:");
        Console.WriteLine($"Placa: {vehiculo.Placa}");
        Console.WriteLine($"Marca: {vehiculo.Marca.Nombre}");
        Console.WriteLine($"Categoría: {vehiculo.Categoria.Nombre}");
        Console.WriteLine($"Combustible: {vehiculo.TipoCombustible.Nombre}");
        Console.WriteLine($"Propietario: {vehiculo.Propietario.nombre}");

        Console.ReadLine();
    }
}