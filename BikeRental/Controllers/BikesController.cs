using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        IRepository<Bike> _bikesRepo;

        public BikesController(IRepository<Bike> bikes)
        {
            _bikesRepo = bikes;
        }

        // GET: api/Bikes
        [HttpGet]
        public ActionResult<List<Bike>> GetBikes()
        {
            IEnumerable<Bike> bikes = _bikesRepo.Get();
            if (bikes == null)
                return NotFound();
            return bikes.ToList();
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public ActionResult<Bike> GetBike(int id)
        {
            Bike bike = _bikesRepo.Get(id);
            if (bike == null)
                return NotFound();
            return bike;
        }

        // PUT: api/Bikes/5
        [HttpPut("{id}")]
        public ActionResult<Bike> PutBike(Bike bike)
        {
            return _bikesRepo.Update(bike);
        }

        // POST: api/Bikes
        [HttpPost]
        public ActionResult<Bike> PostBike(Bike bike)
        {
            return _bikesRepo.Insert(bike);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public ActionResult<Bike> DeleteBike(int id)
        {
            return _bikesRepo.Delete(id);
        }

        bool BikeExists(int id) => _bikesRepo.Get().Any(b => b.Id == id);
    }
}
