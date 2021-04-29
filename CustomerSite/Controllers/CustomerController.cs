using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class CustomerController : Controller
    {
        readonly IRepositoryAsync<Customer> _customersRepo;

        const string _routeCustomers = "customers";

        public CustomerController(IRepositoryAsync<Customer> customers)
        {
            _customersRepo = customers;
        }

        public async Task<IActionResult> Index(int id)
        {
            Customer customer = await _customersRepo.GetAsync(id, _routeCustomers);
            return View(customer);
        }

        public IActionResult GuestCustomer()
        {
            return View(new Customer());
        }
    }
}
