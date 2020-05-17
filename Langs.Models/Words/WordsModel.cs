using Langs.Models.Shared;

namespace Langs.Models.Words
{
    public class WordsModel : LanguagePickerModel
    {
        // Depending on language selection, it returns a list of words
        // or two arrays of tuples to represent translations from-to and ID for words

        public (int ID, string Language, string Text, string AlternateSpelling, string Pronunciation)[] Words { get; set; }

        public (int LeftID, string LeftText, int RightID, string RightText)[] Translations { get; set; }
    }
}
