using Langs.Data.Objects;
using System.Linq;

namespace Langs.Models
{
    public class EntityModel
    {
        public int WordID { get; set; }
        public string WordText { get; set; }
        public string WordLanguage { get; set; }
        public string WordArticle { get; set; }
        public string WordDescription { get; set; }
        public string WordPronunciation { get; set; }
        public string WordAlternateSpelling { get; set; }

        public int WordLanguageID { get; set; }
        public int PreferredDefaultLanguageID { get; set; }

        public string TranslationText { get; set; }
        public string TranslationDescription { get; set; }

        public (string LangName, string Explanation)[] Explanations { get; set; }
        public (string LangName, string Text, int ID)[] Translations { get; set; }

        public static EntityModel FillUpModel(EntityModel Model, Word word, Word translation, Language nativeLang)
        {
            Model.WordID = word.ID;
            Model.WordText = word.Text;
            Model.WordLanguage = word.Language.Name;
            Model.WordLanguageID = word.Language.ID;
            Model.WordArticle = word.Article;
            Model.WordPronunciation = word.Pronunciation;
            Model.WordDescription = word.Description;
            Model.WordAlternateSpelling = word.AlternateSpelling;
            Model.Explanations = word.Explanations?.Select(e => (e.LanguageTo.Name, e.Text)).ToArray();
            Model.Translations = word.Translations?.Select(e => (e.Language.Name, e.Text, e.ID)).ToArray();
            Model.TranslationText = translation?.Text;
            Model.TranslationDescription = translation?.Description;
            Model.PreferredDefaultLanguageID = nativeLang.ID;
            return Model;
        }
    }
}
