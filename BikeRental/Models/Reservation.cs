using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRentalApi.Models
{
    public class Reservation : BaseEntity
    {
        public int CustomerId { get; set; }
        public int BikeId { get; set; }
        public int? CurrentStoreId { get; set; }
        public bool? Archive { get; set; }
        public DateTime DateReserved { get; set; }  // set when reserve button is clicked
        public DateTime DateDue { get; set; }       // chosen by user
        public DateTime? DateReturned { get; set; } // set by employee
        public decimal GrandTotal { get; set; }

        public virtual Bike Bike { get; set; }
        public virtual BikeStore CurrentStore { get; set; }
        public virtual Customer Customer { get; set; }
        [NotMapped]
        public string SearchString { get; set; }
    }
}
