using Langs.Data.Context;
using Langs.Data.Objects;
using Langs.Utilities;
using Microsoft.EntityFrameworkCore;
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
        public WordsService(IBooksService BooksService, IMasterWordsService MasterWordsService, IDatabaseContext context) : base(context)
        {
            this.BooksService = BooksService;
            this.MasterWordsService = MasterWordsService;
        }

        public override Word Get(int id) => GetWordsWithData().WithID(id);

        public override IEnumerable<Word> GetAll() => m_Context.Words.Include(w => w.Language);

        public IEnumerable<Word> GetWordsWithData() => m_Context.Words
            .Include(p => p.Language)
            .Include(p => p.Definition)
            .Include(p => p.MasterWord)
                .ThenInclude(t => t.Words)
                    .ThenInclude(t => t.Language)
            .Include(word => word.MasterWord)
                .ThenInclude(t => t._BookWordCollection);
                    

        public override void Remove(Word obj)
        {
            obj = Get(obj.ID);

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
            var masters = objs.Select(w => w.MasterWord ?? MasterWordsService.Get(w.MasterWordID)).ToHashSet();

            base.Remove(objs);
            MasterWordsService.Remove(masters.Where(m => m.Words.Count == 0));
        }

        public void AddTranslation(Word word, Word translation)
        {
            // Words might be loaded without master word
            var masterA = MasterWordsService.Get(word.MasterWordID);
            var masterB = MasterWordsService.Get(translation.MasterWordID);

            if (!IsMergePossibleInternal(masterA, masterB))
                throw new InvalidOperationException($"Cannot add translation to word '{translation.Text}' " +
                    $"because it is already translated to '{word.Language?.Name}'.");

            // Transfer to one which has more translations, so less operations are made
            var shouldUseOriginalMasterWord = masterA.Words.Count() >= masterB.Words.Count();

            var mainMasterWord = shouldUseOriginalMasterWord ? masterA : masterB;
            var masterWordToDestroy = shouldUseOriginalMasterWord ? masterB : masterA;

            // Replace all references in books from destroyed to new one 
            foreach (var id in masterWordToDestroy.BookIDs)
            {
                var book = BooksService.Get(id);
                book.ReplaceWord(masterWordToDestroy, mainMasterWord);
            }

            // Transfer all words from one destroyed master word to new one.
            foreach (var w in masterWordToDestroy.Words.ToArray())
                Transfer(masterWordToDestroy, mainMasterWord, w);

            // Update database. Saving only master words, even though some modifications have been made to the book with ReplaceWord. 
            // Although saves everything correctly, and after save all refs are up to date
            using (MasterWordsService.BatchRequests())
            {
                MasterWordsService.Update(mainMasterWord);
                MasterWordsService.Remove(masterWordToDestroy);
            }
        }

        public bool IsMergePossible(MasterWord masterA, MasterWord masterB)
        {
            masterA = MasterWordsService.GetAllWithWords().WithID(masterA.ID);
            masterB = MasterWordsService.GetAllWithWords().WithID(masterB.ID);
            return IsMergePossibleInternal(masterA, masterB);
        }

        private static bool IsMergePossibleInternal(MasterWord masterA, MasterWord masterB)
        {
            // Check if merge is possible. If merging MasterWords will procude multiple words of same language under same master, it's invalid
            var set = masterA.Words.ToHashSet(new LanguageEqualityComparer());
            set.AddRange(masterB.Words);

            return !(set.Count() < masterA.Words.Count() + masterB.Words.Count());
        }

        public void RemoveTranslation(Word word, Word translation)
        {
            // Words might be loaded without master word
            word = Get(word.ID);
            translation = Get(translation.ID);

            var oldMaster = word.MasterWord;
            var newMaster = new MasterWord();

            // Move translation to new master word
            Transfer(oldMaster, newMaster, translation);

            // If books was specifically for language 'a' and translation is in language 'a', prefer to link newly created master word
            // which has translation in question, and not default one.
            foreach (var id in oldMaster.BookIDs.ToArray())
            {
                var book = BooksService.Get(id);
                if (book.Language.ID == translation.Language.ID)
                    book.ReplaceWord(oldMaster, newMaster);
            }

            using (MasterWordsService.BatchRequests())
            {
                MasterWordsService.Update(oldMaster);
                MasterWordsService.Add(newMaster);
            }
        }

        private void Transfer(MasterWord From, MasterWord to, Word word)
        {
            From.Words.Remove(word);
            to.Words.Add(word);
        }
    }
}
