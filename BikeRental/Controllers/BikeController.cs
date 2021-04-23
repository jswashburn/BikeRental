using BikeRental.Models;
using BikeRental.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BikeRental.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BikeController : ControllerBase
    {
        IRepository<Bike> BikesRepo { get; set; }

        public BikeController(IRepository<Bike> bikes)
        {
            BikesRepo = bikes;
        }

        // GET: api/bike
        [HttpGet]
        public IEnumerable<Bike> Get()
        {
            return BikesRepo.Get();
        }
    }
}
