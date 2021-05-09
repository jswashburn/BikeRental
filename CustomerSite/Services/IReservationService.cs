using BikeRentalApi.Models;
using System.Threading.Tasks;
using System;

namespace CustomerSite.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(Customer customer, int bikeId, int daysRequested);
        Task<Bike> GetBikeFromId(int bikeId);
    }
}
