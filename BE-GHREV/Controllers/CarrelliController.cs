using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_GHREV.Data;
using BE_GHREV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_GHREV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrelliController : ControllerBase
    {
        private readonly GhrevDB _context;

        public CarrelliController(GhrevDB context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrelli>>> GetCarrelli()
        {
            return await _context.Carrelli.Include(c => c.Utenti).Include(c => c.Prodotti).ToListAsync();
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrelli>> GetCarrelli(int id)
        {
            var carrelli = await _context.Carrelli.Include(c => c.Utenti).Include(c => c.Prodotti).FirstOrDefaultAsync(c => c.ID == id);

            if (carrelli == null)
            {
                return NotFound();
            }

            return carrelli;
        }

      
        [HttpPost]
        public async Task<ActionResult<Carrelli>> PostCarrelli(Carrelli carrelli)
        {
            _context.Carrelli.Add(carrelli);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarrelli), new { id = carrelli.ID }, carrelli);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrelli(int id, Carrelli carrelli)
        {
            if (id != carrelli.ID)
            {
                return BadRequest();
            }

            _context.Entry(carrelli).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrelliExists(id))
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

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrelli(int id)
        {
            var carrelli = await _context.Carrelli.FindAsync(id);
            if (carrelli == null)
            {
                return NotFound();
            }

            _context.Carrelli.Remove(carrelli);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrelliExists(int id)
        {
            return _context.Carrelli.Any(e => e.ID == id);
        }
    }
}
