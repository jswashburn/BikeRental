using System.Collections.Generic;

namespace BikeRental.Models
{
    public class Customer : Person
    {
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
