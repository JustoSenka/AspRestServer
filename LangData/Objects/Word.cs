using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Word: Lang: {Language.Name} Text: {Text}")]
    public class Word
    {
        public Word() { }
        public Word(string text, Language language)
        {
            Text = text;
            Language = language;
        }

        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        public Language Language { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
