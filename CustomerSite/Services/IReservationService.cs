using BikeRentalApi.Models;
using System.Threading.Tasks;

namespace CustomerSite.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(Customer customer, int bikeId);
        Task<Bike> GetBikeFromId(int bikeId);
        Task<bool> ReservationExists(Bike bike);
    }
}
