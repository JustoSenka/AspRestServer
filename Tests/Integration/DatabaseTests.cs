using LangData.Context;
using LangData.Objects;
using LangServices;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using Tests.Base;
using Tests.Utils;

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
            Assert.AreEqual(7, BookService.GetWords().Count());
        }

        [Test]
        public void CanAdd_WordWithLanguage_ToDatabase()
        {
            BookService.AddWord(new Word() { Text = "Something", Language = new Language() { Name = "Another" } });

            Assert.AreEqual(7, BookService.GetWords().Count());
            Assert.AreEqual(4, BookService.GetLanguages().Count());
        }

        [Test]
        public void Adding_WordWithSameLanguage_WillNotIncrementLanguages()
        {
            var lang = new Language() { Name = "Another" };
            BookService.AddWord(new Word() { Text = "Something", Language = lang });
            BookService.AddWord(new Word() { Text = "anything", Language = lang });

            Assert.AreEqual(8, BookService.GetWords().Count());
            Assert.AreEqual(4, BookService.GetLanguages().Count());
        }

        [Test]
        public void RemovingTranslationFromWord_WillUpdateDB()
        {
            var word = BookService.GetWords().First();
            var trans = word.Translations.First();

            word.Translations.Remove(trans);
            BookService.UpdateWord(word);

            var newWord = BookService.GetWords().First();

            Assert.AreEqual(0, newWord.Translations.Count);
        }

        [Test]
        public void RemovingWord_WillUpdateDB_AndRemoveWordsFromBookAutomatically()
        {
            var word = BookService.GetWords().First();

            BookService.RemoveWord(word);
            var newWord = BookService.GetWords().First();
            var wCount = BookService.GetBooks().First().Words.Count;

            Assert.AreNotEqual(word, newWord);
            Assert.AreEqual(5, wCount);
        }

        [Test]
        public void RemovingDefinition_WillUpdateDB_AndRemoveWordsFromTranslationsAutomatically()
        {
            var word = BookService.GetWords().First();

            BookService.RemoveDefinition(word.Translations[0].Definition);
            var newWord = BookService.GetWords().First();
            var trans = newWord.Translations[0];

            Assert.IsNull(trans.Definition);
        }
    }
}
