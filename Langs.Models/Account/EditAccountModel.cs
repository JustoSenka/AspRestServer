using Langs.Data.Objects;
using Langs.Models.Shared;
using System;

namespace Langs.Models.Accounts
{
    public class EditAccountModel : IAlertMessageModel
    {
        public string AccountName{ get; set; }
        public int NativeLanguageID { get; set; }
        public int LearningLanguageID { get; set; }
        public int AdditionalLanguageID { get; set; }
        public Language[] AvailableLanguages { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }
    }
}
