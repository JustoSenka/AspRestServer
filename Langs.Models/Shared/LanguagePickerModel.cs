using Langs.Data.Objects;

namespace Langs.Models.Shared
{
    public class LanguagePickerModel
    {
        public bool ShowNotSelectedError { get; set; }
        public Language[] AvailableLanguages { get; set; }

        public Language From { get; set; }
        public Language To { get; set; }

        public int LanguageToID { get; set; }
        public int LanguageFromID { get; set; }
    }
}
