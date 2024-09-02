using System.ComponentModel.DataAnnotations.Schema;

namespace BE_GHREV.Models
{
    public class Carrelli
    {
        public int ID { get; set; }

        [ForeignKey("Utenti")]
        public int ID_Utenti { get; set; }

        [ForeignKey("Prodotti")]
        public int ID_Prodotti { get; set; }

        // Relazioni con Utente e Prodotto
        public required Utenti Utenti { get; set; }
        public required Prodotti Prodotti { get; set; }
    }
}
