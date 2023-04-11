using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;

namespace Sales.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*crear indices unicos*/
            //campo unico e irrepetible, ademas de colocarlo como indice por nombre, ya me lo ordena alfabeticamente, ahorra un proceso de ordenamiento
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();

            //indices compuestos (se COLOCA con CountryId por ejm para expresar asi la composicion del indice compuesto, para que sea unico)
            modelBuilder.Entity<State>().HasIndex("CountryId", "Name").IsUnique();
            modelBuilder.Entity<City>().HasIndex("StateId", "Name").IsUnique();

            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
