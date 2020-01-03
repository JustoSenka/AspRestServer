using LangData.Objects;

namespace LanguageLearner.Models.Shared
{
    public class LanguagePickerModel
    {
        public Language[] AvailableLanguages { get; set; }

        public Language From { get; set; }
        public Language To { get; set; }

        public int LanguageToID { get; set; }
        public int LanguageFromID { get; set; }
    }
}
