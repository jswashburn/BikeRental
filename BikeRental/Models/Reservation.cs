using System;

namespace BikeRental.Models
{
    public class Reservation : BaseEntity
    {
        public int CustomerId { get; set; }
        public int BikeId { get; set; }
        public int CurrentStoreId { get; set; }

        public DateTime DateReserved { get; set; }
        public DateTime DateReturned { get; set; }
        public bool Archive { get; set; }

        public BikeStore BikeStore { get; set; }
        public Bike Bike { get; set; }
        public Customer Customer { get; set; }
    }
}
