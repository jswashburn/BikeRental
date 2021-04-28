using System.Collections.Generic;

namespace BikeRental.Models
{
    public class BikeStore : BaseEntity
    {
        public BikeStore()
        {
            Bikes = new HashSet<Bike>();
            Employees = new HashSet<Employee>();
            Reservations = new HashSet<Reservation>();
        }

        public int StoreId { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? HourlyRate { get; set; }
        public double? Discount { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual ICollection<Bike> Bikes { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
