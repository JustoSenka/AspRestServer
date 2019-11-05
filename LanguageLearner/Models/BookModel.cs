namespace LanguageLearner.Objects
{
    public class BookModel
    {
        public Language LanguageFrom { get; set; }
        public Language LanguageTo { get; set; }

        public Translation[] Words { get; set; }

        // public IDictionary<Word, Definition> Dictionary { get; set; }
    }
}
