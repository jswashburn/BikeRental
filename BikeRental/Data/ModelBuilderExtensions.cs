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
                    Available = true,
                    BikeStyle = "Mountain"
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
                    DateReserved = DateTime.Now,
                    DateReturned = DateTime.Now,
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
                });
        }
    }
}
