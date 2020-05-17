using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Langs.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Word> Words { get; set; }
        public DbSet<MasterWord> MasterWords { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Explanation> Explanations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Book - Word : Many To Many
            builder.Entity<BookWord>().HasKey(e => new { e.BookId, e.MasterWordId });
            builder.Entity<BookWord>()
                .HasOne(e => e.MasterWord)
                .WithMany(e => e._BookWordCollection)
                .HasForeignKey(e => e.MasterWordId);

            builder.Entity<BookWord>()
                .HasOne(e => e.Book)
                .WithMany(e => e._BookWordCollection)
                .HasForeignKey(e => e.BookId);

            // Word self referencing : Many To Many
            // builder.Entity<Translation>().HasNoKey(); // (e => new { e.WordFromId, e.WordId });
            /*
            builder.Entity<Translation>().HasKey(e => e.ID);
            builder.Entity<Translation>()
                .HasOne(e => e.Word)
                .WithMany(e => e._WordTranslationCollection)
                .HasForeignKey(e => e.WordId)
                .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}
