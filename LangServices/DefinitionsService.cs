using LangData.Context;
using LangData.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LangServices
{
    public class DefinitionsService : BaseService<Definition>, IDefinitionsService
    {
        private readonly ITranslationsService TranslationsService;

        protected override DbSet<Definition> EntitiesProxy => m_Context.Definitions;
        public DefinitionsService(ITranslationsService TranslationsService, DatabaseContext context) : base(context)
        {
            this.TranslationsService = TranslationsService;
        }

        public override Definition Get(int id) => m_Context.Definitions
            .Include(d => d.Language)
            .Include(d => d.Translations)
                .ThenInclude(t => t.Definition)
                    .ThenInclude(d => d.Language)
            .Include(d => d.Translations)
                .ThenInclude(t => t.Word)
                    .ThenInclude(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

        public override IEnumerable<Definition> GetAll() => m_Context.Definitions.Include(w => w.Language);

        public IEnumerable<Definition> GetDefinitionsWithData() => m_Context.Definitions
            .Include(p => p.Language)
                .Include(p => p.Translations)
                    .ThenInclude(t => t.Word)
                        .ThenInclude(d => d.Language)
                .Include(p => p.Translations)
                    .ThenInclude(t => t.Definition)
                        .ThenInclude(d => d.Language);

        public override void Remove(Definition obj)
        {
            TranslationsService.StartBatchingRequests();

            TranslationsService.Remove(obj.Translations);
            base.Remove(obj);

            TranslationsService.EndBatchingRequestsAndSave();
        }
    }
}
