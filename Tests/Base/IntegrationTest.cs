using LangData.Context;
using LangServices;
using LanguageLearner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Base
{
    public abstract class IntegrationTest
    {
        protected IWebHost Host;

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

            Host = CreateWebHostBuilder();

            DatabaseContext = Host.Services.GetService<DatabaseContext>();
            BookService = Host.Services.GetService<IBooksService>();
            WordsService = Host.Services.GetService<IWordsService>();
            LanguagesService = Host.Services.GetService<ILanguagesService>();
            DefinitionsService = Host.Services.GetService<IDefinitionsService>();
            TranslationsService = Host.Services.GetService<ITranslationsService>();

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

        protected static IWebHost CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();
    }
}
