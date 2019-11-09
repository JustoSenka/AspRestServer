using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
    public class Definition
    {
        public Definition() { }
        public Definition(string text, Language language, string examples = "")
        {
            Text = text;
            Language = language;
            Examples = examples;
        }

        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        public string Examples { get; set; }

        public Language Language { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
