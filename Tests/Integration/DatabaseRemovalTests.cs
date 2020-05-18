using Langs.Data.Objects;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseRemovalTests : IntegrationTest
    {
        [Test]
        public void RemovingTranslation_FromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();
            var trans = word.Translations.First();
            var translationsBefore = word.Translations.Count();

            word.RemoveTranslation(trans);
            WordsService.Update(word);

            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(translationsBefore - 1, newWord.Translations.Count(), "New reference has less translations");
            Assert.AreEqual(translationsBefore - 1, word.Translations.Count(), "Old reference should also be updated");
        }

        [Test]
        public void RemovingDefinition_FromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();
            var before = DefinitionsService.GetAll().Count();

            word.Definition = null;
            WordsService.Update(word);
            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(null, newWord.Definition, "Definition is empty");
            Assert.AreEqual(before - 1, DefinitionsService.GetAll().Count(), "Definition should be removed from it's table as well");
        }

        [Test]
        public void RemovingExplanation_FromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();
            var before = word.Explanations.Count();

            word.Explanations = null;
            WordsService.Update(word);
            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(null, newWord.Explanations, "Explanations is empty");
        }

        /*
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
        */
    }
}
