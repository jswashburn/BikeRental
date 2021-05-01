using System.Net.Http;
using System.Threading.Tasks;

namespace BikeRentalApi.Models.Repositories
{
    public interface IBikeApiRepository : IRepositoryAsync<Bike>
    {
        Task<Bike> GetReservationByBike(int id);
    }

    public class BikeApiRepository : ApiRepository<Bike>, IBikeApiRepository
    {
        public BikeApiRepository(IHttpClientFactory clientFactory)
            : base(clientFactory) { }

        public async Task<Bike> GetReservationByBike(int id)
        {
            using HttpResponseMessage response = await client
                .GetAsync($"{BikeRentalRoute.ReservationsByBikeId}/{id}");
            return await DeserializeFromResponse<Bike>(response);
        }
    }
}
