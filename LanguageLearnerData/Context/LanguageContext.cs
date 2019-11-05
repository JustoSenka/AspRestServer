using LanguageLearnerData.Objects;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearnerData.Context
{
    public class LanguageContext : DbContext
    {
        public LanguageContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }

    }
}
