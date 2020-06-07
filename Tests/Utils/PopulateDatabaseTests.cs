using Langs.Services;
using Langs.Utilities;
using NUnit.Framework;
using System;
using System.Collections;
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
            Assert.AreEqual(1, BooksService.GetAllWithData().Count(), "Books");
            Assert.AreEqual(3, LanguagesService.GetAll().Count(), "Langs");
            Assert.AreEqual(9, WordsService.GetWordsWithData().Count(), "Words");
            Assert.AreEqual(3, MasterWordsService.GetAll().Count(), "MasterWords");
            Assert.AreEqual(1, AccountService.GetAll().Count(), "Accounts");
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
            DatabaseContext.RefreshDatabaseContext();
            DatabaseUtils.ClearDB(DatabaseContext);

            foreach (var service in Services)
            {
                Assert.AreEqual(0, service.Count(), service.GetType().Name);
            }
        }
    }
}
