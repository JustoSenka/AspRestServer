using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    /// <summary>
    /// These tests use existing database which is created once.
    /// Tests do not alter the db.
    /// </summary>
    public class RealDatabaseTests : IntegrationTest
    {
        public override bool UseInMemoryDB => false;

        [Test]
        public void GettingBook_ContainsAListOfWords()
        {
            var book = BookService.GetBooksWithData().First();

            Assert.IsNotNull(book.Words);
            Assert.AreEqual(6, book.Words.Count);
        }

        [Test]
        public void GettingWords_ContainsALanguageAndListOfTranslation()
        {
            var word = WordsService.GetWordsWithData().First();

            Assert.IsNotNull(word.Language);
            Assert.IsNotNull(word.Translations);
            Assert.AreEqual(1, word.Translations.Count);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AllWords_ContainLanguage_AndTranslation(bool getWordsFromBook)
        {
            var words = getWordsFromBook ? BookService.GetBooksWithData().First().Words : WordsService.GetWordsWithData();
            foreach (var word in words)
            {
                Assert.IsNotNull(word.Language);
                Assert.IsNotNull(word.Translations);
                Assert.AreEqual(1, word.Translations.Count);
            }
        }

        [Test]
        // Note: order at which translations are added might differ from this array since we're adding the Book object only
        public void GettingDefinitions_ContainsALanguageAndListOfTranslation()
        {
            var def = DefinitionsService.GetDefinitionsWithData().First();

            Assert.IsNotNull(def.Language);
            Assert.IsNotNull(def.Translations);
            Assert.AreEqual(2, def.Translations.Count);

            var trans = TranslationsService.GetTranslationsWithLanguages().ElementAt(0);

            Assert.IsNotNull(trans.Definition);
            Assert.IsNotNull(trans.Word);
        }
    }
}
