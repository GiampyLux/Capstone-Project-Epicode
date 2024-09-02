using System.ComponentModel.DataAnnotations.Schema;

namespace BE_GHREV.Models
{
    public class Prodotti
    {
        public int ID { get; set; }
        public required string Nome { get; set; }
        public required string Descrizione { get; set; }
        public float Prezzo { get; set; }

        [ForeignKey("Categorie")]
        public int ID_Categorie { get; set; }

        public required string Immagini { get; set; }

        // Relazione con Categoria
        public required  Categorie Categorie { get; set; }
    }
}
