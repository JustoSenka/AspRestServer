using LangData.Objects;
using LanguageLearner.Models.Shared;
using LanguageLearner.Utilities;

namespace LanguageLearner.Models.Languages
{
    public class LanguagesModel : IAlertMessageModel
    {
        public Language[] AvailableLanguages { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }
    }
}
