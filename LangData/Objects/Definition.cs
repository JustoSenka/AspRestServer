using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Def: Lang: {Language.Name} Text: {Text}")]
    public class Definition
    {
        public Definition() { }
        public Definition(string text, Language language, string examples = "", string alternateText = "")
        {
            Text = text;
            Language = language;
            Examples = examples;
            AlternateText = alternateText;
        }

        public int ID { get; set; }

        [Required]
        public string Text { get; set; }
        public string AlternateText { get; set; }

        public string Examples { get; set; }

        public Language Language { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
