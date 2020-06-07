using Langs.Data.Context;
using Langs.Services;
using Langs.Utilities;
using LanguageLearner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace Tests.Base
{
    public abstract class IntegrationTest
    {
        protected IHost m_Host;

        public virtual bool UseInMemoryDB => false; // SQLite is not compatible with my database model anymore

        protected IService[] Services => new IService[]
        {
            BooksService,
            WordsService,
            MasterWordsService,
            LanguagesService,
            AccountService,
        };

        protected IBooksService BooksService;
        protected IWordsService WordsService;
        protected IMasterWordsService MasterWordsService;
        protected ILanguagesService LanguagesService;
        protected IAccountService AccountService;

        protected IDatabaseContext DatabaseContext;

        [SetUp]
        public void CreateWebHostBuilderAndDatabase()
        {
            Startup.UseInMemoryDatabase = UseInMemoryDB;
            Startup.UseTestDatabase = true;

            m_Host = CreateWebHostBuilder();
            RefreshServicesAndClearCache();

            try
            {
                if (!UseInMemoryDB)
                {
                    DatabaseUtils.MigrateDB(DatabaseContext);
                    DatabaseUtils.ClearDB(DatabaseContext);
                }

                DatabaseUtils.PopulateWithTestData(DatabaseContext);
            }
            catch
            {
                DatabaseUtils.DeleteDB(DatabaseContext);
                if (!UseInMemoryDB)
                {
                    DatabaseUtils.MigrateDB(DatabaseContext);
                    DatabaseUtils.ClearDB(DatabaseContext);
                }

                DatabaseUtils.PopulateWithTestData(DatabaseContext);
            }
        }

        protected void RefreshServicesAndClearCache()
        {
            DatabaseContext = m_Host.Services.GetService<IDatabaseContext>();
            DatabaseContext.RefreshDatabaseContext();

            BooksService = m_Host.Services.GetService<IBooksService>();
            WordsService = m_Host.Services.GetService<IWordsService>();
            MasterWordsService = m_Host.Services.GetService<IMasterWordsService>();
            LanguagesService = m_Host.Services.GetService<ILanguagesService>();
            AccountService = m_Host.Services.GetService<IAccountService>();
        }

        public static IHost CreateWebHostBuilder() =>
         Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .Build();
    }
}
