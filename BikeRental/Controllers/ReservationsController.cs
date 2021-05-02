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
        IRepository<Reservation> _reservationsRepo;

        public ReservationsController(IRepository<Reservation> reservations)
        {
            _reservationsRepo = reservations;
        }

        // GET: api/Reservations
        [HttpGet]
        public ActionResult<List<Reservation>> GetReservations()
        {
            IEnumerable<Reservation> reservation = _reservationsRepo.Get();
            if (reservation == null)
                return NotFound();
            return Ok(reservation.ToList());
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            Reservation reservation = _reservationsRepo.Get(id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        // PUT: api/Reservations
        [HttpPut]
        public ActionResult<Reservation> PutReservation(Reservation reservation)
        {
            Reservation updated = _reservationsRepo.Update(reservation);
            return Ok(updated);
        }

        // POST: api/Reservations
        [HttpPost]
        public ActionResult<Reservation> PostReservation(Reservation reservation)
        {
            if (ReservationExists(reservation.Id))
                return BadRequest($"Reservation Already exists! ID {reservation.Id}");

            Reservation created = _reservationsRepo.Insert(reservation);
            return CreatedAtAction(nameof(PostReservation), created);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public ActionResult<Reservation> DeleteReservation(int id)
        {
            Reservation deleted = _reservationsRepo.Delete(id);
            return Ok(deleted);
        }

        bool ReservationExists(int id) => _reservationsRepo.Get().Any(r => r.Id == id);
    }
}
