namespace BikeRental.Models
{
    public class Employee : Person
    {
        public int StoreId { get; set; }
        public int? SupervisorId { get; set; }

        public string JobTitle { get; set; }

        public Employee Supervisor { get; set; }
    }
}
