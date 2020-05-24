using Langs.Data.Context;
using Langs.Data.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.XEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Langs.Utilities
{
    public static class DatabaseUtils
    {
        public static void DeleteDB(DatabaseContext DatabaseContext)
        {
            DatabaseContext.Database.EnsureDeleted();
        }

        public static void MigrateDB(DatabaseContext DatabaseContext)
        {
            DatabaseContext.Database.Migrate();
        }

        public static void MigrateDB(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                MigrateDB(context);
            }
        }

        public static void ClearDB(DatabaseContext DatabaseContext)
        {
            DatabaseContext.Words.RemoveRange(DatabaseContext.Words);
            DatabaseContext.MasterWords.RemoveRange(DatabaseContext.MasterWords);
            DatabaseContext.Books.RemoveRange(DatabaseContext.Books);
            DatabaseContext.Languages.RemoveRange(DatabaseContext.Languages);

            DatabaseContext.Accounts.RemoveRange(DatabaseContext.Accounts);

            DatabaseContext.SaveChanges();
        }

        public static void PopulateWithTestData(DatabaseContext DatabaseContext)
        {
            MigrateDB(DatabaseContext);

            var langEn = new Language("English");
            var langEsp = new Language("Spanish");
            var langJp = new Language("Japanese");

            var masterWords = Enumerable.Repeat<Func<MasterWord>>(() => new MasterWord(), 3).Select(a => a()).ToArray();

            var words = new[]
            {
                new Word(masterWords[0], "Buenos dias", langEsp),
                new Word(masterWords[1], "Mucho gusto", langEsp),
                new Word(masterWords[2], "Como estas", langEsp),
                new Word(masterWords[0], "こんにちわ", langJp),
                new Word(masterWords[1], "はじめまして", langJp),
                new Word(masterWords[2], "おげんきです", langJp),
                new Word(masterWords[0], "Hello", langEn),
                new Word(masterWords[1], "Nice to meet you", langEn),
                new Word(masterWords[2], "How are you", langEn),
            };

            words[0].Definition = new Definition("saludo utilizado durante la mañana");
            words[3].Definition = new Definition("その日初めて人に会うときの挨拶として使われる");
            words[6].Definition = new Definition("used as a greeting or to begin a conversation");

            words[0].Explanations = new List<Explanation> { new Explanation("Hello, good morning, good day", langEn) };
            words[3].Explanations = new List<Explanation> { new Explanation("Hello, good day", langEn) };

            var book = new Book("Book 0", langEn, "") { Words = masterWords.ToList() };

            DatabaseContext.Books.Add(book);
            DatabaseContext.Accounts.Add(new Account() { Name = "Justas", LearningLanguage = langEsp, NativeLanguage = langEn });
            DatabaseContext.SaveChanges();
        }
    }
}
