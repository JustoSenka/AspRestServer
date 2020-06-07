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

            WordsService.RemoveTranslation(word, trans);

            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(translationsBefore - 1, newWord.Translations.Count(), "New reference has less translations");
            Assert.AreEqual(translationsBefore - 1, word.Translations.Count(), "Old reference should also be updated");
        }

        [Test]
        public void RemovingDefinition_FromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();

            word.Definition = null;
            WordsService.Update(word);
            var newWord = WordsService.GetWordsWithData().First();

            Assert.AreEqual(null, newWord.Definition, "Definition should be empty");
        }

        [Test]
        public void RemovingExplanation_FromWord_WillUpdateDB()
        {
            var word = WordsService.GetWordsWithData().First();
            var before = word.Explanations.Count();

            word.Explanations = null;
            WordsService.Update(word);
            var newWord = WordsService.GetWordsWithData().First();

            Assert.IsTrue(newWord.Explanations == null || newWord.Explanations.Count == 0, "Explanations should be empty");
        }

        [Test]
        public void RemovingWord_WithExplanationsAndDefinitions_UdpatesDB()
        {
            var before = WordsService.GetAll().Count();
            var word = WordsService.GetWordsWithData().First();
            WordsService.Remove(word);

            var after= WordsService.GetAll().Count();

            Assert.AreEqual(before - 1, after, "Count missmatch");
        }

        [Test]
        public void RemovingBook_WillUpdateDB_AndRemoveBookFromMasterWordsAutomatically()
        {
            var book = BooksService.GetAllWithData().First();

            BooksService.Remove(book);
            var newBook = BooksService.GetAllWithData().FirstOrDefault();
            var bCount = MasterWordsService.GetAll().First()._BookWordCollection.Count;

            Assert.IsNull(newBook);
            Assert.AreEqual(0, bCount);
        }

        [Test]
        public void RemovingMasterWord_WillUpdateDB_AndRemoveWordsFromBookAutomatically()
        {
            var before = BooksService.GetAllWithData().First().Words.Count();

            var word = MasterWordsService.GetAll().First();
            WordsService.Remove(word.Words); // removing all words will remove master automatically

            var newWord = MasterWordsService.GetAll().First();

            var after = BooksService.GetAllWithData().First().Words.Count();

            Assert.AreNotEqual(word, newWord);
            Assert.AreEqual(before - 1, after, "Count missmatch");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void RemovingAllWords_FromMaster_WillDeleteMasterAsWell(bool oneByOne)
        {
            var master = MasterWordsService.GetAll().First();
            var book = master.Books.First();

            var before = MasterWordsService.GetAll().Count();
            var beforeInBook = book.Words.Count();

            if (oneByOne)
            {
                foreach(var w in master.Words.ToArray())
                    WordsService.Remove(w);
            }
            else
            {
                WordsService.Remove(master.Words);
            }

            var secondMaster = MasterWordsService.GetAll().First();

            Assert.AreNotEqual(secondMaster, master);
            Assert.AreEqual(before - 1, MasterWordsService.GetAll().Count(), "Master count missmatch");
            Assert.AreEqual(beforeInBook - 1, book.Words.Count(), "Word in book count missmatch");
        }
    }
}
