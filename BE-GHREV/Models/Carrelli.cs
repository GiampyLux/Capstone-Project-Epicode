namespace BE_GHREV.Models
{
    public class Carrelli
    {
        public int ID { get; set; }
        public int ID_utenti { get; set; }
        public int ID_prodotti { get; set; }

        // Relazioni con Utente e Prodotto
        public Utenti Utenti { get; set; }
        public Prodotti Prodotti { get; set; }
    }
}
