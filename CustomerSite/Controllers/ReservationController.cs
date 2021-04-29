using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class ReservationController : Controller
    {
        readonly IRepositoryAsync<Reservation> _reservationsRepo;

        const string _routeReservations = "reservations";

        public ReservationController(IRepositoryAsync<Reservation> reservations)
        {
            _reservationsRepo = reservations;
        }

        public IActionResult Index(Customer customer)
        {
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Reservation reservation)
        {
            Reservation confirmed = await _reservationsRepo
                .InsertAsync(reservation, _routeReservations);
            return View("ConfirmReservation", confirmed);
        }
    }
}
