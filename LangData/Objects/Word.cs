using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Word: Lang: {Language.Name} Text: {Text}")]
    public class Word
    {
        public Word() { }
        public Word(string text, Language language, string article = "")
        {
            Text = text;
            Language = language;
            Article = article;
        }

        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Language Language { get; set; }

        public string Article { get; set; }
        public string Pronunciation { get; set; }
        public string AlternateSpelling { get; set; }

        public List<Translation> Translations { get; set; }
    }
}
