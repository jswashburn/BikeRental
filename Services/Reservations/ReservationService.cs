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

        public async Task<Reservation> CreateReservation(ReservationRequest request)
        {
            // Find the bike
            Bike requestedBike = await _bikesRepo
                .GetAsync(request.RequestedBikeId, BikeRentalRoute.Bikes);

            // Update the bike
            requestedBike.Available = false;
            requestedBike = await _bikesRepo
                .UpdateAsync(requestedBike, BikeRentalRoute.Bikes);

            // Update the customer
            request.Customer = await RegisterNewCustomerAsync(request.Customer);

            // Get the reservation object
            Reservation reservation = request.GetReservation(requestedBike);

            // Post it
            reservation = await _reservationsRepo
                .InsertAsync(reservation, BikeRentalRoute.Reservations);

            // We have to set these properties after posting or there will be key conflicts
            reservation.Customer = request.Customer;
            reservation.Bike = requestedBike;

            return reservation;
        }

        public async Task<Bike> FindBikeAsync(int id)
        {
            return await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);
        }

        async Task<Customer> RegisterNewCustomerAsync(Customer customer)
        {
            Customer existing = await _customersRepo.GetByEmailAsync(customer.EmailAddress);

            if (existing != null)
                return existing;

            Customer newCustomer = await _customersRepo
                .InsertAsync(customer, BikeRentalRoute.Customers);

            return newCustomer;
        }
    }
}
