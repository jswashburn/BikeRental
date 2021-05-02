using BikeRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CustomerSite.Services;
using System;

namespace CustomerSite.Controllers
{
    public class ReservationController : Controller
    {
        IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index(int id)
        {
            try
            {
                Bike requestedBike = await _reservationService.GetBikeFromId(id);
                TempData["BikeId"] = id;
                return View(requestedBike);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Customer customer)
        {
            int bikeId = (int)TempData["BikeId"];
            Bike bike = await _reservationService.GetBikeFromId(bikeId);

            if (!await _reservationService.ReservationExists(bike))
            {
                Reservation createdReservation = await _reservationService
                    .CreateReservation(customer, bikeId);

                return View("ReservationConfirmed", createdReservation);
            }
            return View("ReservationAlreadyExists", bike);
        }
    }
}
