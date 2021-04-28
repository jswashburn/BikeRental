using System.Collections.Generic;

namespace BikeRental.Models
{
    public class Customer : Person
    {
        public Customer()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
