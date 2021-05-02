using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BikeRentalApi.Repositories
{
    public class ApiRepository<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        public HttpClient Client { get; }

        public ApiRepository(IHttpClientFactory clientFactory)
        {
            Client = clientFactory.CreateClient("Client");
        }

        public async Task<IEnumerable<T>> GetAsync(string route)
        {
            using HttpResponseMessage response = await Client.GetAsync(route);
            return await DeserializeFromResponse<IEnumerable<T>>(response);
        }

        public async Task<T> GetAsync(int id, string route)
        {
            using HttpResponseMessage response = await Client.GetAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> DeleteAsync(int id, string route)
        {
            using HttpResponseMessage response = await Client.DeleteAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> InsertAsync(T item, string route)
        {
            using HttpResponseMessage response = await Client.PostAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> UpdateAsync(T item, string route)
        {
            using HttpResponseMessage response = await Client.PutAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<TResult> DeserializeFromResponse<TResult>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TResult>();

            return default;
        }
    }
}
