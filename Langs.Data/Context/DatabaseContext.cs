using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

            // Remove cascading on all foreign keys, since in most cases we don't want it.
            // Re-Add later manually for join tables
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            // Book - Word : Many To Many
            builder.Entity<BookWord>().HasKey(e => new { e.BookId, e.MasterWordId });
            builder.Entity<BookWord>()
                .HasOne(e => e.MasterWord)
                .WithMany(e => e._BookWordCollection)
                .HasForeignKey(e => e.MasterWordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BookWord>()
                .HasOne(e => e.Book)
                .WithMany(e => e._BookWordCollection)
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
