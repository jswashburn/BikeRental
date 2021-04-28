using System;

namespace BikeRental.Models
{
    public class Reservation : BaseEntity
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int BikeId { get; set; }
        public int? CurrentStoreId { get; set; }
        public bool? Archive { get; set; }
        public DateTime DateReserved { get; set; }
        public DateTime? DateReturned { get; set; }

        public virtual Bike Bike { get; set; }
        public virtual BikeStore CurrentStore { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
