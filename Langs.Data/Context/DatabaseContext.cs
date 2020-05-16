using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Langs.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Word> Words { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Book - Word : Many To Many
            builder.Entity<BookWord>().HasKey(e => new { e.BookId, e.WordId });
            builder.Entity<BookWord>()
                .HasOne<Word>(e => e.Word)
                .WithMany(e => e.BookWordCollection)
                .HasForeignKey(e => e.WordId);

            builder.Entity<BookWord>()
                .HasOne<Book>(e => e.Book)
                .WithMany(e => e.BookWordCollection)
                .HasForeignKey(e => e.BookId);
        }
    }
}
