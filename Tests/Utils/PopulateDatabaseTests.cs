using Langs.Utilities;
using NUnit.Framework;
using System.Linq;
using Tests.Base;

namespace Tests.Utils
{
    public class PopulateDatabaseTests : IntegrationTest
    {
        public override bool UseInMemoryDB => false;

        [Test]
        public void AddSingleBook_CheckIfDataCountIsCorrect()
        {
            PopulateDatabase.PopulateWithTestData(DatabaseContext);

            Assert.AreEqual(1, BookService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(3, LanguagesService.GetAll().Count(), "Langs");
            Assert.AreEqual(3, DefinitionsService.GetDefinitionsWithData().Count(), "Defs");
            Assert.AreEqual(6, WordsService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(6, TranslationsService.GetTranslationsWithLanguages().Count(), "Translations");
        }

        [Test]
        [Ignore("Can be used in special occasions whe something goes wrong. Do not enable for normal test run.")]
        // Also deletes migration history if used on main db
        public void DeteteDB()
        {
            PopulateDatabase.DeleteDB(DatabaseContext);
        }

        [Test]
        public void ClearDB()
        {
            PopulateDatabase.PopulateWithTestData(DatabaseContext);
            PopulateDatabase.ClearDatabase(DatabaseContext);

            Assert.AreEqual(0, BookService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(0, LanguagesService.GetAll().Count(), "Langs");
            Assert.AreEqual(0, DefinitionsService.GetDefinitionsWithData().Count(), "Defs");
            Assert.AreEqual(0, WordsService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(0, TranslationsService.GetTranslationsWithLanguages().Count(), "Translations");
        }
    }
}
