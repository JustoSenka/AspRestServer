using LangData.Context;
using LanguageLearner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using Tests.Utils;

namespace Tests.Base
{
    public abstract class IntegrationTest
    {
        protected IWebHost Host;

        public virtual bool UseInMemoryDB => true;

        [SetUp]
        public void CreateWebHostBuilderAndDatabase()
        {
            Startup.UseInMemoryDatabase = UseInMemoryDB;

            Host = CreateWebHostBuilder();

            var BookContext = Host.Services.GetService<DatabaseContext>();
            BookContext.Database.EnsureCreated();

            if (UseInMemoryDB || BookContext.Books.Count() == 0)
            {
                PopulateDatabase.PopulateWithTestData(BookContext);
            }
        }

        protected static IWebHost CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();
    }
}
