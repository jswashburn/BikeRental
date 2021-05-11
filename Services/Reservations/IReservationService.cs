using BikeRentalApi.Models;
using System.Threading.Tasks;

namespace Services.Reservations
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(ReservationRequest request);
        Task<Bike> FindBikeAsync(int bikeId);
    }
}
