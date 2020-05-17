using Langs.Controllers;
using Langs.Data.Context;
using Langs.Services;
using Langs.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace LanguageLearner
{
    public class Startup
    {
        /// <summary>
        /// Used only from tests
        /// </summary>
        public static bool UseInMemoryDatabase = false;
        public static bool UseTestDatabase = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var msg = Configuration.GetValue<string>("WelcomeMessage");
            Debug.WriteLine("Config in use: " + msg);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(HomeController).Assembly).AddControllersAsServices();
            services.AddControllersWithViews();

            services.AddSingleton(Configuration);

            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IWordsService, WordsService>();
            services.AddScoped<ILanguagesService, LanguagesService>();
            services.AddScoped<IExplanationsService, ExplanationService>();
            services.AddScoped<IUserService, UserService>();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            SetupDatabase(services);
        }

        private void SetupDatabase(IServiceCollection services)
        {
            // var inMemory = Configuration.GetValue<bool>("UseInMemoryDB");
            var inMemory = UseInMemoryDatabase;
            if (inMemory)
            {
                var connectionString = Configuration.GetConnectionString("InMemoryDB");
                var connection = new SqliteConnection(connectionString);
                connection.Open();

                services.AddDbContext<DatabaseContext>(o => o.UseSqlite(connection));
            }
            else
            {
                var dbName = UseTestDatabase ? "TestDB" : "MainDB";
                var connectionString = Configuration.GetConnectionString(dbName);

                Debug.WriteLine("Database in use: " + dbName);

                services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("Langs.Data")));
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                // app.UseHsts();

                // uncomment above when moving to production, now it's easier to always see exception
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
