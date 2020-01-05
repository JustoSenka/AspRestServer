using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Def: Lang: {Language.Name} Text: {Text}")]
    public class Definition : IHaveID
    {
        public Definition() { }
        public Definition(string text, Language language, string description = "")
        {
            Text = text;
            Language = language;
            Description = description;
        }

        [Required]
        public string Text { get; set; }
        [Required]
        public Language Language { get; set; }

        public string Description { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
