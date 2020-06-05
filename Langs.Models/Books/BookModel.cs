namespace Langs.Models.Books
{
    public class BookModel
    {
        /// <summary>
        /// ID = master word ID.
        /// Word = translated word to user/book prefered language.
        /// </summary>
        public (int ID, string Word)[] WordsToAdd { get; set; }
        public (int MasterWordID, int WordID, string Word, int TranslationID, string Translation, int OtherID, string Other)[] Words { get; set; }
        public (int ID, string Name, string LanguageName, string Description, int WordCount) Book { get; set; }
        public (string Learning, string Native, string Additional) Languages { get; set; }
    }
}
