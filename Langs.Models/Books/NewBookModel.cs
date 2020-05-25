using Langs.Data.Objects;
using Langs.Models.Shared;

namespace Langs.Models.Books
{
    public class NewBookModel : IAlertMessageModel
    {
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public int BookLanguageID { get; set; }
        public Language[] AvailableLanguages { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }
    }
}
