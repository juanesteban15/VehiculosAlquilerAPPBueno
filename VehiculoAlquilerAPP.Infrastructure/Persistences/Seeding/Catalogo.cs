// ============================================================
// ARCHIVO: Seeding/CatalogoSeeder.cs
// ============================================================
// Crea los datos iniciales del catálogo en la base de datos
// Se ejecuta al arrancar la aplicación
// Si los datos ya existen no los duplica

using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Seeding
{
    internal class CatalogoSeeder : ISeedable
    {
        private readonly AlquilerDbContext _context;

        public CatalogoSeeder(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Primero sembramos TipoVehiculo porque otros catálogos dependen de él
            await SeedTiposVehiculoAsync();
            await SeedMarcasAsync();
            await SeedCategoriasAsync();
            await SeedSistemasTransmisionAsync();
            await SeedTiposCombustibleAsync();
            await SeedEstadosVehiculoAsync();
            await SeedEstadosReservaAsync();
            await SeedEstadosUsuarioAsync();
            await SeedMetodosPagoAsync();
            await SeedColoresVehiculoAsync();

            // Guarda todo de una sola vez al final
            await _context.SaveChangesAsync();
        }

        private async Task SeedTiposVehiculoAsync()
        {
            await AgregarTipoVehiculoSiNoExisteAsync("AUTO", "Automóvil", "Vehículo de pasajeros");
            await AgregarTipoVehiculoSiNoExisteAsync("MOTO", "Motocicleta", "Vehículo de dos ruedas");
            await AgregarTipoVehiculoSiNoExisteAsync("CAM", "Camioneta", "Vehículo utilitario");

            // SaveChanges aqui para que los TipoVehiculo tengan ID
            // antes de que Marca y Categoria los necesiten
            await _context.SaveChangesAsync();
        }

        private async Task SeedMarcasAsync()
        {
            var auto = _context.TiposVehiculo.First(t => t.Codigo == "AUTO");
            var moto = _context.TiposVehiculo.First(t => t.Codigo == "MOTO");
            var cam = _context.TiposVehiculo.First(t => t.Codigo == "CAM");

            await AgregarMarcaSiNoExisteAsync(auto, "Toyota");
            await AgregarMarcaSiNoExisteAsync(auto, "Chevrolet");
            await AgregarMarcaSiNoExisteAsync(auto, "Mazda");
            await AgregarMarcaSiNoExisteAsync(moto, "Honda");
            await AgregarMarcaSiNoExisteAsync(moto, "Yamaha");
            await AgregarMarcaSiNoExisteAsync(moto, "Suzuki");
            await AgregarMarcaSiNoExisteAsync(moto, "Bajaj");
            await AgregarMarcaSiNoExisteAsync(cam, "Ford");
            await AgregarMarcaSiNoExisteAsync(cam, "Nissan");
        }

        private async Task SeedCategoriasAsync()
        {
            var auto = _context.TiposVehiculo.First(t => t.Codigo == "AUTO");
            var moto = _context.TiposVehiculo.First(t => t.Codigo == "MOTO");

            await AgregarCategoriaSiNoExisteAsync(auto, "Sedán", "Automóvil de 4 puertas");
            await AgregarCategoriaSiNoExisteAsync(auto, "Hatchback", "Automóvil compacto");
            await AgregarCategoriaSiNoExisteAsync(auto, "SUV", "Vehículo deportivo utilitario");
            await AgregarCategoriaSiNoExisteAsync(moto, "Deportiva", "Moto de alto rendimiento");
            await AgregarCategoriaSiNoExisteAsync(moto, "Scooter", "Moto de ciudad");
            await AgregarCategoriaSiNoExisteAsync(moto, "Enduro", "Moto para terreno mixto");
        }

        private async Task SeedSistemasTransmisionAsync()
        {
            var auto = _context.TiposVehiculo.First(t => t.Codigo == "AUTO");
            var moto = _context.TiposVehiculo.First(t => t.Codigo == "MOTO");

            await AgregarTransmisionSiNoExisteAsync(auto, SistemaTransmision.Manual);
            await AgregarTransmisionSiNoExisteAsync(auto, SistemaTransmision.Automatico);
            await AgregarTransmisionSiNoExisteAsync(moto, SistemaTransmision.Manual);
            await AgregarTransmisionSiNoExisteAsync(moto, SistemaTransmision.Automatico);
        }

        private async Task SeedTiposCombustibleAsync()
        {
            var auto = _context.TiposVehiculo.First(t => t.Codigo == "AUTO");
            var moto = _context.TiposVehiculo.First(t => t.Codigo == "MOTO");

            await AgregarCombustibleSiNoExisteAsync(auto, TipoCombustible.Gasolina);
            await AgregarCombustibleSiNoExisteAsync(auto, TipoCombustible.Diesel);
            await AgregarCombustibleSiNoExisteAsync(auto, TipoCombustible.Electrico);
            await AgregarCombustibleSiNoExisteAsync(auto, TipoCombustible.Hibrido);
            await AgregarCombustibleSiNoExisteAsync(moto, TipoCombustible.Gasolina);
            await AgregarCombustibleSiNoExisteAsync(moto, TipoCombustible.Electrico);
        }

        private async Task AgregarTipoVehiculoSiNoExisteAsync(string codigo, string nombre, string descripcion)
        {
            bool existe = _context.TiposVehiculo.Any(t => t.Codigo == codigo);

            if (!existe)
                await _context.TiposVehiculo.AddAsync(new TipoVehiculo(codigo, nombre, descripcion));
        }

        private async Task AgregarMarcaSiNoExisteAsync(TipoVehiculo tipo, string nombre)
        {
            bool existe = _context.Marcas.Any(m => m.TipoVehiculo.Codigo == tipo.Codigo && m.Nombre == nombre);

            if (!existe)
                await _context.Marcas.AddAsync(new Marca(tipo, nombre));
        }

        private async Task AgregarCategoriaSiNoExisteAsync(TipoVehiculo tipo, string nombre, string descripcion)
        {
            bool existe = _context.Categorias.Any(c => c.TipoVehiculo.Codigo == tipo.Codigo && c.Nombre == nombre);

            if (!existe)
                await _context.Categorias.AddAsync(new Categoria(tipo, nombre, descripcion));
        }

        private async Task AgregarTransmisionSiNoExisteAsync(TipoVehiculo tipo, string nombre)
        {
            string nombreNormalizado = nombre.Trim().ToUpper();
            bool existe = _context.SistemasTransmision.Any(t => t.TipoVehiculo.Codigo == tipo.Codigo && t.Nombre == nombreNormalizado);

            if (!existe)
                await _context.SistemasTransmision.AddAsync(new SistemaTransmision(tipo, nombre));
        }

        private async Task AgregarCombustibleSiNoExisteAsync(TipoVehiculo tipo, string nombre)
        {
            string nombreNormalizado = nombre.Trim().ToUpper();
            bool existe = _context.TiposCombustible.Any(c => c.TipoVehiculo.Codigo == tipo.Codigo && c.Nombre == nombreNormalizado);

            if (!existe)
                await _context.TiposCombustible.AddAsync(new TipoCombustible(tipo, nombre));
        }

        private async Task SeedEstadosVehiculoAsync()
        {
            if (_context.EstadosVehiculo.Any()) return;

            var estados = new[]
            {
                new EstadoVehiculo(EstadoVehiculo.Disponible),
                new EstadoVehiculo(EstadoVehiculo.Rentado),
                new EstadoVehiculo(EstadoVehiculo.Mantenimiento),
                new EstadoVehiculo(EstadoVehiculo.NoDisponible),
            };

            await _context.EstadosVehiculo.AddRangeAsync(estados);
        }

        private async Task SeedEstadosReservaAsync()
        {
            if (_context.EstadosReserva.Any()) return;

            var estados = new[]
            {
                new EstadoReserva(EstadoReserva.Pendiente),
                new EstadoReserva(EstadoReserva.Confirmada),
                new EstadoReserva(EstadoReserva.Completada),
                new EstadoReserva(EstadoReserva.Cancelada),
            };

            await _context.EstadosReserva.AddRangeAsync(estados);
        }

        private async Task SeedEstadosUsuarioAsync()
        {
            string[] nombres =
            {
                EstadoUsuario.Verificado,
                EstadoUsuario.Sinverificar,
                EstadoUsuario.Betado,
                EstadoUsuario.NoDisponible,
            };

            foreach (string nombre in nombres)
            {
                string nombreNormalizado = nombre.Trim().ToUpper();
                bool existe = _context.EstadosUsuario.Any(e => e.Nombre == nombreNormalizado);

                if (!existe)
                    await _context.EstadosUsuario.AddAsync(new EstadoUsuario(nombre));
            }
        }

        private async Task SeedMetodosPagoAsync()
        {
            string[] metodos =
            {
                MetodoPago.Efectivo,
                MetodoPago.TarjetaCredito,
                MetodoPago.Transferencia,
                MetodoPago.Pse
            };

            foreach (string metodo in metodos)
            {
                string nombreNormalizado = metodo.Trim().ToUpper();
                bool existe = _context.MetodosPago.Any(m => m.Nombre == nombreNormalizado);

                if (!existe)
                    await _context.MetodosPago.AddAsync(new MetodoPago(metodo));
            }
        }

        private async Task SeedColoresVehiculoAsync()
        {
            string[] colores =
            {
                "AMARILLO",
                "AZUL",
                "BEIGE",
                "BLANCO",
                "CAFE",
                "DORADO",
                "GRIS",
                "MARRON",
                "MORADO",
                "NARANJA",
                "NEGRO",
                "PLATA",
                "ROJO",
                "VERDE",
                "VINOTINTO"
            };

            foreach (string nombre in colores)
            {
                string nombreNormalizado = nombre.Trim().ToUpper();
                bool existe = _context.Colores.Any(c => c.Nombre == nombreNormalizado);

                if (!existe)
                    await _context.Colores.AddAsync(new ColorVehiculo(nombreNormalizado));
            }
        }
    }
}
