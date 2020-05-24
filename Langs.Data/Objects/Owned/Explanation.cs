using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [Owned]
    [DebuggerDisplay("Translation: Text: {Text}")]
    public class Explanation
    {
        public Explanation() { }
        public Explanation(string text, Language languageTo)
        {
            Text = text;
            LanguageTo = languageTo;
        }

        [Required, MaxLength(100)] // short line, multiple words which explain translation better
        public virtual string Text { get; set; }

        [Required, ForeignKey("LanguageToID")]
        public virtual Language LanguageTo { get; set; }

        public virtual int? LanguageToID { get; set; }
    }
}
