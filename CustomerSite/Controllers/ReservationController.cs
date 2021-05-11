using BikeRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Reservations;
using System.Threading.Tasks;

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
            Bike requestedBike = await _reservationService.FindBikeAsync(id);

            if (requestedBike == null)
                return NotFound();

            return View(requestedBike);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationRequest reservationRequest)
        {
            // Refresh page if model validation fails
            if (!ModelState.IsValid)
            {
                TempData["InvalidSubmit"] = true;
                return RedirectToAction(nameof(Index), reservationRequest.RequestedBikeId);
            }

            // Create a reservation and move to confirmation page
            Reservation createdReservation = await _reservationService
                .CreateReservation(reservationRequest);

            return View("ReservationConfirmed", createdReservation);
        }
    }
}
