using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BikeRentalApi.Models.Repositories
{
    public class ApiRepository<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        readonly HttpClient _client;

        public string ClientName { get; set; } = "Customer";

        public ApiRepository(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient(ClientName);
        }

        public async Task<IEnumerable<T>> GetAsync(string route)
        {
            return await _client.GetFromJsonAsync<IEnumerable<T>>(route);
        }

        public async Task<T> GetAsync(int id, string route)
        {
            return await _client.GetFromJsonAsync<T>($"{route}/{id}");
        }

        public async Task<T> DeleteAsync(int id, string route)
        {
            using HttpResponseMessage response = await _client.DeleteAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> InsertAsync(T item, string route)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> UpdateAsync(T item, string route)
        {
            using HttpResponseMessage response = await _client.PutAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        async Task<TResult> DeserializeFromResponse<TResult>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TResult>();

            return default;
        }
    }
}
