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

        // GET api/Bikes/reservation/1
        [HttpGet("reservation/{id}")]
        public ActionResult<Reservation> GetReservationByBike(int id)
        {
            Reservation reservation = _reservationsRepo.Get().FirstOrDefault(r => r.BikeId == id);
            if (reservation == null)
                return NotFound();
            return reservation;
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
