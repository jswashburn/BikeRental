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
        readonly IRepository<Employee> _employeesRepo;

        public EmployeesController(IRepository<Employee> employees)
        {
            _employeesRepo = employees;
        }

        // GET: api/Employees
        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            IEnumerable<Employee> employees = _employeesRepo.Get();
            if (employees == null)
                return NotFound();
            return Ok(employees.ToList());
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            Employee employee = _employeesRepo.Get(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        // PUT: api/Employees
        [HttpPut]
        public ActionResult<Employee> PutEmployee(Employee employee)
        {
            Employee updated = _employeesRepo.Update(employee);
            return Ok(updated);
        }

        // POST: api/Employees
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            if (EmployeeExists(employee.Id))
                return BadRequest($"Employee already exists! ID {employee.Id}");

            Employee created = _employeesRepo.Insert(employee);
            return CreatedAtAction(nameof(PostEmployee), created);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            Employee deleted = _employeesRepo.Delete(id);
            return Ok(deleted);
        }

        bool EmployeeExists(int id) => _employeesRepo.Get().Any(e => e.Id == id);
    }
}
