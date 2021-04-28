using BikeRental.Models;
using BikeRental.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeStoresController : ControllerBase
    {
        IRepository<BikeStore> _storesRepo;

        public BikeStoresController(IRepository<BikeStore> stores)
        {
            _storesRepo = stores;
        }

        // GET: api/BikeStores
        [HttpGet]
        public IEnumerable<BikeStore> GetBikeStores()
        {
            return _storesRepo.Get();
        }

        // GET: api/BikeStores/5
        [HttpGet("{id}")]
        public ActionResult<BikeStore> GetBikeStore(int id)
        {
            return _storesRepo.Get(id);
        }

        // PUT: api/BikeStores/5
        [HttpPut("{id}")]
        public ActionResult<BikeStore> PutBikeStore(BikeStore bikeStore)
        {
            return _storesRepo.Update(bikeStore);
        }

        // POST: api/BikeStores
        [HttpPost]
        public ActionResult<BikeStore> PostBikeStore(BikeStore bikeStore)
        {
            return _storesRepo.Insert(bikeStore);
        }

        // DELETE: api/BikeStores/5
        [HttpDelete("{id}")]
        public ActionResult<BikeStore> DeleteBikeStore(int id)
        {
            return _storesRepo.Delete(id);
        }

        bool BikeStoreExists(int id) => _storesRepo.Get().Any(s => s.Id == id);
    }
}
