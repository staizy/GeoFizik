using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GeoFizik.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<PicketValue> PicketValues { get; set; }
        public DbSet<Picket> Pickets { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaPoint> AreaPoints { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<ProfilePoint> ProfilePoints { get; set; }

        public static string ConnectionString { get; set; }

        private static ApplicationContext instance;
        public static ApplicationContext getInstance()
        {
            if (instance == null)
            {
                instance = new ApplicationContext(ConnectionString.ToString());
                //instance.Database.EnsureDeleted();
                //var exists = instance.Database.EnsureCreated();

                instance.Customers.Load();
                instance.Projects.Load();
                instance.AreaPoints.Load();
                instance.Areas.Load();
                instance.Operators.Load();
                instance.ProfilePoints.Load();
                instance.Profiles.Load();
                instance.Pickets.Load();
                instance.PicketValues.Load();
                //if (exists) instance.Customers.Add(DefaultData);
                instance.SaveChanges();
            }
            return instance;
        }

        public ApplicationContext() { }

        public ApplicationContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Server=LAPTOP-DA4EO4II;Database={ConnectionString};Trusted_Connection=True;TrustServerCertificate=True");
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-DA4EO4II;Database=GeoKrizo2024;Trusted_Connection=True;TrustServerCertificate=True");
        }*/

        /*static Customer DefaultData = new Customer()
        {

            Name = "Кит Магнит",
            Email = "asdfasdfa@mail.ru",
            Projects = new()
            {
                new() { Name = "Сборка", Address="Лермонтова 116", Areas = new()
                    {
                        new() { Name="Лесная зона", Points = new()
                        {
                            new() { X=0, Y=0 },
                            new() { X=3, Y=22 },
                            new() { X=6, Y=21 },
                            new() { X=6, Y=23 },
                        },
                            Profiles=new() 
                            {
                                new()
                                {
                                    Points= new()
                                    {
                                        new () {X=3, Y=5},
                                        new () {X=5, Y=7 },
                                        new () {X=11, Y=6 },
                                        new () {X=21, Y=9 },
                                        new () {X=30, Y=10 },
                                    },
                                    Pickets = new()
                                    {
                                        new () {X=25, Y=9, PicketValues= new() { new() {Amplitude = 2, H_value = 3 },
                                                                                 new() {Amplitude = 1, H_value = 4},
                                                                                 new() {Amplitude = 2, H_value = 6}, } },
                                        new () {X=6, Y=9},
                                        new () {X=13, Y=3},
                                        new () {X=16, Y=1},
                                    }
                                },                                
                                new()
                                {
                                    Points= new()
                                    {
                                        new () {X=2, Y=21 },
                                        new () {X=6, Y=11 },
                                        new () {X=15, Y=14 },
                                        new () {X=13, Y=26 },
                                        new () {X=33, Y=11 },
                                    }
                                }
                            } },
                        new() { Name="Пустынка", Points = new()
                            {
                                new() { X=31, Y=-20 },
                                new() { X=33, Y=-30 },
                                new() { X=38, Y=-15 },
                                new() { X=36, Y=-11 },
                                new() { X=9, Y=-11 },
                            },
                        Profiles=new()
                            {
                                new()
                                {
                                    Points= new()
                                    {
                                        new () {X=32, Y=-22},
                                        new () {X=34, Y=-24 },
                                        new () {X=35, Y=-20 },
                                    },
                                    Pickets = new() {}
                                    
                                },
                                new()
                                {
                                    Points= new() { }                                    
                                }
                            }
                        },
                        new() { Name="Зеленка" }
                    }
                }
            }
        };*/
    }
}
