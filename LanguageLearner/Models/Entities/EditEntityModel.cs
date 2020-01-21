using LangData.Objects;
using LanguageLearner.Models.Shared;

namespace LanguageLearner.Models
{
    public class EditEntityModel : EntityModel, IAlertMessageModel
    {
        public Language[] AvailableLanguages { get; set; }
        public int LanguageID { get; set; }

        public Definition[] Definitions { get; set; }
        public Word[] Words { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }
    }
}
