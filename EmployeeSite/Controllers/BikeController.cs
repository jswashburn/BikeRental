using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using EmployeeSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BikeRentalApi.Repositories.Extensions;
using BikeRentalApi;
using Microsoft.AspNetCore.Http;

namespace EmployeeSite.Controllers
{
    public class BikeController : Controller
    {
        readonly IRepositoryAsync<Bike> _bikeRepo;

        public BikeController(IRepositoryAsync<Bike> bike)
        {
            _bikeRepo = bike;
        }
        // GET: BikeController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Bike> bike = await _bikeRepo.GetAsync(BikeRentalRoute.Bikes);
            return View(bike);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Delete(int id)
        {
            Bike res = await _bikeRepo.GetAsync(id, BikeRentalRoute.Reservations);
            if (res == null)
                return NotFound();
            await _bikeRepo.DeleteAsync(id, "reservation");
            return View(nameof(Index));
        }
    }
}
