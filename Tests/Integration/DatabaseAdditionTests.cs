using Langs.Data.Objects;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Integration
{
    public class DatabaseAdditionTests : IntegrationTest
    {
        [Test]
        public void Service_WithDefaultWebHost_IsResolved()
        {
            Assert.IsNotNull(BookService);
        }

        [Test]
        public void CanAdd_Word_ToDatabase()
        {
            WordsService.Add(new Word() { Text = "Something", Language = LanguagesService.GetAll().First() });
            Assert.AreEqual(7, WordsService.GetWordsWithData().Count());
        }

        [Test]
        public void CanAdd_WordWithLanguage_ToDatabase()
        {
            WordsService.Add(new Word() { Text = "Something", Language = new Language() { Name = "Another" } });

            Assert.AreEqual(7, WordsService.GetWordsWithData().Count());
            Assert.AreEqual(4, LanguagesService.GetAll().Count());
        }

        [Test]
        public void Adding_WordWithSameLanguage_WillNotIncrementLanguages()
        {
            var lang = new Language() { Name = "Another" };
            WordsService.Add(new Word() { Text = "Something", Language = lang });
            WordsService.Add(new Word() { Text = "anything", Language = lang });

            Assert.AreEqual(8, WordsService.GetWordsWithData().Count());
            Assert.AreEqual(4, LanguagesService.GetAll().Count());
        }
    }
}
