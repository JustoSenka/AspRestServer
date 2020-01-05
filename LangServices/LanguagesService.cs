using LangData.Context;
using LangData.Objects;
using Microsoft.EntityFrameworkCore;

namespace LangServices
{
    public class LanguagesService : BaseService<Language>, ILanguagesService
    {
        protected override DbSet<Language> EntitiesProxy => m_Context.Languages;
        public LanguagesService(DatabaseContext context) : base(context) { }
    }
}
