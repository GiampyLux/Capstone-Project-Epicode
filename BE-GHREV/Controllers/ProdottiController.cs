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
    public class ProdottiController : ControllerBase
    {
        private readonly GhrevDB _context;

        public ProdottiController(GhrevDB context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prodotti>>> GetProdotti()
        {
            return await _context.Prodotti.Include(p => p.Categorie).ToListAsync();
        }

 
        [HttpGet("{id}")]
        public async Task<ActionResult<Prodotti>> GetProdotti(int id)
        {
            var prodotti = await _context.Prodotti.Include(p => p.Categorie).FirstOrDefaultAsync(p => p.ID == id);

            if (prodotti == null)
            {
                return NotFound();
            }

            return prodotti;
        }

     
        [HttpPost]
        public async Task<ActionResult<Prodotti>> PostProdotti(Prodotti prodotti)
        {
            _context.Prodotti.Add(prodotti);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdotti), new { id = prodotti.ID }, prodotti);
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdotti(int id, Prodotti prodotti)
        {
            if (id != prodotti.ID)
            {
                return BadRequest();
            }

            _context.Entry(prodotti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdottiExists(id))
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
        public async Task<IActionResult> DeleteProdotti(int id)
        {
            var prodotti = await _context.Prodotti.FindAsync(id);
            if (prodotti == null)
            {
                return NotFound();
            }

            _context.Prodotti.Remove(prodotti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdottiExists(int id)
        {
            return _context.Prodotti.Any(e => e.ID == id);
        }
    }
}
