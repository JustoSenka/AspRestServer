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

            Assert.AreEqual(1, BookService.GetBooks().Count());
            Assert.AreEqual(3, BookService.GetLanguages().Count());
            Assert.AreEqual(3, BookService.GetDefinitions().Count());
            Assert.AreEqual(6, BookService.GetWords().Count());
        }
    }
}
