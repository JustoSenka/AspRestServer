using LangData.Objects;
using LangServices;
using LanguageLearner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private IWebHost m_Host;

        [SetUp]
        public void Setup()
        {
            m_Host = CreateWebHostBuilder();
        }

        public static IWebHost CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();

        [Test]
        public void Service_WithDefaultWebHost_IsResolved()
        {
            var service = m_Host.Services.GetService<IBookService>();
            Assert.IsNotNull(service);
        }

        [Test]
        public void CanAdd_Word_ToDatabase()
        {
            var service = m_Host.Services.GetService<IBookService>();
            var count = service.GetWords().Count();

            service.AddWord(new Word() { Text = "Something" });
            Assert.AreEqual(count + 1, service.GetWords().Count());
        }
    }
}
