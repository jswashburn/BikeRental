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
            TempData["BikeId"] = bikeId;

            var vm = new CustomerReservationViewModel
            {
                Customer = new Customer(),
            };

            return View(vm);
        }
    }
}
