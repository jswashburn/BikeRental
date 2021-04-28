using BikeRental.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Data
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Seed();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BikeRental;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bike>(entity =>
            {
                entity.Property(e => e.BikeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Bike_ID");

                entity.Property(e => e.AllTerrainSuspension).HasColumnName("All_Terrain_Suspension");

                entity.Property(e => e.BikeStyle)
                    .HasMaxLength(50)
                    .HasColumnName("Bike_Style");

                entity.Property(e => e.ElectricMotor).HasColumnName("Electric_Motor");

                entity.Property(e => e.FrameSize).HasColumnName("Frame_Size");

                entity.Property(e => e.OwningStoreId).HasColumnName("Owning_Store_ID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Surcharge).HasColumnType("money");

                entity.HasOne(d => d.OwningStore)
                    .WithMany(p => p.Bikes)
                    .HasForeignKey(d => d.OwningStoreId)
                    .HasConstraintName("FK_Bikes_Bike_Stores_Current");
            });

            modelBuilder.Entity<BikeStore>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.ToTable("Bike_Stores");

                entity.Property(e => e.StoreId)
                    .ValueGeneratedNever()
                    .HasColumnName("Store_ID");

                entity.Property(e => e.HourlyRate)
                    .HasColumnType("money")
                    .HasColumnName("Hourly_Rate");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("Phone_Number");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("Customer_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("Phone_Number");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Employee_ID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Job_Title");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Employees_Bike_Stores");

                entity.HasOne(d => d.SupervisorNavigation)
                    .WithMany(p => p.InverseSupervisorNavigation)
                    .HasForeignKey(d => d.Supervisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.ReservationId)
                    .ValueGeneratedNever()
                    .HasColumnName("Reservation_ID");

                entity.Property(e => e.BikeId).HasColumnName("Bike_ID");

                entity.Property(e => e.CurrentStoreId).HasColumnName("Current_Store_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DateReserved).HasColumnName("Date_Reserved");

                entity.Property(e => e.DateReturned).HasColumnName("Date_Returned");

                entity.HasOne(d => d.Bike)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.BikeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Bikes");

                entity.HasOne(d => d.CurrentStore)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.CurrentStoreId)
                    .HasConstraintName("FK_Reservation_Bike_Stores");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Customers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
