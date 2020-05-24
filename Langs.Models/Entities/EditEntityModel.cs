using Langs.Data.Objects;
using Langs.Models.Shared;

namespace Langs.Models
{
    public class EditEntityModel : EntityModel, IAlertMessageModel
    {
        public bool ExpandTranslationList { get; set; }
        public (string LangName, string Text, int ID)[] WordsToLink { get; set; }
        public Language[] AvailableLanguages { get; set; }

        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }

        public static Word FillUpWord(EditEntityModel Model, Word word, Language language)
        {
            word.Text = Model.WordText;
            word.Article = Model.WordArticle;
            word.Pronunciation = Model.WordPronunciation;
            word.Description = Model.WordDescription;
            word.AlternateSpelling = Model.WordAlternateSpelling;
            word.Language = language;
            return word;
        }
    }
}
