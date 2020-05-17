using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public class WordsService : BaseService<Word>, IWordsService
    {
        private readonly IBooksService BooksService;

        protected override DbSet<Word> EntitiesProxy => m_Context.Words;
        public WordsService(IBooksService BooksService, DatabaseContext context) : base(context)
        {
            this.BooksService = BooksService;
        }

        public override Word Get(int id) => m_Context.Words
            .Include(word => word.Language)
            .Include(word => word.Definition)
            .Include(word => word.MasterWord)
                .ThenInclude(t => t.Words)
                    .ThenInclude(t => t.Language)
            .SingleOrDefault(e => e.ID == id);

        // TODO: Language is not needed in all situations in Words/Show
        public override IEnumerable<Word> GetAll() => m_Context.Words.Include(w => w.Language);

        public IEnumerable<Word> GetWordsWithData() => m_Context.Words
            .Include(p => p.Language)
            .Include(p => p.Definition)
            .Include(p => p.MasterWord)
                .ThenInclude(t => t.Words)
                    .ThenInclude(t => t.Language);

        public override void Remove(Word obj)
        {
            // TranslationsService.StartBatchingRequests();

            // TranslationsService.Remove(obj.Translations);
            base.Remove(obj);

            // TranslationsService.EndBatchingRequestsAndSave();
        }
        /*
        public override void Update(Word obj)
        {
            TranslationsService.StartBatchingRequests();

            var oldWord = Get(obj.ID);
            var differentceInTranslations = oldWord.Translations.Concat(obj.Translations).Except(obj.Translations);
            TranslationsService.Remove(obj.Translations);
            base.Remove(obj);

            TranslationsService.EndBatchingRequestsAndSave();
        }*/
    }
}
