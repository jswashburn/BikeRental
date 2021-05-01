using System.Net.Http;
using System.Threading.Tasks;

namespace BikeRentalApi.Models.Repositories
{
    public interface ICustomerApiRepository : IRepositoryAsync<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }

    public class CustomerApiRepository : ApiRepository<Customer>, ICustomerApiRepository
    {
        public CustomerApiRepository(IHttpClientFactory clientFactory)
            : base(clientFactory) { }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            using HttpResponseMessage response = await client
                .GetAsync($"{BikeRentalRoute.CustomersByEmail}/{email}");
            return await DeserializeFromResponse<Customer>(response);
        }
    }
}
