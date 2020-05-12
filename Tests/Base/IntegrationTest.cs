using LangData.Context;
using LangServices;
using LanguageLearner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Base
{
    public abstract class IntegrationTest
    {
        protected IHost m_Host;

        public virtual bool UseInMemoryDB => true;

        protected IBooksService BookService;
        protected IWordsService WordsService;
        protected ILanguagesService LanguagesService;
        protected IDefinitionsService DefinitionsService;
        protected ITranslationsService TranslationsService;

        protected DatabaseContext DatabaseContext;

        [SetUp]
        public void CreateWebHostBuilderAndDatabase()
        {
            Startup.UseInMemoryDatabase = UseInMemoryDB;

            m_Host = CreateWebHostBuilder();

            DatabaseContext = m_Host.Services.GetService<DatabaseContext>();
            BookService = m_Host.Services.GetService<IBooksService>();
            WordsService = m_Host.Services.GetService<IWordsService>();
            LanguagesService = m_Host.Services.GetService<ILanguagesService>();
            DefinitionsService = m_Host.Services.GetService<IDefinitionsService>();
            TranslationsService = m_Host.Services.GetService<ITranslationsService>();

            try
            {
                PopulateDatabase.PopulateWithTestData(DatabaseContext);
            }
            catch
            {
                PopulateDatabase.DeleteDB(DatabaseContext);
                PopulateDatabase.PopulateWithTestData(DatabaseContext);
            }
        }

        /*
        protected static IWebHost CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();
        */
        public static IHost CreateWebHostBuilder() =>
         Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .Build();
    }
}
