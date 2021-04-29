using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IRepository<Employee> _employeesRepo;

        public EmployeesController(IRepository<Employee> employees)
        {
            _employeesRepo = employees;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeesRepo.Get();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            return _employeesRepo.Get(id);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public ActionResult<Employee> PutEmployee(Employee employee)
        {
            return _employeesRepo.Update(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            return _employeesRepo.Insert(employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            return _employeesRepo.Delete(id);
        }

        bool EmployeeExists(int id) => _employeesRepo.Get().Any(e => e.Id == id);
    }
}
