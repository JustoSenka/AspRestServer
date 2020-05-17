using Langs.Data.Objects;
using Langs.Models.Shared;

namespace Langs.Models
{
    public class EditEntityModel : EntityModel, IAlertMessageModel
    {
        public bool ExpandTranslationList { get; set; }

        public Language[] AvailableLanguages { get; set; }
        public int LanguageID { get; set; }

        public Word[] Words { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }
    }
}
