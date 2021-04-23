using System.Collections.Generic;

namespace BikeRental.Models
{
    public class BikeStore : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Discount { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Bike> Bikes { get; set; }
    }
}
