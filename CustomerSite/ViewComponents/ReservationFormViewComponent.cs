using Microsoft.AspNetCore.Mvc;
using BikeRentalApi.Models;
using CustomerSite.Models;

namespace CustomerSite.ViewComponents
{
    public class ReservationFormViewComponent : ViewComponent
    {
        public ReservationFormViewComponent()
        {
        }

        public IViewComponentResult Invoke(int bikeId)
        {
            var vm = new CustomerReservationViewModel
            {
                RequestedBikeId = bikeId
            };

            return View(vm);
        }
    }
}
