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
            return Ok(bikeStores.ToList());
        }

        // GET: api/BikeStores/5
        [HttpGet("{id}")]
        public ActionResult<BikeStore> GetBikeStore(int id)
        {
            BikeStore bikeStore = _storesRepo.Get(id);
            if (bikeStore == null)
                return NotFound();
            return Ok(bikeStore);
        }

        // PUT: api/BikeStores
        [HttpPut]
        public ActionResult<BikeStore> PutBikeStore(BikeStore bikeStore)
        {
            BikeStore updated = _storesRepo.Update(bikeStore);
            return Ok(updated);
        }

        // POST: api/BikeStores
        [HttpPost]
        public ActionResult<BikeStore> PostBikeStore(BikeStore bikeStore)
        {
            if (BikeStoreExists(bikeStore.Id))
                return BadRequest($"BikeStore already exists! (BikeStore ID {bikeStore.Id})");

            BikeStore created = _storesRepo.Insert(bikeStore);
            return CreatedAtAction(nameof(PostBikeStore), created);
        }

        // DELETE: api/BikeStores/5
        [HttpDelete("{id}")]
        public ActionResult<BikeStore> DeleteBikeStore(int id)
        {
            BikeStore deleted = _storesRepo.Delete(id);
            return Ok(deleted);
        }

        bool BikeStoreExists(int id) => _storesRepo.Get().Any(s => s.Id == id);
    }
}
