using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using System.Net.Http;
using System.Threading.Tasks;

namespace BikeRentalApi.Repositories.Extensions
{
    public static class RepositoryExtentions
    {
        public static async Task<Bike> GetReservationByBike(
            this IRepositoryAsync<Bike> repo, int id)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.ReservationsByBikeId}/{id}");
            return await repo.DeserializeFromResponse<Bike>(response);
        }

        public static async Task<Customer> GetByEmailAsync(
            this IRepositoryAsync<Customer> repo, string email)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.CustomersByEmail}/{email}");
            return await repo.DeserializeFromResponse<Customer>(response);
        }

        public static async Task<Reservation> GetByBikeId(
            this IRepositoryAsync<Reservation> repo, int id)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.ReservationsByBikeId}/{id}");
            return await repo.DeserializeFromResponse<Reservation>(response);
        }

        public static async Task<Customer> GetCustomerAsync(
            this IRepositoryAsync<Reservation> repo, int id)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.CustomerFromReservation}/{id}");
            return await repo.DeserializeFromResponse<Customer>(response);
        }
        public static async Task<Reservation> GetReservationId(
            this IRepositoryAsync<Reservation> repo, int id)
        {
            using HttpResponseMessage response = await repo.Client
                .GetAsync($"{BikeRentalRoute.Reservations}/{id}");
            return await repo.DeserializeFromResponse<Reservation>(response);
        }
    }
}
