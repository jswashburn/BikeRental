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
        IRepository<Reservation> _reservationsRepo;

        public BikesController(IRepository<Bike> bikes, IRepository<Reservation> reservations)
        {
            _bikesRepo = bikes;
            _reservationsRepo = reservations;
        }

        // GET: api/Bikes
        [HttpGet]
        public ActionResult<List<Bike>> GetBikes()
        {
            IEnumerable<Bike> bikes = _bikesRepo.Get();
            if (bikes == null)
                return NotFound();
            return Ok(bikes.ToList());
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public ActionResult<Bike> GetBike(int id)
        {
            Bike bike = _bikesRepo.Get(id);
            if (bike == null)
                return NotFound();
            return Ok(bike);
        }

        // GET api/Bikes/reservation/1
        [HttpGet("reservation/{id}")]
        public ActionResult<Reservation> GetReservationByBike(int id)
        {
            Reservation reservation = _reservationsRepo.Get().FirstOrDefault(r => r.BikeId == id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        // PUT: api/Bikes/
        [HttpPut]
        public ActionResult<Bike> PutBike(Bike bike)
        {
            Bike updated = _bikesRepo.Update(bike);
            return Ok(updated);
        }

        // POST: api/Bikes
        [HttpPost]
        public ActionResult<Bike> PostBike(Bike bike)
        {
            if (BikeExists(bike.Id))
                return BadRequest($"Bike already exists! (Bike ID {bike.Id})");

            Bike created = _bikesRepo.Insert(bike);
            return CreatedAtAction(nameof(PostBike), created);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public ActionResult<Bike> DeleteBike(int id)
        {
            Bike deleted = _bikesRepo.Delete(id);
            return Ok(deleted);
        }

        bool BikeExists(int id) => _bikesRepo.Get().Any(b => b.Id == id);
    }
}
