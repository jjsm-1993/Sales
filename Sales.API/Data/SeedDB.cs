using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                await _context.Countries.AddAsync(new Country { Name = "Colombia" });
                await _context.Countries.AddAsync(new Country { Name = "Perú" });
                await _context.Countries.AddAsync(new Country { Name = "México" });
                await _context.Countries.AddAsync(new Country { Name = "Costa Rica" });
                await _context.Countries.AddAsync(new Country { Name = "Argentina" });
                await _context.Countries.AddAsync(new Country { Name = "El Salvador" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
