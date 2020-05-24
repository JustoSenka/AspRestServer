using Langs.Services;
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
            // DatabaseUtils.PopulateWithTestData(DatabaseContext); // This is done in setup

            Assert.AreEqual(1, BooksService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(3, LanguagesService.GetAll().Count(), "Langs");
            Assert.AreEqual(9, WordsService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(3, MasterWordsService.GetAll().Count(), "MasterWords");
        }

        [Test]
        [Ignore("Can be used in special occasions whe something goes wrong. Do not enable for normal test run.")]
        // Also deletes migration history if used on main db
        public void DeteteDB()
        {
            DatabaseUtils.DeleteDB(DatabaseContext);
        }

        [Test]
        public void ClearDB()
        {
            // DatabaseUtils.PopulateWithTestData(DatabaseContext); // This is done in setup
            DatabaseUtils.ClearDB(DatabaseContext);

            Assert.AreEqual(0, BooksService.GetBooksWithData().Count(), "Books");
            Assert.AreEqual(0, LanguagesService.GetAll().Count(), "Langs");
            Assert.AreEqual(0, WordsService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(0, MasterWordsService.GetAll().Count(), "MasterWords");
        }
    }
}
