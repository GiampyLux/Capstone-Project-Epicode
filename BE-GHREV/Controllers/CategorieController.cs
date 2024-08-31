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
    public class CategorieController : ControllerBase
    {
        private readonly GhrevDB _context;

        public CategorieController(GhrevDB context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategorie()
        {
            return await _context.Categorie.ToListAsync();
        }

        // GET: api/Categorie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var categorie = await _context.Categorie.FindAsync(id);

            if (categorie == null)
            {
                return NotFound();
            }

            return categorie;
        }

        
        [HttpPost]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
        {
            _context.Categorie.Add(categorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorie), new { id = categorie.ID }, categorie);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorie(int id, Categorie categorie)
        {
            if (id != categorie.ID)
            {
                return BadRequest();
            }

            _context.Entry(categorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
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
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await _context.Categorie.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            _context.Categorie.Remove(categorie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieExists(int id)
        {
            return _context.Categorie.Any(e => e.ID == id);
        }
    }
}
