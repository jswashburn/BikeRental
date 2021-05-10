using BikeRentalApi.Models;
using System.Threading.Tasks;

namespace Services.Reservations
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(Customer customer, int bikeId, int daysRequested);
        Task<Bike> FindBikeAsync(int bikeId);
    }
}
