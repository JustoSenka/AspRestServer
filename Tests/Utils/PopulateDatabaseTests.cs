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

            Assert.AreEqual(1, BookService.GetBooks().Count(), "Books");
            Assert.AreEqual(3, BookService.GetLanguages().Count(), "Langs");
            Assert.AreEqual(3, BookService.GetDefinitions().Count(), "Defs");
            Assert.AreEqual(6, BookService.GetWords().Count(), "Words");
            Assert.AreEqual(6, BookService.GetTranslations().Count(), "Translations");
        }

        [Test]
        public void DeteteDB()
        {
            PopulateDatabase.DeleteAndRecreateDB(BookContext);
        }

        [Test]
        public void ClearDB()
        {
            PopulateDatabase.PopulateWithTestData(BookContext);
            PopulateDatabase.ClearDatabase(BookContext);

            Assert.AreEqual(0, BookService.GetBooks().Count(), "Books");
            Assert.AreEqual(0, BookService.GetLanguages().Count(), "Langs");
            Assert.AreEqual(0, BookService.GetDefinitions().Count(), "Defs");
            Assert.AreEqual(0, BookService.GetWords().Count(), "Words");
            Assert.AreEqual(0, BookService.GetTranslations().Count(), "Translations");
        }
    }
}
