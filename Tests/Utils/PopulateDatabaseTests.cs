using LangData.Context;
using LangServices;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Utils
{
    class PopulateDatabaseTests : IntegrationTest
    {
        private IBookService BookService;
        private BookContext BookContext;

        // public override bool UseInMemoryDB => false;

        [SetUp]
        public void Setup()
        {
            BookService = Host.Services.GetService<IBookService>();
            BookContext = Host.Services.GetService<BookContext>();
        }

        [Test]
        public void AddSingleBook_CheckIfDataCountIsCorrect()
        {
            PopulateDatabase.PopulateWithTestData(BookContext);

            Assert.AreEqual(1, BookService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(3, BookService.GetLanguages().Count(), "Langs");
            Assert.AreEqual(3, BookService.GetDefinitionsWithData().Count(), "Defs");
            Assert.AreEqual(6, BookService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(6, BookService.GetTranslationsWithData().Count(), "Translations");
        }

        [Test]
        [Ignore("Can be used in special occasions whe something goes wrong. Do not enable for normal test run.")]
        public void DeteteDB()
        {
            PopulateDatabase.DeleteDB(BookContext);
        }

        [Test]
        public void ClearDB()
        {
            PopulateDatabase.PopulateWithTestData(BookContext);
            PopulateDatabase.ClearDatabase(BookContext);

            Assert.AreEqual(0, BookService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(0, BookService.GetLanguages().Count(), "Langs");
            Assert.AreEqual(0, BookService.GetDefinitionsWithData().Count(), "Defs");
            Assert.AreEqual(0, BookService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(0, BookService.GetTranslationsWithData().Count(), "Translations");
        }
    }
}
