using Microsoft.EntityFrameworkCore;
using BE_GHREV.Models;

namespace BE_GHREV.Data
{
    public class GhrevDB : DbContext
    {
        public GhrevDB(DbContextOptions<GhrevDB> options)
            : base(options)
        {
        }

        
        public DbSet<Utenti> Utenti { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Prodotti> Prodotti { get; set; }
        public DbSet<Carrelli> Carrelli { get; set; }
    }
}
