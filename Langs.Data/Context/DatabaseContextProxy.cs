using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Langs.Data.Context
{
    public class DatabaseContextProxy : IDatabaseContext
    {
        private IDatabaseContext m_RealContext;

        public DatabaseContextProxy(DatabaseContext realContext)
        {
            m_RealContext = realContext;
        }

        public DbContext Context => m_RealContext.Context;

        public DbSet<Book> Books
        {
            get => m_RealContext.Books;
            set => m_RealContext.Books = value;
        }

        public DbSet<Word> Words
        {
            get => m_RealContext.Words;
            set => m_RealContext.Words = value;
        }

        public DbSet<MasterWord> MasterWords
        {
            get => m_RealContext.MasterWords;
            set => m_RealContext.MasterWords = value;
        }

        public DbSet<Language> Languages
        {
            get => m_RealContext.Languages;
            set => m_RealContext.Languages = value;
        }

        public DbSet<Account> Accounts
        {
            get => m_RealContext.Accounts;
            set => m_RealContext.Accounts = value;
        }

        public void RefreshDatabaseContext() => m_RealContext = Clone();
        public IDatabaseContext Clone() => m_RealContext.Clone();

        public void SaveChanges() => m_RealContext.SaveChanges();
    }
}
