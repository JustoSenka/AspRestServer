using LangData.Context;
using LanguageLearner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace Tests.Base
{
    public abstract class IntegrationTest
    {
        protected IWebHost Host;

        [SetUp]
        public void CreateWebHostBuilderAndDatabase()
        {
            Host = CreateWebHostBuilder();

            Host.Services.GetService<BookContext>().Database.EnsureCreated();
        }

        protected static IWebHost CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();
    }
}
