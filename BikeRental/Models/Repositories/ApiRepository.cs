using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BikeRentalApi.Models.Repositories
{
    public class ApiRepository<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        protected readonly HttpClient client;

        public string ClientName { get; set; } = "Customer";

        public ApiRepository(IHttpClientFactory clientFactory)
        {
            // Notice in the startup.cs there is an HttpClient configured. ClientName
            // tells the factory which of those HttpClients to get. (There is only one right now)
            client = clientFactory.CreateClient(ClientName);
        }

        public async Task<IEnumerable<T>> GetAsync(string route)
        {
            using HttpResponseMessage response = await client.GetAsync(route);
            return await DeserializeFromResponse<IEnumerable<T>>(response);
        }

        public async Task<T> GetAsync(int id, string route)
        {
            using HttpResponseMessage response = await client.GetAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> DeleteAsync(int id, string route)
        {
            using HttpResponseMessage response = await client.DeleteAsync($"{route}/{id}");
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> InsertAsync(T item, string route)
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync(route, item);
            return await DeserializeFromResponse<T>(response);
        }

        public async Task<T> UpdateAsync(T item, string route)
        {
            using HttpResponseMessage response = await client.PutAsJsonAsync(route, item);
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
