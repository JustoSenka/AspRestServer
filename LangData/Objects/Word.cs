using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
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
        public IEnumerable<Translation> Translations { get; set; }
    }
}
