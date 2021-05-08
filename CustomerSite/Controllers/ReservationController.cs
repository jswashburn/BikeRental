using BikeRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CustomerSite.Services;
using CustomerSite.Models;

namespace CustomerSite.Controllers
{
    public class ReservationController : Controller
    {
        readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index(int id)
        {
            Bike requestedBike = await _reservationService.GetBikeFromId(id);

            if (requestedBike == null)
                return NotFound();

            TempData["BikeId"] = id;
            return View(requestedBike);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CustomerReservationViewModel vm)
        {
            int bikeId = (int)TempData["BikeId"];
            Bike bike = await _reservationService.GetBikeFromId(bikeId);

            if (bike == null)
                return BadRequest();

            bool reservationExists = await _reservationService.ReservationExists(bike);

            if (reservationExists)
                return View("ReservationAlreadyExists", bike);

            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return View(nameof(Index), bike);
            }

            Reservation createdReservation = await _reservationService
                .CreateReservation(vm.Customer, bikeId, vm.DaysRequested);

            return View("ReservationConfirmed", createdReservation);
        }
    }
}
