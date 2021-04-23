
using System.Collections.Generic;

namespace BikeRental.Models
{
    public class Bike : BaseEntity
    {
        public int OwningStoreId { get; set; }

        public int FrameSize { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Price { get; set; }
        public bool ElectricMotor { get; set; }
        public bool AllTerrain { get; set; }
        public bool Available { get; set; }
        public string BikeStyle { get; set; }

        public BikeStore OwningStore { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
