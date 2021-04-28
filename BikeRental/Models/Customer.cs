using System.Collections.Generic;

namespace BikeRental.Models
{
    public class Customer : Person
    {
        public Customer()
        {
            Reservations = new HashSet<Reservation>();
        }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
