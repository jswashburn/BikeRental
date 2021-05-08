using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using System;
using System.Threading.Tasks;
using BikeRentalApi;
using BikeRentalApi.Repositories.Extensions;

namespace CustomerSite.Services
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
            Bike bike = await GetBikeFromId(bikeId);
            Customer existing = await PostCustomerIfEmailNotFound(customer);
            Reservation created = await PostNewReservation(bike, existing, daysRequested);

            return created;
        }

        public async Task<Bike> GetBikeFromId(int id)
        {
            Bike bike = await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);
            return bike;
        }

        async Task<Reservation> PostNewReservation(Bike bike, Customer customer, int daysRequested)
        {
            Reservation reservation = new Reservation
            {
                BikeId = bike.Id,
                CustomerId = customer.Id,
                DateReserved = DateTime.Now,
                DateDue = DateTime.Now.AddDays(daysRequested)
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

        async Task<Customer> PostCustomerIfEmailNotFound(Customer customer) =>
            await _customersRepo.GetByEmailAsync(customer.EmailAddress) ??
            await PostNewCustomer(customer);

        async Task<Customer> PostNewCustomer(Customer customer) =>
            await _customersRepo.InsertAsync(customer, BikeRentalRoute.Customers);
    }
}
