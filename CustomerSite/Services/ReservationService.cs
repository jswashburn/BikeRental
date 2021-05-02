using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using System;
using System.Threading.Tasks;
using BikeRentalApi;
using BikeRentalApi.Repositories.Extensions;

namespace CustomerSite.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(Customer customer, int bikeId);
        Task<Bike> GetBikeFromId(int bikeId);
    }

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

        public async Task<Reservation> CreateReservation(Customer customer, int bikeId)
        {
            Bike bike = await GetBikeFromId(bikeId);
            Customer existing = await PostCustomerIfEmailNotFound(customer);
            return await PostNewReservation(bike, existing);
        }

        public async Task<Bike> GetBikeFromId(int id)
        {
            Bike bike = await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);
            if (bike == null)
                throw new ArgumentException($"Could not find bike {id}");
            return bike;
        }

        public async Task<bool> ReservationExists(Bike bike)
        {
            return await _reservationsRepo.GetByBikeId(bike.Id) != null;
        }

        async Task<Reservation> PostNewReservation(Bike bike, Customer customer)
        {
            Reservation reservation = new Reservation
            {
                BikeId = bike.Id,
                CustomerId = customer.Id,
                DateReserved = DateTime.Now
            };

            reservation = await _reservationsRepo
                .InsertAsync(reservation, BikeRentalRoute.Reservations);

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
