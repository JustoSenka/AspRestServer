namespace Langs.Models.Books
{
    public class BookModel
    {
        public (int WordID, string Word, int TranslationID, string Translation, int OtherID, string Other)[] Words { get; set; }
        public (string Name, string LanguageName, string Description, int WordCount) Book { get; set; }
        public (string Learning, string Native, string Additional) Languages { get; set; }
    }
}
