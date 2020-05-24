using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public class WordsService : BaseService<Word>, IWordsService
    {
        private readonly IBooksService BooksService;
        private readonly IMasterWordsService MasterWordsService;

        protected override DbSet<Word> EntitiesProxy => m_Context.Words;
        public WordsService(IBooksService BooksService, IMasterWordsService MasterWordsService, DatabaseContext context) : base(context)
        {
            this.BooksService = BooksService;
            this.MasterWordsService = MasterWordsService;
        }

        public override Word Get(int id) => m_Context.Words
            .Include(word => word.Language)
            .Include(word => word.Definition)
            .Include(word => word.MasterWord)
                .ThenInclude(t => t.Words)
                    .ThenInclude(t => t.Language)
            .SingleOrDefault(e => e.ID == id);

        public override IEnumerable<Word> GetAll() => m_Context.Words.Include(w => w.Language);

        public IEnumerable<Word> GetWordsWithData() => m_Context.Words
            .Include(p => p.Language)
            .Include(p => p.Definition)
            .Include(p => p.MasterWord)
                .ThenInclude(t => t.Words)
                    .ThenInclude(t => t.Language);

        public override void Remove(Word obj)
        {
            var master = obj.MasterWord;
            var deleteMaster = master.Words.Count == 1;

            using (BatchRequests())
            {
                base.Remove(obj);
                
                if (deleteMaster)
                    MasterWordsService.Remove(master);
            }
        }

        public override void Remove(IEnumerable<Word> objs)
        {
            var masters = objs.Select(w => w.MasterWord).ToHashSet();

            base.Remove(objs);
            MasterWordsService.Remove(masters.Where(m => m.Words.Count == 0));
        }

        public void AddTranslation(Word word, Word translation)
        {
            // Transfer to one which has more translations, so less operations are made
            var shouldUseOriginalMasterWord = word.Translations.Count() >= translation.Translations.Count();

            var mainMasterWord = shouldUseOriginalMasterWord ? word.MasterWord : translation.MasterWord;
            var masterWordToDestroy = shouldUseOriginalMasterWord ? translation.MasterWord : word.MasterWord;

            // Replace all references in books from destroyed to new one 
            foreach (var book in masterWordToDestroy.Books)
                book.ReplaceWord(masterWordToDestroy, mainMasterWord);

            // Transfer all words from one destroyed master word to new one.
            foreach (var w in masterWordToDestroy.Words.ToArray())
                mainMasterWord.Transfer(w);

            // Update database. Saving only master words, even though some modifications have been made to the book with ReplaceWord. 
            // Although saves everything correctly, and after save all refs are up to date
            using (MasterWordsService.BatchRequests())
            {
                MasterWordsService.Update(mainMasterWord);
                MasterWordsService.Remove(masterWordToDestroy);
            }
        }

        public void RemoveTranslation(Word word, Word translation)
        {
            var oldMaster = word.MasterWord;
            var newMaster = new MasterWord();

            // Move translation to new master word
            newMaster.Transfer(translation);

            // If books was specifically for language 'a' and translation is in language 'a', prefer to link newly created master word
            // which has translation in question, and not default one.
            foreach (var book in oldMaster.Books.ToArray())
            {
                if (book.Language.ID == translation.Language.ID)
                    book.ReplaceWord(oldMaster, newMaster);
            }

            using (MasterWordsService.BatchRequests())
            {
                MasterWordsService.Update(oldMaster);
                MasterWordsService.Add(newMaster);
            }
        }
    }
}
