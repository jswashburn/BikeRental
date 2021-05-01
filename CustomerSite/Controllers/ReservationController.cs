using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BikeRentalApi;

namespace CustomerSite.Controllers
{
    public class ReservationController : Controller
    {
        // TODO: Create a service to handle the logic of creating reservations
        // TODO: New project for API Repository?
        // TODO: New project for Reservation Service/All services?

        readonly IReservationApiRepository _reservationsRepo;
        readonly IRepositoryAsync<Bike> _bikesRepo;
        readonly ICustomerApiRepository _customersRepo;

        public ReservationController(IReservationApiRepository reservations,
            IRepositoryAsync<Bike> bikes, ICustomerApiRepository customers)
        {
            _reservationsRepo = reservations;
            _bikesRepo = bikes;
            _customersRepo = customers;
        }

        public async Task<IActionResult> Index(int id)
        {
            var requestedBike = await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);
            TempData["BikeId"] = id;
            return View(requestedBike);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Customer customer)
        {
            Bike bike = await GetBikeFromId((int)TempData["BikeId"]);
            if (bike == null)
                return BadRequest();

            if (await ReservationExists(bike))
                return View("ReservationAlreadyExists", bike);

            Customer existing = await PostCustomerIfEmailNotFound(customer);
            Reservation reservation = await PostNewReservation(bike, existing);

            return View("ReservationConfirmed", reservation);
        }

        private async Task<Customer> PostCustomerIfEmailNotFound(Customer customer)
        {
            return await _customersRepo
                .GetByEmailAsync(customer.EmailAddress) ?? await PostNewCustomer(customer);
        }

        async Task<Bike> GetBikeFromId(int id)
        {
            return await _bikesRepo.GetAsync(id, BikeRentalRoute.Bikes);
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

        async Task<Customer> PostNewCustomer(Customer customer)
        {
            return await _customersRepo.InsertAsync(customer, BikeRentalRoute.Customers);
        }

        async Task<bool> ReservationExists(Bike bike)
        {
            return await _reservationsRepo.GetByBikeId(bike.Id) != null;
        }
    }
}
