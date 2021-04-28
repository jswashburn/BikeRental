using System.Collections.Generic;

namespace BikeRental.Models
{
    public class Employee : Person
    {
        public Employee()
        {
            InverseSupervisorNavigation = new HashSet<Employee>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int? StoreId { get; set; }
        public int Supervisor { get; set; }

        public virtual BikeStore Store { get; set; }
        public virtual Employee SupervisorNavigation { get; set; }
        public virtual ICollection<Employee> InverseSupervisorNavigation { get; set; }
    }
}
