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
                Customer = new Customer(),
                RequestedBikeId = bikeId
            };

            return View(vm);
        }
    }
}
