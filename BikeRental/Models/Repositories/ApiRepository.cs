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

        public async Task<IEnumerable<T>> GetAsync(string controller)
        {
            return await _client.GetFromJsonAsync<IEnumerable<T>>(controller);
        }

        public async Task<T> GetAsync(int id, string controller)
        {
            return await _client.GetFromJsonAsync<T>($"{controller}/{id}");
        }

        public async Task<T> DeleteAsync(int id, string controller)
        {
            using HttpResponseMessage response = await _client.DeleteAsync($"{controller}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> InsertAsync(T item, string controller)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync(controller, item);
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> UpdateAsync(T item, string controller)
        {
            using HttpResponseMessage response = await _client.PutAsJsonAsync(controller, item);
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
