using Langs.Data.Objects.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Translation: Text: {Text}")]
    public class Explanation : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Explanation() { }
        public Explanation(string text, Language languageTo)
        {
            Text = text;
            LanguageTo = languageTo;
        }

        [Required, MaxLength(100)] // short line, multiple words which explain translation better
        public virtual string Text { get; set; }

        [Required]
        public virtual Language LanguageTo { get; set; }

        // ----------
        string IDisplayText.DisplayText => Text;
    }
}
