using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic>().HasData(new Comic { Id = 1, Name = "Marvel" },
              new Comic { Id = 2, Name = "DC" });
            modelBuilder.Entity<SuperHero>().HasData(new SuperHero
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Parker",
                HeroName = "Spiderman",                
                ComicId = 1,
            },
             new SuperHero
             {
                 Id = 2,
                 FirstName = "Bruce",
                 LastName = "Wyane",
                 HeroName = "Batman",
                 ComicId = 2
             }
         );    

          
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Comic> Comics { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
