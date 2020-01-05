using LangData.Context;
using LangData.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LangServices
{
    public class WordsService : BaseService<Word>, IWordsService
    {
        protected override DbSet<Word> EntitiesProxy => m_Context.Words;
        public WordsService(DatabaseContext context) : base(context) { }

        public override Word Get(int id) => m_Context.Words
            .Include(word => word.Language)
            .Include(word => word.Translations)
                .ThenInclude(t => t.Definition)
                    .ThenInclude(d => d.Language)
            .Include(word => word.Translations)
                .ThenInclude(t => t.Word)
                    .ThenInclude(d => d.Language)
            .SingleOrDefault(e => e.ID == id);

       // TODO: Language is not needed in all situations in Words/Show
        public override IEnumerable<Word> GetAll() => m_Context.Words.Include(w => w.Language);

        public IEnumerable<Word> GetWordsWithData() => m_Context.Words
            .Include(p => p.Language)
            .Include(p => p.Translations)
                .ThenInclude(t => t.Word)
                    .ThenInclude(d => d.Language)
            .Include(p => p.Translations)
                .ThenInclude(t => t.Definition)
                    .ThenInclude(d => d.Language);
    }
}
