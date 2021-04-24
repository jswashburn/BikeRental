using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental.Data;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeStoresController : ControllerBase
    {
        private readonly BikeRentalDbContext _context;

        public BikeStoresController(BikeRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/BikeStores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeStore>>> GetBikeStores()
        {
            return await _context.BikeStores.ToListAsync();
        }

        // GET: api/BikeStores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeStore>> GetBikeStore(int id)
        {
            var bikeStore = await _context.BikeStores.FindAsync(id);

            if (bikeStore == null)
            {
                return NotFound();
            }

            return bikeStore;
        }

        // PUT: api/BikeStores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikeStore(int id, BikeStore bikeStore)
        {
            if (id != bikeStore.Id)
            {
                return BadRequest();
            }

            _context.Entry(bikeStore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeStoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BikeStores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BikeStore>> PostBikeStore(BikeStore bikeStore)
        {
            _context.BikeStores.Add(bikeStore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikeStore", new { id = bikeStore.Id }, bikeStore);
        }

        // DELETE: api/BikeStores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BikeStore>> DeleteBikeStore(int id)
        {
            var bikeStore = await _context.BikeStores.FindAsync(id);
            if (bikeStore == null)
            {
                return NotFound();
            }

            _context.BikeStores.Remove(bikeStore);
            await _context.SaveChangesAsync();

            return bikeStore;
        }

        private bool BikeStoreExists(int id)
        {
            return _context.BikeStores.Any(e => e.Id == id);
        }
    }
}
