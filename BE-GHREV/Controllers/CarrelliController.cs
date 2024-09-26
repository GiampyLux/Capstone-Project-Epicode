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

        // Metodo per ottenere tutti i carrelli
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrelli>>> GetCarrelli()
        {
            return await _context.Carrelli.Include(c => c.Utenti).Include(c => c.Prodotti).ToListAsync();
        }

        // Metodo per ottenere un carrello specifico tramite ID
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

        // Nuovo metodo per ottenere il carrello di un utente
        [HttpGet("utente/{idUtente}")]
        public async Task<ActionResult<IEnumerable<Carrelli>>> GetCarrelloPerUtente(int idUtente)
        {
            var carrello = await _context.Carrelli
                .Include(c => c.Prodotti)
                .Where(c => c.ID_Utenti == idUtente)
                .ToListAsync();

            if (carrello == null || !carrello.Any())
            {
                return NotFound();
            }

            return carrello;
        }

        // Nuovo metodo per ottenere il totale del carrello
        [HttpGet("totale/{idUtente}")]
        public async Task<ActionResult<double>> GetTotaleCarrello(int idUtente)
        {
            var carrello = await _context.Carrelli
                .Include(c => c.Prodotti)
                .Where(c => c.ID_Utenti == idUtente)
                .ToListAsync();

            if (carrello == null || !carrello.Any())
            {
                return NotFound();
            }

            // Calcola il totale moltiplicando la quantità per il prezzo del prodotto
            double totale = carrello.Sum(c => c.Prodotti.Prezzo * c.Quantita);
            return totale;
        }
        //inizio
        [HttpPost]
        public async Task<ActionResult<Carrelli>> PostCarrelli(Carrelli carrelli)
        {
            // Verifica se l'utente esiste
            var utente = await _context.Utenti.FindAsync(carrelli.ID_Utenti);
            if (utente == null)
            {
                return NotFound($"Utente con ID {carrelli.ID_Utenti} non trovato.");
            }

            // Usa Attach per evitare che EF tracci un'istanza duplicata di 'Utenti'
            _context.Attach(utente); // Collega l'entità 'Utenti' senza tracciarla nuovamente

            // Verifica se il prodotto esiste
            var prodotto = await _context.Prodotti.FindAsync(carrelli.ID_Prodotti);
            if (prodotto == null)
            {
                return NotFound($"Prodotto con ID {carrelli.ID_Prodotti} non trovato.");
            }

            // Usa Attach per evitare che EF tracci un'istanza duplicata di 'Prodotti'
            _context.Attach(prodotto);

            // Controlla se lo stesso prodotto è già nel carrello dell'utente
            var existingCarrello = await _context.Carrelli
                .FirstOrDefaultAsync(c => c.ID_Utenti == carrelli.ID_Utenti && c.ID_Prodotti == carrelli.ID_Prodotti);

            if (existingCarrello != null)
            {
                // Se esiste, incrementa la quantità
                existingCarrello.Quantita += carrelli.Quantita;

                _context.Entry(existingCarrello).State = EntityState.Modified;
            }
            else
            {
                // Altrimenti aggiungi un nuovo prodotto al carrello
                carrelli.Prodotti = prodotto; // Associa il prodotto al carrello
                carrelli.Utenti = utente; // Associa l'utente al carrello

                _context.Carrelli.Add(carrelli);
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarrelli), new { id = carrelli.ID }, carrelli);
        }
        //fine http post

        // Metodo per modificare un carrello esistente
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

        // Metodo per eliminare un carrello
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

        // Nuovo metodo per eliminare un singolo prodotto dal carrello
        [HttpDelete("utente/{idUtente}/prodotto/{idProdotto}")]
        public async Task<IActionResult> DeleteProdottoDalCarrello(int idUtente, int idProdotto)
        {
            var carrello = await _context.Carrelli
                .FirstOrDefaultAsync(c => c.ID_Utenti == idUtente && c.ID_Prodotti == idProdotto);

            if (carrello == null)
            {
                return NotFound();
            }

            _context.Carrelli.Remove(carrello);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Metodo privato per verificare se il carrello esiste
        private bool CarrelliExists(int id)
        {
            return _context.Carrelli.Any(e => e.ID == id);
        }
    }
}
