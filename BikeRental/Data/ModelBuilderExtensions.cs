﻿using BikeRentalApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BikeRentalApi.Data
{
    public static class ModelBuilderExtensions
    {
        // Alter/Amend seed data here. EF will make the appropriate modifications to the DB.
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedBikes(modelBuilder);
            SeedBikeStores(modelBuilder);
            SeedCustomers(modelBuilder);
            SeedEmployees(modelBuilder);
            SeedReservations(modelBuilder);
        }
        static void SeedBikes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>().HasData(
                new Bike
                {
                    Id = 1,
                    OwningStoreId = 1,
                    FrameSize = 100,
                    Surcharge = 99.99m,
                    Price = 200.00m,
                    ElectricMotor = false,
                    AllTerrainSuspension = null,
                    Available = false,
                    BikeStyle = "Mountain"
                },
                new Bike
                {
                    Id = 2,
                    OwningStoreId = 1,
                    FrameSize = 230,
                    Surcharge = 199.99m,
                    Price = 60.00m,
                    ElectricMotor = false,
                    AllTerrainSuspension = null,
                    Available = false,
                    BikeStyle = "Road"
                },
                new Bike
                {
                    Id = 3,
                    OwningStoreId = 2,
                    FrameSize = 45,
                    Surcharge = 10.50m,
                    Price = 60.00m,
                    ElectricMotor = false,
                    AllTerrainSuspension = true,
                    Available = true,
                    BikeStyle = "Road"
                },
                new Bike
                {
                    Id = 4,
                    OwningStoreId = 1,
                    FrameSize = 200,
                    Surcharge = 399.99m,
                    Price = 600.00m,
                    ElectricMotor = true,
                    AllTerrainSuspension = true,
                    Available = true,
                    BikeStyle = "Dirt"
                },
                new Bike
                {
                    Id = 5,
                    OwningStoreId = 1,
                    FrameSize = 75,
                    Surcharge = 199.99m,
                    Price = 60.00m,
                    ElectricMotor = false,
                    AllTerrainSuspension = null,
                    Available = true,
                    BikeStyle = "Trike"
                });
        }

        static void SeedReservations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    CustomerId = 1,
                    BikeId = 1,
                    CurrentStoreId = 1,
                    DateReserved = DateTime.Now.AddDays(-32), // reserved it for 30 days
                    DateDue = DateTime.Now.AddDays(-2),       
                    DateReturned = DateTime.Now,              // returned today, 2 days after due
                    Archive = true
                },
                new Reservation
                {
                    Id = 2,
                    CustomerId = 1,
                    BikeId = 2,
                    CurrentStoreId = 1,
                    DateReserved = DateTime.Now.AddDays(-14),
                    DateDue = DateTime.Now.AddDays(14),
                    Archive = true
                });
        }

        static void SeedEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    StoreId = 1,
                    SupervisorId = 2,
                    JobTitle = "Cashier",
                    FirstName = "Maksim",
                    LastName = "Kasperov",
                    PhoneNumber = "867-5309",
                    EmailAddress = "mkasperov@bikesRus.com"
                },
                new Employee
                {
                    Id = 2,
                    StoreId = 1,
                    SupervisorId = null,
                    JobTitle = "Manager",
                    FirstName = "Jason",
                    LastName = "Dopke",
                    PhoneNumber = "555-5555",
                    EmailAddress = "jdopke@bikesRus.com"
                });
        }

        static void SeedCustomers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Craig",
                    LastName = "Bernall",
                    PhoneNumber = "123-4567",
                    EmailAddress = "cbernall@gmail.com"
                });
        }

        static void SeedBikeStores(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BikeStore>().HasData(
                new BikeStore
                {
                    Id = 1,
                    PhoneNumber = "1-800-BIKE",
                    HourlyRate = 15.00m,
                    Discount = 0.10d,
                    Latitude = 34.749432m,
                    Longitude = -77.421997m
                },
                new BikeStore
                {
                    Id = 2,
                    PhoneNumber = "1-800-BIKE",
                    HourlyRate = 15.00m,
                    Discount = 0.10d,
                    Latitude = 37.749432m,
                    Longitude = -71.421997m
                });
        }

    }
}
