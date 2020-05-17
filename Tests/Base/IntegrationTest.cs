using Langs.Data.Context;
using Langs.Services;
using Langs.Utilities;
using LanguageLearner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace Tests.Base
{
    public abstract class IntegrationTest
    {
        protected IHost m_Host;

        public virtual bool UseInMemoryDB => true;

        protected IBooksService BookService;
        protected IWordsService WordsService;
        protected ILanguagesService LanguagesService;
        protected IExplanationsService ExplanationsService;
        protected IDefinitionsService DefinitionsService;

        protected DatabaseContext DatabaseContext;

        [SetUp]
        public void CreateWebHostBuilderAndDatabase()
        {
            Startup.UseInMemoryDatabase = UseInMemoryDB;
            Startup.UseTestDatabase = true;

            m_Host = CreateWebHostBuilder();

            DatabaseContext = m_Host.Services.GetService<DatabaseContext>();

            BookService = m_Host.Services.GetService<IBooksService>();
            WordsService = m_Host.Services.GetService<IWordsService>();
            LanguagesService = m_Host.Services.GetService<ILanguagesService>();
            ExplanationsService = m_Host.Services.GetService<IExplanationsService>();
            DefinitionsService = m_Host.Services.GetService<IDefinitionsService>();

            try
            {
                if (!UseInMemoryDB)
                    DatabaseUtils.MigrateDB(DatabaseContext);
                DatabaseUtils.PopulateWithTestData(DatabaseContext);
            }
            catch
            {
                DatabaseUtils.DeleteDB(DatabaseContext);
                if (!UseInMemoryDB)
                    DatabaseUtils.MigrateDB(DatabaseContext);
                DatabaseUtils.PopulateWithTestData(DatabaseContext);
            }
        }

        public static IHost CreateWebHostBuilder() =>
         Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .Build();
    }
}
