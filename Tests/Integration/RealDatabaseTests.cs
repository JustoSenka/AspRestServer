using LangData.Context;
using LangServices;
using Microsoft.Extensions.DependencyInjection;
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
        private IBookService BookService;
        private BookContext BookContext;

        public override bool UseInMemoryDB => false;

        [SetUp]
        public void Setup()
        {
            BookService = Host.Services.GetService<IBookService>();
            BookContext = Host.Services.GetService<BookContext>();
        }

        [Test]
        public void DatabaseContains_CorrectAmountOfItems()
        {
            Assert.AreEqual(1, BookService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(3, BookService.GetLanguages().Count(), "Langs");
            Assert.AreEqual(3, BookService.GetDefinitionsWithData().Count(), "Defs");
            Assert.AreEqual(6, BookService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(6, BookService.GetTranslationsWithData().Count(), "Translations");
        }

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
            var word = BookService.GetWordsWithData().First();

            Assert.IsNotNull(word.Language);
            Assert.AreEqual(BookService.GetLanguages().First(lang => lang.Name == "Spanish"), word.Language);

            Assert.IsNotNull(word.Translations);
            Assert.AreEqual(1, word.Translations.Count);
            Assert.AreEqual(BookService.GetTranslationsWithData().First(), word.Translations.First());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AllWords_ContainLanguage_AndTranslation(bool getWordsFromBook)
        {
            var words = getWordsFromBook ? BookService.GetBooksWithData().First().Words : BookService.GetWordsWithData();
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
            var def = BookService.GetDefinitionsWithData().First();

            Assert.IsNotNull(def.Language);
            Assert.AreEqual(BookService.GetLanguages().First(lang => lang.Name == "English"), def.Language);

            Assert.IsNotNull(def.Translations);
            Assert.AreEqual(2, def.Translations.Count);
            Assert.AreEqual(BookService.GetTranslationsWithData().ElementAt(0), def.Translations.ElementAt(0));
            Assert.AreEqual(BookService.GetTranslationsWithData().ElementAt(3), def.Translations.ElementAt(1));

            var trans = BookService.GetTranslationsWithData().ElementAt(0);

            Assert.IsNotNull(trans.Definition);
            Assert.IsNotNull(trans.Word);
        }
    }
}