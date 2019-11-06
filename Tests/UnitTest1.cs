using LangData.Context;
using LangServices;
using LanguageLearner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore;

namespace Tests
{
    public class Tests
    {
        private DependencyResolverHelpercs _serviceProvider;

        public Tests()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelpercs(webHost);
        }

        [Test]
        public void Service_Should_Get_Resolved()
        {
            var YourService = _serviceProvider.GetService<IBookService>();
            Assert.IsNotNull(YourService);
        }
    }

    public class DependencyResolverHelpercs
    {
        private readonly IWebHost _webHost;

        /// <inheritdoc />
        public DependencyResolverHelpercs(IWebHost WebHost) => _webHost = WebHost;

        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            };
        }
    }
}
