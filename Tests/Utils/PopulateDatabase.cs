using LangData.Context;
using LangData.Objects;

namespace Tests.Utils
{
    public static class PopulateDatabase
    {
        public static void ClearDatabase(BookContext BookContext)
        {
            BookContext.Books.RemoveRange(BookContext.Books);
            BookContext.Translations.RemoveRange(BookContext.Translations);
            BookContext.Words.RemoveRange(BookContext.Words);
            BookContext.Definitions.RemoveRange(BookContext.Definitions);
            BookContext.Languages.RemoveRange(BookContext.Languages);
        }

        public static void PopulateWithTestData(BookContext BookContext)
        {
            var langEn = new Language("English");
            var langEsp = new Language("Spanish");
            var langJp = new Language("Japanese");

            var def = new[]
            {
                new Definition("Hello", langEn),
                new Definition("Nice to meet you", langEn),
                new Definition("How are you", langEn),
            };

            var words = new[]
            {
                new Word("Buenos dias", langEsp),
                new Word("Mucho gusto", langEsp),
                new Word("Como estas", langEsp),
                new Word("こんにちわ", langJp),
                new Word("はじめまして", langJp),
                new Word("おーげんきです", langJp),
            };

            var translations = new[]
            {
                new Translation(words[0], def[0]),
                new Translation(words[1], def[1]),
                new Translation(words[2], def[2]),
                new Translation(words[3], def[0]),
                new Translation(words[4], def[1]),
                new Translation(words[5], def[2]),
            };
            
            def[0].Translations = new[] { translations[0], translations[3] };
            def[1].Translations = new[] { translations[1], translations[4] };
            def[2].Translations = new[] { translations[2], translations[5] };

            words[0].Translations = new[] { translations[0] };
            words[1].Translations = new[] { translations[1] };
            words[2].Translations = new[] { translations[2] };
            words[3].Translations = new[] { translations[3] };
            words[4].Translations = new[] { translations[4] };
            words[5].Translations = new[] { translations[5] };
            
            var book = new Book("Book 0", words);

            BookContext.Books.Add(book);
            BookContext.SaveChanges();
        }
    }
}
