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
        readonly IRepository<Customer> _customersRepo;

        public CustomersController(IRepository<Customer> customers)
        {
            _customersRepo = customers;
        }

        // GET: api/Customers
        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            IEnumerable<Customer> customers = _customersRepo.Get();
            if (customers == null)
                return NotFound();
            return Ok(customers.ToList());
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            Customer customer = _customersRepo.Get(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        // GET: api/Customers/email/jsw@erau.edu
        [HttpGet("email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email)
        {
            Customer customer = _customersRepo.Get().FirstOrDefault(c => c.EmailAddress == email);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        // PUT: api/Customers/
        [HttpPut]
        public ActionResult<Customer> PutCustomer(Customer customer)
        {
            Customer updated = _customersRepo.Update(customer);
            return Ok(updated);
        }

        // POST: api/Customers
        [HttpPost]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            if (CustomerExists(customer.Id))
                return BadRequest($"Customer already exists! Id {customer.Id}");

            Customer created = _customersRepo.Insert(customer);
            return CreatedAtAction(nameof(PostCustomer), created);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            Customer deleted = _customersRepo.Delete(id);
            return Ok(deleted);
        }

        bool CustomerExists(int id) => _customersRepo.Get().Any(c => c.Id == id);
    }
}
