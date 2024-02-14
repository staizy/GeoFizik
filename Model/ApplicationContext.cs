using Microsoft.EntityFrameworkCore;

namespace GeoFizik.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Picket> Pickets { get; set; }
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-DA4EO4II;Database=GeoKrizo2024;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
