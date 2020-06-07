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
        public void GettingBook_ContainsListOfMasterWords_EachOfThemHave3_OtherWords()
        {
            var book = BooksService.GetAllWithData().First();

            Assert.IsNotNull(book.Words, "Book is null");
            Assert.AreEqual(3, book.Words.Count(), "Master words not 3");
            Assert.IsTrue(book.Words.All(w => w.Words.Count == 3), "Inner words not 3");
        }

        [Test]
        public void GettingWords_ContainsALanguageAndListOfTranslation()
        {
            var word = WordsService.GetWordsWithData().First();

            Assert.IsNotNull(word.Language);
            Assert.IsNotNull(word.Translations);
            Assert.AreEqual(2, word.Translations.Count());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AllWords_ContainLanguage_AndTranslation(bool getWordsFromBook)
        {
            var words = getWordsFromBook ? BooksService.GetAllWithData().First().Words.First().Words : WordsService.GetWordsWithData();
            foreach (var word in words)
            {
                Assert.IsNotNull(word.Language);
                Assert.IsNotNull(word.Translations);
                Assert.AreEqual(2, word.Translations.Count());
            }
        }
    }
}
