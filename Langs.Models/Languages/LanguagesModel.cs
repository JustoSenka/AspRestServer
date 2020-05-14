using Langs.Data.Objects;
using Langs.Models.Shared;

namespace Langs.Models.Languages
{
    public class LanguagesModel : IAlertMessageModel
    {
        public Language[] AvailableLanguages { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }
    }
}
