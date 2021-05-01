using System.Net.Http;
using System.Threading.Tasks;

namespace BikeRentalApi.Models.Repositories
{
    public interface IReservationApiRepository : IRepositoryAsync<Reservation>
    {
        Task<Reservation> GetByBikeId(int id);
    }

    public class ReservationApiRepository : ApiRepository<Reservation>, IReservationApiRepository
    {
        public ReservationApiRepository(IHttpClientFactory clientFactory)
            : base(clientFactory) { }

        public async Task<Reservation> GetByBikeId(int id)
        {
            using HttpResponseMessage response = await client
                .GetAsync($"{BikeRentalRoute.ReservationsByBikeId}/{id}");
            return await DeserializeFromResponse<Reservation>(response);
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            using HttpResponseMessage response = await client
                .GetAsync($"{BikeRentalRoute.CustomerFromReservation}/{id}");
            return await DeserializeFromResponse<Customer>(response);
        }

    }
}
