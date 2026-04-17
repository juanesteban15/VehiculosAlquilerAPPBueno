using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var carro = new tipo_vehiculo ("Carro", "Carro", null);


        var marca = new marca(carro, "Toyota");
        var categoria = new categoria(carro, "Sedan");
        var combustible = new tipo_combustible(carro, "Gasolina");
        var transmision = new sistema_transmision(carro, "Automatica");

        var usuarioDueño = new usuario("Juan", "Perez", "juan@mail.com", "Colombia", new DateTime(1990, 5, 10));

        var usuarioArrenda = new usuario("Sebas", "daza", "jsdsn@mail.com", "Colombia", new DateTime(1990, 5, 10));


        var _vehiculo = new vehiculo(
            "ABC123",
            carro,
            marca,
            categoria,
            combustible,
            transmision,
            2022,
            usuarioDueño,
            estado_vehiculo
        );

        var estado = new estado_reserva("Activa");

        var tarifa = new tarifa(
            _vehiculo,
            150000,
            DateTime.Now
        );

        var metodoPago = new metodo_pago("PSE");

        var reserva = new reserva(
            _vehiculo,
            usuarioArrenda,
            DateTime.Now,
            DateTime.Now.AddDays(3),
            estado,
            450000,
            tarifa,
            metodoPago,
            "Reserva de prueba"
        );

        Console.WriteLine("===== RESERVA CREADA =====");
        Console.WriteLine($"Cliente: {usuarioArrenda.nombre} {usuarioArrenda.apellido}");
        Console.WriteLine($"Vehiculo: {_vehiculo.placa}");
        Console.WriteLine($"Marca: {marca.nombre}");
        Console.WriteLine($"Precio Total: {reserva.PrecioTotal}");
        Console.WriteLine($"Estado: {estado.nombre}");
        Console.WriteLine($"Metodo Pago: {metodoPago.nombre}");
        Console.WriteLine($"Fecha Inicio: {reserva.FechaInicio}");
        Console.WriteLine($"Fecha Fin: {reserva.FechaFin}");





    }
}