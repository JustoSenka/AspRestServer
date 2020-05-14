using Langs.Data.Objects.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Word: Lang: {Language.Name} Text: {Text}")]
    public class Word : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Word() { }
        public Word(string text, Language language, string article = "")
        {
            Text = text;
            Language = language;
            Article = article;
        }

        [Required]
        public string Text { get; set; }

        [Required]
        public Language Language { get; set; }

        public string Article { get; set; }
        public string Pronunciation { get; set; }
        public string AlternateSpelling { get; set; }

        public List<Translation> Translations { get; set; }

        string IDisplayText.DisplayText => Text;
    }
}
