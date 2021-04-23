using BikeRental.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Data
{
    public class BikeRentalDbContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<BikeStore> BikeStores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public BikeRentalDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Seed();
    }
}
