using BikeRentalApi;
using BikeRentalApi.Models;
using Services.Extensions;
using Services.Repositories;
using System;
using System.Threading.Tasks;

namespace Services.Reservations
{
    public class ReservationService : IReservationService
    {
        readonly IRepositoryAsync<Customer> _customersRepo;
        readonly IRepositoryAsync<Reservation> _reservationsRepo;
        readonly IRepositoryAsync<Bike> _bikesRepo;

        public ReservationService(IRepositoryAsync<Customer> customers,
            IRepositoryAsync<Reservation> reservations, IRepositoryAsync<Bike> bikes)
        {
            _customersRepo = customers;
            _reservationsRepo = reservations;
            _bikesRepo = bikes;
        }

        public async Task<Reservation> CreateReservation(Customer customer, int bikeId, int daysRequested)
        {
            Bike bike = await FindBikeAsync(bikeId);
            Customer existing = await RegisterNewCustomerAsync(customer);

            return await PostNewReservation(bike, existing, daysRequested);
        }

        public async Task<Bike> FindBikeAsync(int id)
        {
            return await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);
        }

        async Task<Reservation> PostNewReservation(Bike bike, Customer customer, int daysRequested)
        {
            Reservation reservation = new Reservation
            {
                BikeId = bike.Id,
                CustomerId = customer.Id,
                DateReserved = DateTime.Now,
                DateDue = DateTime.Now.AddDays(daysRequested),
                GrandTotal = CalculateGrandTotal(bike, daysRequested)
            };

            reservation = await _reservationsRepo
                .InsertAsync(reservation, BikeRentalRoute.Reservations);

            bike.Available = false;
            bike = await _bikesRepo
                .UpdateAsync(bike, BikeRentalRoute.Bikes);

            reservation.Customer = customer;
            reservation.Bike = bike;

            return reservation;
        }

        async Task<Customer> RegisterNewCustomerAsync(Customer customer)
        {

            Customer existing = await _customersRepo
                .GetByEmailAsync(customer.EmailAddress);

            if (existing != null)
                return existing;

            Customer newCustomer = await _customersRepo
                .InsertAsync(customer, BikeRentalRoute.Customers);

            return newCustomer;
        }

        decimal CalculateGrandTotal(Bike bike, int daysRequested)
        {
            decimal subtotal = bike.Price * daysRequested;
            return subtotal + (bike.Surcharge ?? 0);
        }
    }
}
