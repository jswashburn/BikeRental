using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BikeRentalApi;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        readonly IBikeApiRepository _bikesRepo;

        public HomeController(IBikeApiRepository bikes)
        {
            _bikesRepo = bikes;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Bike> bikes = await _bikesRepo.GetAsync(BikeRentalRoute.Bikes);
            return View(bikes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
