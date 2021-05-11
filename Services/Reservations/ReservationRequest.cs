using BikeRentalApi.Models;
using System;

namespace Services.Reservations
{
    public class ReservationRequest
    {
        public Customer Customer { get; set; }
        public int RequestedBikeId { get; set; }
        public int DaysRequested { get; set; }

        public Reservation GetReservation(Bike bikeRequested)
        {
            Reservation reservation = new Reservation
            {
                BikeId = RequestedBikeId,
                CustomerId = Customer.Id,
                DateReserved = DateTime.Now,
                DateDue = DateTime.Now.AddDays(DaysRequested),
                GrandTotal = CalculateGrandTotal(bikeRequested, DaysRequested)
            };

            return reservation;
        }

        decimal CalculateGrandTotal(Bike bike, int daysRequested) =>
            (bike.Price * daysRequested) + (bike.Surcharge ?? 0);
    }
}
