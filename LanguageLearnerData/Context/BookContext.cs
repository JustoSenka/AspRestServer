using LanguageLearnerData.Objects;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearnerData.Context
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Word> Word { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Definition> Definition { get; set; }
        public DbSet<Translation> Translation { get; set; }
    }
}
