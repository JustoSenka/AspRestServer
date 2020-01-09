using LangData.Context;
using LangData.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LangServices
{
    public class DefinitionsService : BaseService<Definition>, IDefinitionsService
    {
        protected override DbSet<Definition> EntitiesProxy => m_Context.Definitions;
        public DefinitionsService(DatabaseContext context) : base(context) { }

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
    }
}
