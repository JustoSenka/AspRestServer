using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Langs.Data.Context
{
    public interface IDatabaseContext
    {
        DbContext Context { get; }

        DbSet<Account> Accounts { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<MasterWord> MasterWords { get; set; }
        DbSet<Word> Words { get; set; }

        IDatabaseContext Clone();
        void RefreshDatabaseContext();
        void SaveChanges();
    }
}