using Microsoft.AspNetCore.Mvc;
using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using System.Threading.Tasks;
using BikeRentalApi.Repositories.Extensions;

namespace CustomerSite.ViewComponents
{
    public class AvailabilityCheckViewComponent : ViewComponent
    {
        readonly IRepositoryAsync<Reservation> _reservationsRepo;

        public AvailabilityCheckViewComponent(IRepositoryAsync<Reservation> reservations)
        {
            _reservationsRepo = reservations;
        }

        public async Task<IViewComponentResult> InvokeAsync(int bikeId)
        {
            Reservation reservation = await _reservationsRepo.GetByBikeIdAsync(bikeId);
            return View(reservation);
        }
    }
}
