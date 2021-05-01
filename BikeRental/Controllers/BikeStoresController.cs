using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRentalApi.Controllers
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
        public ActionResult<List<BikeStore>> GetBikeStores()
        {
            IEnumerable<BikeStore> bikeStores = _storesRepo.Get();
            if (bikeStores == null)
                return NotFound();
            return bikeStores.ToList();
        }

        // GET: api/BikeStores/5
        [HttpGet("{id}")]
        public ActionResult<BikeStore> GetBikeStore(int id)
        {
            BikeStore bikeStore = _storesRepo.Get(id);
            if (bikeStore == null)
                return NotFound();
            return bikeStore;
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
