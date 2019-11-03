using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspRestServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var http = configuration["http"];
            if (string.IsNullOrEmpty(http))
                http = "http://0.0.0.0:80";

            var https = configuration["https"];
            if (string.IsNullOrEmpty(https))
                https = "https://0.0.0.0:443";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(http, https)  
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .Build();

            host.Run();
        }
    }
}
