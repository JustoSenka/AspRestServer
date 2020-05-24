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
            Assert.IsNotNull(BooksService);
        }

        [Test]
        public void CanAdd_Word_ToDatabase()
        {
            var beforeWords = WordsService.GetAll().Count();
            WordsService.Add(new Word() { MasterWord = new MasterWord(), Text = "Something", Language = LanguagesService.GetAll().First() });
            Assert.AreEqual(beforeWords + 1, WordsService.GetWordsWithData().Count());
        }

        [Test]
        public void Adding_WordWithSameLanguage_WillNotIncrementLanguages()
        {
            var beforeWords = WordsService.GetAll().Count();
            var beforeLangs = LanguagesService.GetAll().Count();

            var lang = new Language() { Name = "Another" };
            WordsService.Add(new Word() { MasterWord = new MasterWord(), Text = "Something", Language = lang });
            WordsService.Add(new Word() { MasterWord = new MasterWord(), Text = "anything", Language = lang });

            Assert.AreEqual(beforeWords + 2, WordsService.GetWordsWithData().Count(), "Words count missmatch");
            Assert.AreEqual(beforeLangs + 1, LanguagesService.GetAll().Count(), "Language count missmatch");
        }
    }
}
