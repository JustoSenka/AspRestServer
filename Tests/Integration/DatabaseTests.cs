using LangData.Objects;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseTests : IntegrationTest
    {
        [Test]
        public void Service_WithDefaultWebHost_IsResolved()
        {
            Assert.IsNotNull(BookService);
        }

        [Test]
        public void CanAdd_Word_ToDatabase()
        {
            WordsService.Add(new Word() { Text = "Something", Language = LanguagesService.GetAll().First() });
            Assert.AreEqual(7, WordsService.GetWordsWithData().Count());
        }

        [Test]
        public void CanAdd_WordWithLanguage_ToDatabase()
        {
            WordsService.Add(new Word() { Text = "Something", Language = new Language() { Name = "Another" } });

            Assert.AreEqual(7, WordsService.GetWordsWithData().Count());
            Assert.AreEqual(4, LanguagesService.GetAll().Count());
        }

        [Test]
        public void Adding_WordWithSameLanguage_WillNotIncrementLanguages()
        {
            var lang = new Language() { Name = "Another" };
            WordsService.Add(new Word() { Text = "Something", Language = lang });
            WordsService.Add(new Word() { Text = "anything", Language = lang });

            Assert.AreEqual(8, WordsService.GetWordsWithData().Count());
            Assert.AreEqual(4, LanguagesService.GetAll().Count());
        }

        [Test]
        public void RemovingTranslationFromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();
            var trans = word.Translations.First();

            word.Translations.Remove(trans);
            WordsService.Update(word);

            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(0, newWord.Translations.Count);
        }

        [Test]
        public void RemovingWord_WillUpdateDB_AndRemoveWordsFromBookAutomatically()
        {
            var word = WordsService.GetWordsWithData().First();

            WordsService.Remove(word);
            var newWord = WordsService.GetWordsWithData().First();
            var wCount = BookService.GetBooksWithData().First().Words.Count;

            Assert.AreNotEqual(word, newWord);
            Assert.AreEqual(5, wCount);
        }

        [Test]
        public void RemovingDefinition_WillUpdateDB_AndRemoveWordsFromTranslationsAutomatically()
        {
            var word = WordsService.GetWordsWithData().First();

            DefinitionsService.Remove(word.Translations[0].Definition);
            var newWord = WordsService.GetWordsWithData().First();
            var trans = newWord.Translations[0];

            Assert.IsNull(trans.Definition);
        }
    }
}
