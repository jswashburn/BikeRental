using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BikeRental.Models.Repositories
{
    public class ApiRepository<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        readonly IConfiguration _config;

        public ApiRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<T> DeleteAsync(int id, string controller)
        {
            using (var client = new HttpClient())
            {
                var responseMessage = await client.DeleteAsync($"{controller}/{id}");
                responseMessage.c
            }
        }
    }
}
