// ============================================================
// ARCHIVO: Seeding/SeedDb.cs
// ============================================================
// Coordinador de todos los seeders
// Es la única clase pública del seeding
// Program.cs la llama al arrancar la aplicación

using Microsoft.EntityFrameworkCore;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Seeding
{
    public class SeedDb
    {
        private readonly AlquilerDbContext _context;

        public SeedDb(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Aplica las migraciones pendientes automáticamente al arrancar
                   await _context.Database.MigrateAsync();

            // Ejecuta el seeder del catálogo
            await new CatalogoSeeder(_context).SeedAsync();
        }
    }
}
