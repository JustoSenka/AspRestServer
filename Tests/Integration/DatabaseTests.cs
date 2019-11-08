using LangData.Context;
using LangData.Objects;
using LangServices;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseTests : IntegrationTest
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
        public void Service_WithDefaultWebHost_IsResolved()
        {
            Assert.IsNotNull(BookService);
        }

        [Test]
        public void CanAdd_Word_ToDatabase()
        {
            BookService.AddWord(new Word() { Text = "Something" });
            Assert.AreEqual(1, BookService.GetWords().Count());
        }

        [Test]
        public void CanAdd_WordWithLanguage_ToDatabase()
        {
            BookService.AddWord(new Word() { Text = "Something", Language = new Language() { Name = "Spanish" } });

            Assert.AreEqual(1, BookService.GetWords().Count());
            Assert.AreEqual(1, BookService.GetLanguages().Count());
        }

        [Test]
        public void Adding_WordWithSameLanguage_WillNotIncrementLanguages()
        {
            var lang = new Language() { Name = "Spanish" };
            BookService.AddWord(new Word() { Text = "Something", Language = lang });
            BookService.AddWord(new Word() { Text = "anything", Language = lang });

            Assert.AreEqual(2, BookService.GetWords().Count());
            Assert.AreEqual(1, BookService.GetLanguages().Count());
        }
    }
}
