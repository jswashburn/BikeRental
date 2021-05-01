using Microsoft.AspNetCore.Mvc;
using BikeRentalApi.Models;

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
            return View(new Customer());
        }
    }
}
