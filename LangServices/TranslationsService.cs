using LangData.Context;
using LangData.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LangServices
{
    public class TranslationsService : BaseService<Translation>, ITranslationsService
    {
        protected override DbSet<Translation> EntitiesProxy => m_Context.Translations;
        public TranslationsService(DatabaseContext context) : base(context) { }

        public Translation GetTranslation(int id) => m_Context.Translations
            .Include(t => t.Definition)
                .ThenInclude(d => d.Language)
            .Include(t => t.Word)
                .ThenInclude(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

        public IEnumerable<Translation> GetTranslations() => m_Context.Translations.Include(t => t.Word).Include(t => t.Definition);
        
        public IEnumerable<Translation> GetTranslationsWithData()
        {
            return m_Context.Translations
                .Include(t => t.Word)
                    .ThenInclude(d => d.Language)
                .Include(t => t.Definition)
                    .ThenInclude(d => d.Language);
        }
    }
}
