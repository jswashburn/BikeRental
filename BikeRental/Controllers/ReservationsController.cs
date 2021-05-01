using BikeRentalApi.Models;
using BikeRentalApi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        // TODO: Update to return notfounds
        IRepository<Reservation> _reservationsRepo;

        public ReservationsController(IRepository<Reservation> reservations)
        {
            _reservationsRepo = reservations;
        }

        // GET: api/Reservations
        [HttpGet]
        public IEnumerable<Reservation> GetReservations()
        {
            return _reservationsRepo.Get();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            Reservation reservation = _reservationsRepo.Get(id);
            if (reservation == null)
                return NotFound();
            return reservation;
        }

        // GET api/Reservations/bike/1
        [HttpGet("bike/{id}")]
        public ActionResult<Reservation> GetReservationByBike(int id)
        {
            Reservation reservation = _reservationsRepo.Get().FirstOrDefault(r => r.BikeId == id);
            if (reservation == null)
                return NotFound();
            return reservation;
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public ActionResult<Reservation> PutReservation(Reservation reservation)
        {
            return _reservationsRepo.Update(reservation);
        }

        // POST: api/Reservations
        [HttpPost]
        public ActionResult<Reservation> PostReservation(Reservation reservation)
        {
            return _reservationsRepo.Insert(reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public ActionResult<Reservation> DeleteReservation(int id)
        {
            return _reservationsRepo.Delete(id);
        }

        bool ReservationExists(int id) => _reservationsRepo.Get().Any(r => r.Id == id);
    }
}
