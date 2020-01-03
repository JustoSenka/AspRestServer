using LanguageLearner.Models.Shared;

namespace LanguageLearner.Models.Words
{
    public class AddWordsModel : LanguagePickerModel
    {
        public bool TreatFirstWordAsArticle { get; set; }

        public string WordsText { get; set; }
        public string DefinitionsText { get; set; }
    }
}
