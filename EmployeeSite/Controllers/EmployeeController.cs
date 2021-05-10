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
    public class EmployeeController : Controller
    {    
        readonly IRepositoryAsync<Employee> _empRepo;

        public EmployeeController(IRepositoryAsync<Employee> emp)
        {
            _empRepo = emp;
        }
        // GET: Employee
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> emp = await _empRepo.GetAsync(BikeRentalRoute.Employees);
            return View(emp);
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
        public IActionResult Edit()
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
            Employee res = await _empRepo.GetAsync(id, BikeRentalRoute.Reservations);
            if (res == null)
                return NotFound();
            await _empRepo.DeleteAsync(id, "reservation");
            return View(nameof(Index));
        }
    }
}
