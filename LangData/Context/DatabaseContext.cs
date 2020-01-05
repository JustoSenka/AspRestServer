using LangData.Objects;
using Microsoft.EntityFrameworkCore;

namespace LangData.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Word> Words { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Translation> Translations { get; set; }
    }
}
