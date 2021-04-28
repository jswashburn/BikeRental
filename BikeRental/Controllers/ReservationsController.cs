using BikeRental.Models;
using BikeRental.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeRental.Controllers
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
        public IEnumerable<Reservation> GetReservations()
        {
            return _reservationsRepo.Get();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            return _reservationsRepo.Get(id);
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
