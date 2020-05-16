using Langs.Data.Objects;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseRemovalTests : IntegrationTest
    {
        #region // Translation - Word - Definition
        [Test]
        public void RemovingTranslationFromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();
            var trans = word.Translations.First();

            word.Translations.Remove(trans);
            WordsService.Update(word);

            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(0, newWord.Translations.Count, "New reference has less translations");
            Assert.AreEqual(0, word.Translations.Count, "Old reference should also be updated");
        }

        [Test]
        public void RemovingDefinition_WillUpdateDB_AndRemoveWordsFromTranslationsAutomatically()
        {
            var word = WordsService.GetWordsWithData().First();
            Assert.AreEqual(1, word.Translations.Count, "Translations count incorrect in the beginning");

            DefinitionsService.Remove(word.Translations.First().Definition);
            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(0, newWord.Translations.Count, "Translations should be automatically deleted when removing definition");
            Assert.AreEqual(0, word.Translations.Count, "Old reference should also be updated");
        }

        [Test]
        public void RemovingWord_WillUpdateDB_AndRemoveDefinitionsFromTranslationsAutomatically()
        {
            var def = DefinitionsService.GetDefinitionsWithData().First();
            Assert.AreEqual(2, def.Translations.Count, "Translations count incorrect in the beginning");

            WordsService.Remove(def.Translations.First().Word);
            var newDef = DefinitionsService.GetDefinitionsWithData().First();

            Assert.AreEqual(1, newDef.Translations.Count, "Translations should be automatically deleted when removing word");
            Assert.AreEqual(1, def.Translations.Count, "Old reference should also be updated");
        }
        #endregion

        #region // Book - Word
        [Test]
        public void RemovingBook_WillUpdateDB_AndRemoveBookFromWordsAutomatically()
        {
            var book = BookService.GetBooksWithData().First();

            BookService.Remove(book);
            var newBook = BookService.GetBooksWithData().FirstOrDefault();
            var bCount = WordsService.GetWordsWithData().First().Books.Count();

            Assert.IsNull(newBook);
            Assert.AreEqual(0, bCount);
        }

        [Test]
        public void RemovingWord_WillUpdateDB_AndRemoveWordsFromBookAutomatically()
        {
            var word = WordsService.GetWordsWithData().First();

            WordsService.Remove(word);
            var newWord = WordsService.GetWordsWithData().First();
            var wCount = BookService.GetBooksWithData().First().Words.Count();

            Assert.AreNotEqual(word, newWord);
            Assert.AreEqual(5, wCount);
        }
        #endregion
    }
}
