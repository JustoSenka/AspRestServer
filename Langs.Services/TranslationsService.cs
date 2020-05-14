using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public class TranslationsService : BaseService<Translation>, ITranslationsService
    {
        protected override DbSet<Translation> EntitiesProxy => m_Context.Translations;
        public TranslationsService(DatabaseContext context) : base(context) { }

        public override Translation Get(int id) => m_Context.Translations
            .Include(t => t.Definition)
                .ThenInclude(d => d.Language)
            .Include(t => t.Word)
                .ThenInclude(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

        public IEnumerable<Translation> GetTranslationsWithText() => m_Context.Translations.Include(t => t.Word).Include(t => t.Definition);

        public IEnumerable<Translation> GetTranslationsWithLanguages()
        {
            return m_Context.Translations
                .Include(t => t.Word)
                    .ThenInclude(d => d.Language)
                .Include(t => t.Definition)
                    .ThenInclude(d => d.Language);
        }
    }
}
