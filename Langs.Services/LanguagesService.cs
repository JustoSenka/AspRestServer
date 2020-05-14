using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Langs.Services
{
    public class LanguagesService : BaseService<Language>, ILanguagesService
    {
        protected override DbSet<Language> EntitiesProxy => m_Context.Languages;
        public LanguagesService(DatabaseContext context) : base(context) { }
    }
}
