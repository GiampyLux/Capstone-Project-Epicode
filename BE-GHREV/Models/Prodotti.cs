namespace BE_GHREV.Models
{
    public class Prodotti
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public float Prezzo { get; set; }
        public int ID_categorie { get; set; }
        public string Immagini { get; set; }

        // Relazione con Categoria
        public Categorie Categorie { get; set; }
    }
}
