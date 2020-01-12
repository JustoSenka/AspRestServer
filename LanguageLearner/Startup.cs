﻿using LangData.Context;
using LangServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LanguageLearner
{
    public class Startup
    {
        /// <summary>
        /// Used only from tests
        /// </summary>
        public static bool UseInMemoryDatabase = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var msg = Configuration.GetValue<string>("WelcomeMessage");
            System.Diagnostics.Debug.WriteLine("Config in use: " + msg);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(Configuration);

            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IWordsService, WordsService>();
            services.AddScoped<ILanguagesService, LanguagesService>();
            services.AddScoped<IDefinitionsService, DefinitionsService>();
            services.AddScoped<ITranslationsService, TranslationsService>();
            services.AddScoped<IUserService, UserService>();

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
                var connectionString = Configuration.GetConnectionString("LanguageLearner");
                services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("LangData")));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO: Always using developer exception handling while page is in development
                app.UseDeveloperExceptionPage();

                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
