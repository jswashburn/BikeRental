using System.Collections.Generic;

namespace BikeRentalApi.Models
{
    public class Employee : Person
    {
        public Employee()
        {
            InverseSupervisorNavigation = new HashSet<Employee>();
        }

        public string JobTitle { get; set; }
        public int? StoreId { get; set; }
        public int? SupervisorId { get; set; } // TODO: should be supervisor id

        public virtual BikeStore Store { get; set; }
        public virtual Employee SupervisorNavigation { get; set; }
        public virtual ICollection<Employee> InverseSupervisorNavigation { get; set; }
    }
}
