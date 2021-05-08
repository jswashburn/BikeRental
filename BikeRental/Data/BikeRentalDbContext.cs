using BikeRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApi.Data
{
    public partial class BikeRentalDbContext : DbContext
    {
        public virtual DbSet<Bike> Bikes { get; set; }
        public virtual DbSet<BikeStore> BikeStores { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        public BikeRentalDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Seed();
    }
}
