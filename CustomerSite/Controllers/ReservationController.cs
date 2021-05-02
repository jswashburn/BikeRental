using BikeRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CustomerSite.Services;

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
            Bike requestedBike = await _reservationService.GetBikeFromId(id);
            TempData["BikeId"] = id;
            return View(requestedBike);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Customer customer)
        {
            int bikeId = (int)TempData["BikeId"];
            Reservation createdReservation = await _reservationService
                .CreateReservation(customer, bikeId);

            return View("ReservationConfirmed", createdReservation);
        }
    }
}
