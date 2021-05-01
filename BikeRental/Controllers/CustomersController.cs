using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        IRepository<Customer> _customersRepo;

        public CustomersController(IRepository<Customer> customers)
        {
            _customersRepo = customers;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _customersRepo.Get();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            return _customersRepo.Get(id);
        }

        // GET: api/Customers/email/jsw@erau.edu
        [HttpGet("email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email)
        {
            return _customersRepo.Get().FirstOrDefault(c => c.EmailAddress == email);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public ActionResult<Customer> PutCustomer(Customer customer)
        {
            return _customersRepo.Update(customer);
        }

        // POST: api/Customers
        [HttpPost]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            return _customersRepo.Insert(customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            return _customersRepo.Delete(id);
        }

        bool CustomerExists(int id) => _customersRepo.Get().Any(c => c.Id == id);
    }
}
