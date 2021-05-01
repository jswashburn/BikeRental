using BikeRentalApi.Models;

namespace CustomerSite.Models
{
    public class ReservationViewModel
    {
        public Customer Customer { get; set; }
        public Reservation Reservation { get; set; }
        public Bike Bike { get; set; }
    }
}
