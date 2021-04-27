using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models;
using BikeRental.Models.Repositories;

namespace BikeRental.Controllers
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
        public IEnumerable<Bike> GetBikes()
        {
            return _bikesRepo.Get();
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public Bike GetBike(int id)
        {
            return _bikesRepo.Get(id);
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
