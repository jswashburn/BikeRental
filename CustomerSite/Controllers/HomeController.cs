using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepositoryAsync<Bike> _bikesRepo;
        const string _route = "bikes";

        public HomeController(IRepositoryAsync<Bike> bikes)
        {
            _bikesRepo = bikes;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Bike> bikes = await _bikesRepo.GetAsync(_route);
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
